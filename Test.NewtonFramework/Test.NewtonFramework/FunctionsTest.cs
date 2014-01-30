using Newton.Factory.LineAllocation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.NewtonFramework
{
    
    
    /// <summary>
    ///This is a test class for FunctionsTest and is intended
    ///to contain all FunctionsTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FunctionsTest
    {
        private List<Worker> workers;
        private List<ProcessStep> processSteps;

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
        [TestInitialize()]
        public void MyTestInitialize()
        {
            workers = new List<Worker>();

            for (int i = 0; i < 20; i++)
            {
                workers.Add(new Worker());
            }

            processSteps = new List<ProcessStep>();

            processSteps.Add(new ProcessStep() { UnitsPerWorkerPerUnitOfTime = 2 });
            processSteps.Add(new ProcessStep() { UnitsPerWorkerPerUnitOfTime = 15 });
            processSteps.Add(new ProcessStep() { UnitsPerWorkerPerUnitOfTime = 25 });
            processSteps.Add(new ProcessStep() { UnitsPerWorkerPerUnitOfTime = 30 });
        }

        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Next
        ///</summary>
        [TestMethod()]
        [Owner("LineAllocation")]
        public void NextTest()
        {
            //IEnumerable<Worker> workers = null; // TODO: Initialize to an appropriate value
            //IEnumerable<ProcessStep> processSteps = null; // TODO: Initialize to an appropriate value
            //ProcessStep expected = null; // TODO: Initialize to an appropriate value
            //ProcessStep actual;


            foreach(var worker in workers)
            {
                worker.ProcessStep = Functions.Next(workers, processSteps);
            }

            var w = Functions.CreateWeightings(workers, processSteps);


            double bottleNeckSpeed = w.Lowest().Value;


            var d = new MathNet.Numerics.Distributions.Normal(100, 1);

            double[] result = d.Samples().Take(100).ToArray();

            //foreach (var d1 in result)
            //    System.Windows.MessageBox.Show(d1.ToString());

            //for (double d2 = 0; d2 < 1; d2 += 0.1)
                //System.Windows.MessageBox.Show(d.InverseCumulativeDistribution(d2).ToString());
            //Assert.AreEqual(expected, actual);

            var p = new MathNet.Numerics.Distributions.Poisson(100);

            int[] result2 = p.Samples().Take(100).ToArray();

            //foreach (var d1 in result)
            //    System.Windows.MessageBox.Show(d1.ToString());

            //for (double d2 = 0; d2 < 1; d2 += 0.1)
            //    System.Windows.MessageBox.Show(p.Probability(90).ToString());


        }
    }
}
