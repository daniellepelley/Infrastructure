//using Newton.Testing;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using System.Linq;

//namespace Test.NewtonFramework
//{
    
    
//    /// <summary>
//    ///This is a test class for MockRepositoryTest and is intended
//    ///to contain all MockRepositoryTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class MockRepositoryTest
//    {


//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        #region Additional test attributes
//        // 
//        //You can use the following additional attributes as you write your tests:
//        //
//        //Use ClassInitialize to run code before running the first test in the class
//        //[ClassInitialize()]
//        //public static void MyClassInitialize(TestContext testContext)
//        //{
//        //}
//        //
//        //Use ClassCleanup to run code after all tests in a class have run
//        //[ClassCleanup()]
//        //public static void MyClassCleanup()
//        //{
//        //}
//        //
//        //Use TestInitialize to run code before running each test
//        //[TestInitialize()]
//        //public void MyTestInitialize()
//        //{
//        //}
//        //
//        //Use TestCleanup to run code after each test has run
//        //[TestCleanup()]
//        //public void MyTestCleanup()
//        //{
//        //}
//        //
//        #endregion


//        ///// <summary>
//        /////A test for MockRepository`1 Constructor
//        /////</summary>
//        //public void MockRepositoryConstructorTestHelper<T>()
//        //    where T : class
//        //{
//        //    MockRepository<T> target = new MockRepository<T>();
//        //    Assert.Inconclusive("TODO: Implement code to verify target");
//        //}

//        [TestMethod()]
//        public void MockRepositoryTest1()
//        {
//            MockRepository<GenericParameterHelper> target
//                = new MockRepository<GenericParameterHelper>();
            
//            Assert.IsTrue(target.Items.Count() == 20);
//        }

//        [TestMethod()]
//        public void MockRepositoryTest2()
//        {
//            MockRepository<GenericParameterHelper> target
//                = new MockRepository<GenericParameterHelper>();

//            var filtered = target.Items.Where(i => i.Data > 50);

//            Assert.IsTrue(filtered.Count() > 0 && filtered.Count() < 20);
//        }

//    }
//}
