using Newton.Scheduling.Recursion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;

namespace Test.NewtonFramework
{


    /// <summary>
    ///This is a test class for DailyRecurrenceFrequencyItemTest and is intended
    ///to contain all DailyRecurrenceFrequencyItemTest Unit Tests
    ///</summary>
    [TestClass()]
    public class DailyRecurrenceFrequencyItemTest
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
        ///A test for GetDates
        ///</summary>
        [TestMethod()]
        [Owner("DailyRecurrenceFrequencyItem")]
        public void GetDatesTest1()
        {
            DailyRecurrence target = new DailyRecurrence();
            target.DayType = DayType.WeekDays;
            target.Frequency = 2;

            DateTime startDate = new DateTime(2013, 7, 1);
            int occurrences = 3;
            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 1),
                new DateTime(2013, 7, 3),
                new DateTime(2013, 7, 5) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, occurrences);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for GetDates
        ///</summary>
        [TestMethod()]
        [Owner("DailyRecurrenceFrequencyItem")]
        public void GetDatesTest2()
        {
            DailyRecurrence target = new DailyRecurrence();
            target.DayType = DayType.WeekDays;
            target.Frequency = 3;

            DateTime startDate = new DateTime(2013, 7, 1);
            int occurrences = 3;
            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 1),
                new DateTime(2013, 7, 4),
                new DateTime(2013, 7, 9) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, occurrences);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for GetDates
        ///</summary>
        [TestMethod()]
        [Owner("DailyRecurrenceFrequencyItem")]
        public void GetDatesTest3()
        {
            DailyRecurrence target = new DailyRecurrence();
            target.DayType = DayType.All;
            target.Frequency = 1;

            DateTime startDate = new DateTime(2013, 7, 5);
            int occurrences = 3;
            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 6),
                new DateTime(2013, 7, 7) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, occurrences);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for GetDates
        ///</summary>
        [TestMethod()]
        [Owner("DailyRecurrenceFrequencyItem")]
        public void GetDatesTest4()
        {
            DailyRecurrence target = new DailyRecurrence();
            target.DayType = DayType.BusinessDays;
            target.Frequency = 1;

            DateTime startDate = new DateTime(2013, 7, 5);
            int occurrences = 4;
            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 8),
                new DateTime(2013, 7, 9),
                new DateTime(2013, 7, 10) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, occurrences);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for GetDates
        ///</summary>
        [TestMethod()]
        [Owner("DailyRecurrenceFrequencyItem")]
        public void GetDatesTest5()
        {
            DailyRecurrence target = new DailyRecurrence();
            target.DayType = DayType.WeekDays;
            target.Frequency = 1;

            DateTime startDate = new DateTime(2013, 7, 5);
            DateTime endDate = new DateTime(2013, 7, 9);
            
            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 8),
                new DateTime(2013, 7, 9) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, endDate);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for GetDates
        ///</summary>
        [TestMethod()]
        [Owner("DailyRecurrenceFrequencyItem")]
        public void GetDatesTest6()
        {
            DailyRecurrence target = new DailyRecurrence();
            target.DayType = DayType.WeekDays;
            target.Frequency = 2;

            DateTime startDate = new DateTime(2013, 7, 5);
            DateTime endDate = new DateTime(2013, 7, 9);

            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 9) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, endDate);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for GetDates
        ///</summary>
        [TestMethod()]
        [Owner("DailyRecurrenceFrequencyItem")]
        public void GetDatesTest7()
        {
            DailyRecurrence target = new DailyRecurrence();
            target.DayType = DayType.WeekDays;
            target.Frequency = 2;

            DateTime startDate = new DateTime(2013, 7, 5);
            DateTime endDate = new DateTime(2013, 7, 11);

            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 9),
                new DateTime(2013, 7, 11)};

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, endDate);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for GetDates
        ///</summary>
        [TestMethod()]
        [Owner("DailyRecurrenceFrequencyItem")]
        public void GetDatesTest8()
        {
            DailyRecurrence target = new DailyRecurrence();
            target.DayType = DayType.All;
            target.Frequency = 1;

            DateTime startDate = new DateTime(2013, 7, 5);
            DateTime endDate = new DateTime(2013, 7, 11);

            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 6),
                new DateTime(2013, 7, 7),
                new DateTime(2013, 7, 8),
                new DateTime(2013, 7, 9),
                new DateTime(2013, 7, 10),
                new DateTime(2013, 7, 11)};

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, endDate);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

    }
}