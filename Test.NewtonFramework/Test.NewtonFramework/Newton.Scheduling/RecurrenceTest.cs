using Newton.Scheduling.Recursion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Test.NewtonFramework
{
    
    
    /// <summary>
    ///This is a test class for DatesTest and is intended
    ///to contain all DatesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class RecurrenceTest
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
        ///A test for AllDatesMatchingDayOfWeek
        ///</summary>
        [TestMethod()]
        [Owner("WeeklyRecurrenceFrequencyItem")]
        public void AllDatesMatchingDayOfWeekTest1()
        {
            WeeklyRecurrenceFrequencyItem target = new WeeklyRecurrenceFrequencyItem();
            DateTime startDate = new DateTime(2013, 7, 1);
            DateTime endDate = new DateTime(2013, 7, 14);

            target.DayOfWeeks.AddRange(new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Wednesday });

            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 1),
                new DateTime(2013, 7, 3),
                new DateTime(2013, 7, 8),
                new DateTime(2013, 7, 10) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, endDate);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }


        /// <summary>
        ///A test for AllDatesMatchingDayOfWeek
        ///</summary>
        [TestMethod()]
        [Owner("WeeklyRecurrenceFrequencyItem")]
        public void AllDatesMatchingDayOfWeekTest2()
        {
            WeeklyRecurrenceFrequencyItem target = new WeeklyRecurrenceFrequencyItem();
            DateTime startDate = new DateTime(2013, 7, 3);
            DateTime endDate = new DateTime(2013, 7, 14);

            target.DayOfWeeks.AddRange(new DayOfWeek[] { DayOfWeek.Monday, DayOfWeek.Wednesday });


            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 3),
                new DateTime(2013, 7, 8),
                new DateTime(2013, 7, 10) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, endDate);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for AllDatesMatchingDayOfWeek
        ///</summary>
        [TestMethod()]
        [Owner("WeeklyRecurrenceFrequencyItem")]
        public void AllDatesMatchingDayOfWeekTest3()
        {
            WeeklyRecurrenceFrequencyItem target = new WeeklyRecurrenceFrequencyItem();
            DateTime startDate = new DateTime(2013, 7, 3);
            DateTime endDate = new DateTime(2013, 7, 14);

            target.DayOfWeeks.AddRange(new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Friday, DayOfWeek.Wednesday });

            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 3),
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 6),
                new DateTime(2013, 7, 10),
                new DateTime(2013, 7, 12),
                new DateTime(2013, 7, 13) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, endDate);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for AllDatesMatchingDayOfWeek
        ///</summary>
        [TestMethod()]
        [Owner("WeeklyRecurrenceFrequencyItem")]
        public void AllDatesMatchingDayOfWeekTest4()
        {
            WeeklyRecurrenceFrequencyItem target = new WeeklyRecurrenceFrequencyItem();
            DateTime startDate = new DateTime(2013, 7, 3);
            int occurrences = 5;

            target.DayOfWeeks.AddRange(new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Friday, DayOfWeek.Wednesday });

            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 3),
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 6),
                new DateTime(2013, 7, 10),
                new DateTime(2013, 7, 12) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, occurrences);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for AllDatesMatchingDayOfWeek
        ///</summary>
        [TestMethod()]
        [Owner("WeeklyRecurrenceFrequencyItem")]
        public void AllDatesMatchingDayOfWeekTest5()
        {
            WeeklyRecurrenceFrequencyItem target = new WeeklyRecurrenceFrequencyItem();
            DateTime startDate = new DateTime(2013, 7, 1);
            int occurrences = 29;

            target.DayOfWeeks.AddRange(new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Friday, DayOfWeek.Wednesday });

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, occurrences);
            Assert.AreEqual(actual.Count(), occurrences);
        }

        /// <summary>
        ///A test for AllDatesMatchingDayOfWeek
        ///</summary>
        [TestMethod()]
        [Owner("WeeklyRecurrenceFrequencyItem")]
        public void AllDatesMatchingDayOfWeekTest6()
        {
            WeeklyRecurrenceFrequencyItem target = new WeeklyRecurrenceFrequencyItem();
            DateTime startDate = new DateTime(2013, 7, 3);
            int occurrences = 5;
            target.Frequency = 2;
            target.DayOfWeeks.AddRange(new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Friday, DayOfWeek.Wednesday });

            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 3),
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 6),
                new DateTime(2013, 7, 17),
                new DateTime(2013, 7, 19) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, occurrences);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }

        /// <summary>
        ///A test for AllDatesMatchingDayOfWeek
        ///</summary>
        [TestMethod()]
        [Owner("WeeklyRecurrenceFrequencyItem")]
        public void AllDatesMatchingDayOfWeekTest7()
        {
            WeeklyRecurrenceFrequencyItem target = new WeeklyRecurrenceFrequencyItem();
            DateTime startDate = new DateTime(2013, 7, 3);
            int occurrences = 5;
            target.Frequency = 4;
            target.DayOfWeeks.AddRange(new DayOfWeek[] { DayOfWeek.Saturday, DayOfWeek.Friday, DayOfWeek.Wednesday });

            IEnumerable<DateTime> expected = new DateTime[] {
                new DateTime(2013, 7, 3),
                new DateTime(2013, 7, 5),
                new DateTime(2013, 7, 6),
                new DateTime(2013, 7, 31),
                new DateTime(2013, 8, 2) };

            IEnumerable<DateTime> actual;
            actual = target.GetDates(startDate, occurrences);
            CollectionAssert.AreEquivalent((System.Collections.ICollection)expected, (System.Collections.ICollection)actual);
        }
    }
}
