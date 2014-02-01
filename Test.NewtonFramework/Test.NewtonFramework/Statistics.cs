using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using global::Newton.Extensions;

namespace Test.NewtonFramework
{
    /// <summary>
    ///This is a test class for DataTablesTest and is intended
    ///to contain all DataTablesTest Unit Tests
    ///</summary>
    [TestClass()]
    public class StatisticsTest
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

        private double[] sample1 = new double[] { 0, 48, 49, 51, 52, 100 };
        private double[] sample2 = new double[] { 0, 1, 1, 99, 99, 100 };
        private double[] sample3 = new double[] { 1, 3, 4, 8, 6, 9, 3, 4, 1, 2, 5 };
        private double[] sample4 = new double[] { 1, 3, 4, 6, 8, 9 };
        private double[] sample5 = new double[] { 7.2, 6.4, 7.2, 8.0, 8.2 };
        private double[] sample6 = new double[] { 0.8, 1.7, 0.5, 0.6, 0.8, 0.8, 0.8, 0.7, 1.3, 1.0 };

        private double[] sample7 = new double[] { 4, 9, 12, 13, 14, 15, 15, 15, 16, 16, 16, 17, 18, 20 };

        /// <summary>
        ///A test for the mean
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsMeanTest1()
        {
            double? expected = 50;
            double? actual = sample1.Mean();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the mean
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsMeanTest2()
        {
            double? expected = 50;
            double? actual = sample2.Mean();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the mean
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsMeanTest3()
        {
            double? expected = 4.18;
            double? actual = System.Math.Round(sample3.Mean().Value ,2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the median
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsMedianTest1()
        {
            double? expected = 50;
            double? actual = sample1.Median();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the median
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsMedianTest2()
        {
            double? expected = 50;
            double? actual = sample2.Median();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the median
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsMedianTest3()
        {
            double expected = 50;
            double actual = sample1.GetSigma(10, 5);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the median
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsMedianTest4()
        {
            double expected = 50;
            double actual = sample2.GetSigma(10, 5);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the CyhelskySkew
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsCyhelskySkewTest1()
        {
            double expected = 0;
            double actual = sample1.CyhelskySkew();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the CyhelskySkew
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsCyhelskySkewTest2()
        {
            double expected = 0.4;
            double actual = sample6.CyhelskySkew();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the CyhelskySkew
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsCyhelskySkewTest3()
        {
            double expected = 0.27;
            double actual = System.Math.Round(sample3.CyhelskySkew(), 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the variance
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsVarianceTest1()
        {
            double? expected = 1002;
            double? actual = sample1.Variance();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the variance
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsVarianceTest2()
        {
            double? expected = 2920.8;
            double? actual = sample2.Variance();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the variance
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsLowerControl3SigmaTest1()
        {
            double expected = 28.4;
            double actual = Newton.Extensions.Statistics.LowerControlLimit3Sigma(32, 3.79, 10);
            Assert.AreEqual(expected, Math.Round(actual, 1));
        }

        /// <summary>
        ///A test for the variance
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsUpperControl3SigmaTest1()
        {
            double expected = 35.6;
            double actual = Newton.Extensions.Statistics.UpperControlLimit3Sigma(32, 3.79, 10);
            Assert.AreEqual(expected, Math.Round(actual, 1));
        }

        /// <summary>
        ///A test for the Chebysheff Ratio
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsChebysheffRatioTest1()
        {
            double expected = .75;
            double value = 2;
            double actual = value.ChebysheffRatio();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Chebysheff Ratio
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsChebysheffRatioTest2()
        {
            double expected = .889;
            double value = 3;
            double actual = Math.Round(value.ChebysheffRatio(), 3);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Chebysheff Ratio
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsChebysheffRatioTest3()
        {
            double expected = 2;
            double actual = Math.Round(expected.ChebysheffRatio().ReserveChebysheffRatio(), 3);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Chebysheff Ratio
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsChebysheffRatioTest4()
        {
            double expected = .95;
            double ratio = Math.Sqrt(expected.ReserveChebysheffRatio());

            double actual = Math.Round(expected.ReserveChebysheffRatio().ChebysheffRatio(), 3);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Lower Quartile
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsLowerQuartileTest1()
        {
            double expected = 2;
            double actual = sample3.LowerQuartile();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Lower Quartile
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsLowerQuartileTest2()
        {
            double expected = 2.5;
            double actual = sample4.LowerQuartile();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Percentile
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPercentileTest1()
        {
            double expected = 2.5;
            double actual = sample4.Percentile(25);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Percentile
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPercentileTest2()
        {
            double expected = 1;
            double actual = sample4.Percentile(10);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Percentile
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPercentileTrimTest1()
        {
            double expected = 6;
            double actual = sample4.PercentileTrim(10).Count();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Percentile
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPercentileTrimTest2()
        {
            double expected = 4;
            double actual = sample4.PercentileTrim(20).Count();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Lower Quartile
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsUpperQuartileTest1()
        {
            double expected = 6;
            double actual = sample3.UpperQuartile();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Lower Quartile
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsUpperQuartileTest2()
        {
            double expected = 8.25;
            double actual = sample4.UpperQuartile();
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Poisson Probability
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPoissonProbabilityTest1()
        {
            double expected = 0.033690;
            double actual = Math.Round(Statistics.PoissonProbability(5, 1), 6);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Poisson Probability
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPoissonProbabilityTest2()
        {
            double expected = 0.135335;
            double actual = Math.Round(Statistics.PoissonProbability(2, 0), 6);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Poisson Probability
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPoissonProbabilityTest3()
        {
            double expected = 0.270671;
            double actual = Math.Round(Statistics.PoissonProbability(2, 1), 6);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Poisson Probability
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPoissonProbabilityTest4()
        {
            double expected = 0.270671;
            double actual = Math.Round(Statistics.PoissonProbability(2, 2), 6);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Poisson Probability
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPoissonProbabilityTest5()
        {
            double expected = 0.442175;
            double actual = Math.Round(Statistics.PoissonProbabilityMoreThan(1.5, 1), 6);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Poisson Probability
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPoissonProbabilityTest6()
        {
            double expected = 0.323324;
            double actual = Math.Round(Statistics.PoissonProbabilityMoreThan(2, 2), 6);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Outcomes
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsOutcomeTest1()
        {
            int expected = 56;
            int actual = Statistics.BiNominalOutcomes(8, 5);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Outcomes
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsOutcomeTest2()
        {
            int expected = 330;
            int actual = Statistics.BiNominalOutcomes(11, 7);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Outcomes
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsNormalDistributionTest1()
        {
            double expected = Statistics.NormalDistribution(20, 4, 19);
            double actual = Statistics.NormalDistribution(20, 4, 21);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the BiNomial
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsBiNomialTest1()
        {
            double expected = 0.189;
            double actual = Statistics.BiNominalProbability(0.3, 3, 2);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the BiNomial
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsBiNomialTest2()
        {
            double expected = 0.0036;
            double actual = Math.Round(Statistics.BiNominalProbability(0.1, 4, 3), 4);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the BiNomial
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsBiNomialTest3()
        {
            //45% of voters are Conservative. What is the probability that 2 or fewer
            //out of 8 vote Conservative?
            double expected = 0.2201;
            double actual = Math.Round(Statistics.BiNominalProbabilityLessThan(0.45, 8, 3), 4);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the BiNomial
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsEstimateStandardErrorTest1()
        {
            double expected = 0.52;

            double s = PowSum(sample5);

            double actual =
                ((s
                - (System.Math.Pow(sample5.Sum(), 2)
                / sample5.Count()))
                / 4);
            
            Assert.AreEqual(expected, Math.Round(actual, 4));
        }

        /// <summary>
        ///A test for the BiNomial
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsSummaryTest1()
        {
            bool meanTest = sample6.Mean() == .9;
            bool stdErrorTest = System.Math.Round(sample6.StandardError().Value, 5) == .11255;
            bool medianTest = sample6.Median() == .8;
            bool varianceTest = System.Math.Round(sample6.Variance().Value, 3) == .127;
            bool stdDeviation = System.Math.Round(sample6.StandardDeviation().Value, 5) == .35590;
            bool minimumTest = sample6.Min() == .5;
            bool maximumTest = sample6.Max() == 1.70;
            bool rangeTest = sample6.Range() == 1.20;

            double? skew = sample6.Skew();

            bool skewness = sample6.CyhelskySkew() == 1.48;
            double interquartileRange = sample6.UpperQuartile() - sample6.LowerQuartile();


            Assert.IsTrue(
                meanTest &&
                stdErrorTest &&
                medianTest &&
                varianceTest &&
                stdDeviation &&
                minimumTest &&
                maximumTest
                );
        }

        /// <summary>
        ///A test for the BiNomial
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsExponentialDistributionTest1()
        {
            double expected = 0.0002249;
            double actual = Math.Round(Statistics.ExponentialDistribution(2.1, 4), 7);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the BiNomial
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsExponentialDistributionTest2()
        {
            double expected = 0.362372;
            double actual = 1 - Math.Round(Statistics.ExponentialDistribution(0.9, 0.5), 6);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for the Pascals Triangle
        ///</summary>
        [TestMethod()]
        [Owner("Statistics")]
        public void StatisticsPascalsTriangleTest1()
        {
            double[] sample = sample7;

            double[] pascalsBell = Statistics.CreatePascalsTriangleFromVolume(8, sample.Length);
            double[] plutoBell = sample.BellCurveX(100, 2).RollingMeans(2).RollingMeans(2);

            double[] plutoBell2 = Statistics.BellCurveX(sample.PercentileTrim(20), 8);

            double error = 0;
            for (int i = 0; i < 7; i++)

            {
                error += Math.Abs(plutoBell[i] - pascalsBell[i]);
            }
             
            double totalError = error / sample.Length;

        }


        private double PowSum(double[] values)
        {
            double result = 0;

            foreach (double value in values)
                result += System.Math.Pow(value, 2);

            return result;
        }





        //[TestMethod]
        public void MortTest1()
        {
            double interestRate = 3.99;
            double years = 2;
            //double principal = 201875;

            double principal = 200000;

            double repayment = 1066.57;

            repayment = 1500;

            double saving = 0;

            double movingPricipal = principal;

            for (int i = 0; i < years * 12; i++)
            {
                double dailyInterest = Math.Pow(1 + (interestRate / 100), (double)1 / 365);

                double dailyrepayment = (dailyInterest - 1) * movingPricipal;

                movingPricipal = (Math.Pow(dailyInterest, 30.5)) * movingPricipal;
                movingPricipal = movingPricipal - repayment;

                if (movingPricipal < 0)
                {
                    double y = (double)i / 12;
                    saving = ((years * 12) - i) * repayment;

                }
            }

            double repayed = repayment * 12 * years;

            double offPrincipal = principal - movingPricipal;

            double costs = repayed - offPrincipal;

            double repaymentToOutgoings = (offPrincipal / repayed) * 100;
            double perpound = 100 / repaymentToOutgoings;


        }
      


    }
}
