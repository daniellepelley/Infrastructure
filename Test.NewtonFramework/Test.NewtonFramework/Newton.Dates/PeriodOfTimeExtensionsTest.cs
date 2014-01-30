using Newton.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Newton.Dates;
using System.Collections.Generic;
using System.Linq;

namespace Test.NewtonFramework
{
    
    
    /// <summary>
    ///This is a test class for PeriodOfTimeExtensionsTest and is intended
    ///to contain all PeriodOfTimeExtensionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PeriodOfTimeExtensionsTest
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


        public IPeriodOfTime PeriodOfTime1()
        {
            PeriodOfTime periodOfTime = new PeriodOfTime();
            periodOfTime.Start = new DateTime(2013, 7, 1, 9, 0, 0);
            periodOfTime.End = new DateTime(2013, 7, 1, 10, 0, 0);
            return periodOfTime;
        }

        public IPeriodOfTime PeriodOfTime2()
        {
            PeriodOfTime periodOfTime = new PeriodOfTime();
            periodOfTime.Start = new DateTime(2013, 7, 1, 9, 0, 0);
            periodOfTime.End = new DateTime(2013, 7, 2, 9, 0, 0);
            return periodOfTime;
        }

        public IPeriodOfTime PeriodOfTime3()
        {
            PeriodOfTime periodOfTime = new PeriodOfTime();
            periodOfTime.Start = new DateTime(2013, 7, 2, 11, 0, 0);
            periodOfTime.End = new DateTime(2013, 7, 2, 12, 0, 0);
            return periodOfTime;
        }

        public IPeriodOfTime PeriodOfTime4()
        {
            PeriodOfTime periodOfTime = new PeriodOfTime();
            PeriodOfTimePlugIn<IPeriodOfTime> plugIn = new PeriodOfTimePlugIn<IPeriodOfTime>(
                periodOfTime,
                (p, d) => p.Start = d,
                (p) => p.Start,
                (p, d) => p.End = d,
                (p) => p.End);

            plugIn.Start = new DateTime(2013, 7, 1, 10, 0, 0);
            plugIn.End = new DateTime(2013, 7, 1, 12, 0, 0);
            return plugIn;
        }

        /// <summary>
        ///A test for to see if two periods of time conflicts
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_Conflicts1()
        {
            List<IPeriodOfTime> times1 = new List<IPeriodOfTime>();
            times1.Add(PeriodOfTime1());

            List<IPeriodOfTime> times2 = new List<IPeriodOfTime>();
            times2.Add(PeriodOfTime2());

            var result = times1.Conflicts(times2);

            int expected = 1;
            int actual = result.Count();
           
            Assert.AreEqual(expected, actual);
            Assert.AreEqual(result.FirstOrDefault().Item1, times1.FirstOrDefault());
            Assert.AreEqual(result.FirstOrDefault().Item2, times2.FirstOrDefault());
        }

        /// <summary>
        ///A test for to see if two periods of time conflicts
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_Conflicts2()
        {
            List<IPeriodOfTime> times1 = new List<IPeriodOfTime>();
            times1.Add(PeriodOfTime2());

            List<IPeriodOfTime> times2 = new List<IPeriodOfTime>();
            times2.Add(PeriodOfTime4());

            var result = times1.Conflicts(times2);

            int expected = 1;
            int actual = result.Count();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(result.FirstOrDefault().Item1, times1.FirstOrDefault());
            Assert.AreEqual(result.FirstOrDefault().Item2, times2.FirstOrDefault());
        }

        /// <summary>
        ///A test for to see if two periods of time conflicts
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_Conflicts3()
        {
            List<IPeriodOfTime> times1 = new List<IPeriodOfTime>();
            times1.Add(PeriodOfTime1());

            List<IPeriodOfTime> times2 = new List<IPeriodOfTime>();
            times2.Add(PeriodOfTime4());

            var result = times1.Conflicts(times2);

            int expected = 0;
            int actual = result.Count();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for to see if two periods of time conflicts
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_Conflicts4()
        {
            List<IPeriodOfTime> times1 = new List<IPeriodOfTime>();
            times1.Add(PeriodOfTime4());

            List<IPeriodOfTime> times2 = new List<IPeriodOfTime>();
            times2.Add(PeriodOfTime1());

            var result = times1.Conflicts(times2);

            int expected = 0;
            int actual = result.Count();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for to see if two periods of time conflicts
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_Conflicts5()
        {
            List<IPeriodOfTime> times1 = new List<IPeriodOfTime>();
            times1.Add(PeriodOfTime1());
            times1.Add(PeriodOfTime2());
            times1.Add(PeriodOfTime3());
            times1.Add(PeriodOfTime4());

            List<IPeriodOfTime> times2 = new List<IPeriodOfTime>();
            times2.Add(PeriodOfTime1());
            times2.Add(PeriodOfTime2());
            times2.Add(PeriodOfTime3());
            times2.Add(PeriodOfTime4());

            var result = times1.Conflicts(times2);

            int expected = 8;
            int actual = result.Count();

            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for to see if a date is within a period of time
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_IsWithin1()
        {
            IPeriodOfTime source = PeriodOfTime1();
            bool expected = true;
            bool actual = source.IsWithin(new DateTime(2013, 7, 1, 9, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for to see if a date is within a period of time
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_IsWithin2()
        {
            IPeriodOfTime source = PeriodOfTime1();
            bool expected = false;
            bool actual = source.IsWithin(new DateTime(2013, 7, 1, 10, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for to see if a date is within a period of time
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_IsWithin3()
        {
            IPeriodOfTime source = PeriodOfTime1();
            bool expected = true;
            bool actual = source.IsWithin(new DateTime(2013, 7, 1, 9, 0, 1));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for to see if a date is within a period of time
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_IsWithin4()
        {
            IPeriodOfTime source = PeriodOfTime1();
            bool expected = false;
            bool actual = source.IsWithin(new DateTime(2013, 7, 1, 8, 59, 59));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for to see if a date is within a period of time
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_IsWithin5()
        {
            IPeriodOfTime source = PeriodOfTime2();
            bool expected = false;
            bool actual = source.IsWithin(new DateTime(2013, 7, 2, 9, 0, 0));
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for to see if a date is within a period of time
        ///</summary>
        [Owner("PeriodOfTime")]
        [TestMethod()]
        public void PeriodOfTime_IsWithin6()
        {
            IPeriodOfTime source = PeriodOfTime2();
            bool expected = true;
            bool actual = source.IsWithin(new DateTime(2013, 7, 2, 8, 59, 59));
            Assert.AreEqual(expected, actual);
        }
    }
}
