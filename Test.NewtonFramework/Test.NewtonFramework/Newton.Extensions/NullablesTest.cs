using Newton.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test.NewtonFramework
{


    /// <summary>
    ///This is a test class for NullablesTest and is intended
    ///to contain all NullablesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NullablesTest
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


        [Owner("Extensions.Nullable")]
        [TestMethod()]
        public void NullableConvertTest1()
        {
            DateTime? source = null;
            Func<DateTime, string> converter = d => d.ToString("dd/MM/yy");
            string defaultValue = "None";
            string expected = "None"; ;
            string actual;
            actual = Nullables.Convert<DateTime, string>(source, converter, defaultValue);
            Assert.AreEqual(expected, actual);
        }

        [Owner("Extensions.Nullable")]
        [TestMethod()]
        public void NullableConvertTest2()
        {
            DateTime? source = new DateTime(2013, 6, 1);
            Func<DateTime, string> converter = d => d.ToString("dd/MM/yy");
            string defaultValue = "None";
            string expected = "01/06/13"; ;
            string actual;
            actual = source.Convert(converter, defaultValue);
            Assert.AreEqual(expected, actual);
        }

        [Owner("Extensions.Nullable")]
        [TestMethod()]
        public void NullableConvertTest3()
        {
            DateTime? source = null;
            Func<DateTime, string> converter = d => d.ToString("dd/MM/yy");
            string expected = null;
            string actual;
            actual = Nullables.Convert<DateTime, string>(source, converter);
            Assert.AreEqual(expected, actual);
        }

        [Owner("Extensions.Nullable")]
        [TestMethod()]
        public void NullableConvertTest4()
        {
            DateTime? source = new DateTime(2013, 6, 1);
            string expected = "01/06/13"; ;
            string actual;
            actual = source.Convert(d => d.ToString("dd/MM/yy"), "None");
            Assert.AreEqual(expected, actual);
        }

        [Owner("Extensions.Nullable")]
        [TestMethod()]
        public void NullableToStringTest1()
        {
            DateTime? source = new DateTime(2013, 6, 1);
            string expected = "01/06/13";
            string actual;
            actual = source.ToString("dd/MM/yy", null);
            Assert.AreEqual(expected, actual);
        }

        [Owner("Extensions.Nullable")]
        [TestMethod()]
        public void NullableToStringTest2()
        {
            DateTime? source = null;
            string expected = string.Empty;
            string actual;
            actual = source.ToString("dd/MM/yy", null);
            Assert.AreEqual(expected, actual);
        }

        [Owner("Extensions.Nullable")]
        [TestMethod()]
        public void NullableToStringTest3()
        {
            DateTime? source = new DateTime(2013, 6, 1);
            string expected = "01/06/13";
            string actual;
            actual = source.ToString("dd/MM/yy");
            Assert.AreEqual(expected, actual);
        }

        [Owner("Extensions.Nullable")]
        [TestMethod()]
        public void NullableToStringTest4()
        {
            DateTime? source = null;
            string expected = string.Empty;
            string actual;
            actual = source.ToString("dd/MM/yy");
            Assert.AreEqual(expected, actual);
        }
    }
}