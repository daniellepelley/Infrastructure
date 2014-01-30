using Newton.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Test.NewtonFramework
{
    
    
    /// <summary>
    ///This is a test class for PopulatorTest and is intended
    ///to contain all PopulatorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PopulatorTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Populator Constructor
        ///</summary>
        [TestMethod()]
        public void PopulatorTest1()
        {
            Populator target = new Populator();

            List<TestClassToBePopulated> list = new List<TestClassToBePopulated>();

            for (int i = 0; i < 20; i++)
            {
                list.Add(target.Create<TestClassToBePopulated>());
            }

            Assert.IsTrue(list.Where(p => string.IsNullOrEmpty(p.String1)).Count() > 0);
        }
    }

    public class TestClassToBePopulated
    {
        public string String1 { set; get; }
        public int Int1 { set; get; }

        public override string ToString()
        {
            return string.Format("{0} - {1}", String1, Int1);
        }
    }
}
