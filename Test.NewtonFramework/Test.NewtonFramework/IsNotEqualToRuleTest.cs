using Newton.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test.NewtonFramework
{
    
    
    /// <summary>
    ///This is a test class for IsNotEqualToRuleTest and is intended
    ///to contain all IsNotEqualToRuleTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IsNotEqualToRuleTest
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


        [TestMethod()]
        [Owner("PresetRules.IsNotEqualTo")]
        public void IsNotEqualToRuleTest1()
        {
            IsNotEqualToRule<string> target = new IsNotEqualToRule<string>();
            string value = "Three";
            string expected = string.Empty;
            string actual;

            target.OtherValues.Add("One");
            target.OtherValues.Add("Two");

            actual = target.Check(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [Owner("PresetRules.IsNotEqualTo")]
        public void IsNotEqualToRuleTest2()
        {
            IsNotEqualToRule<string> target = new IsNotEqualToRule<string>();
            string value = "One";
            string expected = "Already exists";
            string actual;

            target.OtherValues.Add("One");
            target.OtherValues.Add("Two");

            actual = target.Check(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [Owner("PresetRules.IsNotEqualTo")]
        public void IsNotEqualToRuleTest3()
        {
            IsNotEqualToRule<string> target = new IsNotEqualToRule<string>();
            string value = null;
            string expected = string.Empty;
            string actual;

            target.OtherValues.Add("One");
            target.OtherValues.Add("Two");

            actual = target.Check(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [Owner("PresetRules.IsNotEqualTo")]
        public void IsNotEqualToRuleTest4()
        {
            IsNotEqualToRule target = new IsNotEqualToRule();
            string value = "Three";
            string expected = string.Empty;
            string actual;

            target.OtherValues.Add("One");
            target.OtherValues.Add("Two");

            actual = target.Check(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [Owner("PresetRules.IsNotEqualTo")]
        public void IsNotEqualToRuleTest5()
        {
            IsNotEqualToRule target = new IsNotEqualToRule();
            string value = "ONE";
            string expected = "Already exists";
            string actual;

            target.OtherValues.Add("One");
            target.OtherValues.Add("Two");

            actual = target.Check(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [Owner("PresetRules.IsNotEqualTo")]
        public void IsNotEqualToRuleTest6()
        {
            IsNotEqualToRule target = new IsNotEqualToRule();
            target.IsCaseSensative = true;
            string value = "ONE";
            string expected = string.Empty;
            string actual;

            target.OtherValues.Add("One");
            target.OtherValues.Add("Two");

            actual = target.Check(value);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [Owner("PresetRules.IsNotEqualTo")]
        public void IsNotEqualToRuleTest7()
        {
            IsNotEqualToRule target = new IsNotEqualToRule();
            string value = null;
            string expected = string.Empty;
            string actual;

            target.OtherValues.Add("One");
            target.OtherValues.Add("Two");

            actual = target.Check(value);
            Assert.AreEqual(expected, actual);
        }
    }
}
