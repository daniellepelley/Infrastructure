using Newton.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test.NewtonFramework
{
    
    
    /// <summary>
    ///This is a test class for DatesTest and is intended
    ///to contain all DatesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class NextPreviousDateTest
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
        ///A test for NextWeekDay
        ///</summary>
        [TestMethod()]
        [Owner("NextWeekDay")]
        public void NextWeekDayTest1()
        {
            DateTime date = new DateTime(2013, 7, 5);
            DateTime expected = new DateTime(2013, 7, 8);
            DateTime actual;
            actual = Dates.NextWeekDay(date);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NextWeekDay
        ///</summary>
        [TestMethod()]
        [Owner("NextWeekDay")]
        public void NextWeekDayTest2()
        {
            DateTime date = new DateTime(2013, 7, 6);
            DateTime expected = new DateTime(2013, 7, 8);
            DateTime actual;
            actual = Dates.NextWeekDay(date);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NextWeekDay
        ///</summary>
        [TestMethod()]
        [Owner("NextWeekDay")]
        public void NextWeekDayTest3()
        {
            DateTime date = new DateTime(2013, 7, 7);
            DateTime expected = new DateTime(2013, 7, 8);
            DateTime actual;
            actual = Dates.NextWeekDay(date);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for NextWeekDay
        ///</summary>
        [TestMethod()]
        [Owner("NextWeekDay")]
        public void NextWeekDayTest4()
        {
            DateTime date = new DateTime(2013, 7, 4);
            DateTime expected = new DateTime(2013, 7, 5);
            DateTime actual;
            actual = Dates.NextWeekDay(date);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddWeekDays
        ///</summary>
        [TestMethod()]
        [Owner("AddWeekDays")]
        public void AddWeekDaysTest1()
        {
            DateTime date = new DateTime(2013, 7, 1);
            int numberOfDays = 5;

            DateTime expected = new DateTime(2013, 7, 8);
            DateTime actual;
            actual = Dates.AddWeekDays(date, numberOfDays);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddWeekDays
        ///</summary>
        [TestMethod()]
        [Owner("AddWeekDays")]
        public void AddWeekDaysTest2()
        {
            DateTime date = new DateTime(2013, 7, 1);
            int numberOfDays = 8;

            DateTime expected = new DateTime(2013, 7, 11);
            DateTime actual;
            actual = Dates.AddWeekDays(date, numberOfDays);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddWeekDays
        ///</summary>
        [TestMethod()]
        [Owner("AddWeekDays")]
        public void AddWeekDaysTest3()
        {
            DateTime date = new DateTime(2013, 7, 3);
            int numberOfDays = 8;

            DateTime expected = new DateTime(2013, 7, 15);
            DateTime actual;
            actual = Dates.AddWeekDays(date, numberOfDays);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddWeekDays
        ///</summary>
        [TestMethod()]
        [Owner("AddWeekDays")]
        public void AddWeekDaysTest4()
        {
            DateTime date = new DateTime(2013, 7, 3);
            int numberOfDays = 7;

            DateTime expected = new DateTime(2013, 7, 12);
            DateTime actual;
            actual = Dates.AddWeekDays(date, numberOfDays);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddWeekDays
        ///</summary>
        [TestMethod()]
        [Owner("AddWeekDays")]
        public void AddWeekDaysTest5()
        {
            DateTime date = new DateTime(2013, 7, 1);
            int numberOfDays = 4;

            DateTime expected = new DateTime(2013, 7, 5);
            DateTime actual;
            actual = Dates.AddWeekDays(date, numberOfDays);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for AddWeekDays
        ///</summary>
        [TestMethod()]
        [Owner("AddWeekDays")]
        public void AddWeekDaysTest6()
        {
            DateTime date = new DateTime(2013, 7, 13);
            int numberOfDays = 9;

            DateTime expected = new DateTime(2013, 7, 26);
            DateTime actual;
            actual = Dates.AddWeekDays(date, numberOfDays);
            Assert.AreEqual(expected, actual);
        }

    }
}
