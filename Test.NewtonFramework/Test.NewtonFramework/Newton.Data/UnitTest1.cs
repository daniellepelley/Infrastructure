using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newton.Data;

using System.Reactive.Linq;

namespace Test.NewtonFramework
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AyncMethodTest1()
        {
            var factory = new Newton.Testing.TestDataRepositoryFactory();
            var repository = factory.Create<string>();

            repository = new SlowRepository<string>(repository, new TimeSpan(0, 0, 5));

            var asyncRepository = new ReactiveRepository<string>(repository);
            asyncRepository.SaveAsync("Hi", CallBack);
            System.Threading.Thread.Sleep(8000);
        }

        [TestMethod]
        public void AyncMethodTest2()
        {
            var factory = new Newton.Testing.TestDataRepositoryFactory();
            var repository = factory.Create<TestClass>();

            repository = new SlowRepository<TestClass>(repository, new TimeSpan(0, 0, 0, 0, 500));
            var asyncRepository = new ReactiveRepository<TestClass>(repository);

            asyncRepository.Items.ObserveOn(new System.Reactive.Concurrency.NewThreadScheduler()).Subscribe(CallBack);
            System.Threading.Thread.Sleep(8000);
        }

        public void CallBack(TestClass value)
        {
        
        }

        public void CallBack(string value)
        {

        }
    }

    public class TestClass
    {
        public string Field1 { set; get; }
        public string Field2 { set; get; }
    }
}
