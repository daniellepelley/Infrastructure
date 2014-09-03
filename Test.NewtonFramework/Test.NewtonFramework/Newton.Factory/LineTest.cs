using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newton.Factory;

namespace Newton.Factory.Test
{
    [TestClass]
    public class LineTest
    {
        private Line line = new Line();

        private TimeSpanRate hoursPerWeek = new TimeSpanRate()
        {
            Quantity = new TimeSpan(0, 40, 0, 0),
            TimeSpan = new TimeSpan(7, 0, 0, 0)
        };

        private Rate potentialRate = new Rate()
        {
            Quantity = 50,
            TimeSpan = new TimeSpan(0, 1, 0)
        };

        private Rate actualRate = new Rate()
        {
            Quantity = 40,
            TimeSpan = new TimeSpan(0, 1, 0)
        };


        [TestInitialize()]
        public void Start()
        {
            line.Machines.Add(new Machine()
            {
                ActualSpeed
                    = new Rate()
                    {
                        TimeSpan = new TimeSpan(0, 1, 0),
                        Quantity = 100
                    },
                PotentialSpeed
                    = new Rate()
                    {
                        TimeSpan = new TimeSpan(0, 1, 0),
                        Quantity = 90
                    }
            });

            line.Machines.Add(new Machine()
            {
                ActualSpeed
                    = new Rate()
                    {
                        TimeSpan = new TimeSpan(0, 2, 0),
                        Quantity = 120
                    },
                PotentialSpeed
                    = new Rate()
                    {
                        TimeSpan = new TimeSpan(0, 3, 0),
                        Quantity = 80
                    }
            });

        }


        [TestMethod]
        [Owner("Factory")]
        public void TestLineBottleNeckSpeed()
        {
            double expected = 120;
            double actual = line.BottleNeckSpeed.Quantity;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [Owner("Factory")]
        public void TestPotentialSpeed()
        {
            double expected = 80;
            double actual = line.PotentialSpeed.Quantity;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [Owner("Factory")]
        public void TestEffeciency()
        {
            double expected = 0.9;
            double actual = ((Machine)line.Machines[0]).Efficency;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [Owner("Factory")]
        public void TestGetRatio1()
        {
            var adjustedRate = line.Machines[0].ActualSpeed.IncreaseByRatio(1);


            Assert.IsTrue(
                adjustedRate.TimeSpan == line.Machines[0].ActualSpeed.TimeSpan &&
                adjustedRate.Quantity == line.Machines[0].ActualSpeed.Quantity

                );
        }

        [TestMethod]
        [Owner("Factory")]
        public void TestGetRatio2()
        {
            double ratio = 0.5;

            var adjustedRate = line.Machines[0].ActualSpeed.IncreaseByRatio(ratio);

            Assert.IsTrue(
                adjustedRate.TimeSpan.TotalMilliseconds * ratio == line.Machines[0].ActualSpeed.TimeSpan.TotalMilliseconds &&
                adjustedRate.Quantity == line.Machines[0].ActualSpeed.Quantity
                );
        }

        [TestMethod]
        [Owner("Factory")]
        public void TestGetRatio3()
        {
            double ratio = 1.34;

            var adjustedRate = line.Machines[0].ActualSpeed.IncreaseByRatio(ratio);

            Assert.IsTrue(
                Math.Round(adjustedRate.TimeSpan.TotalMilliseconds * ratio, 0) == Math.Round(line.Machines[0].ActualSpeed.TimeSpan.TotalMilliseconds, 0) &&
                adjustedRate.Quantity == line.Machines[0].ActualSpeed.Quantity
                );
        }

        [TestMethod]
        [Owner("Factory")]
        public void TestGetQuantity()
        {
            double ratio = 0.5;

            var adjustedRate = line.Machines[0].ActualSpeed.IncreaseByRatio(ratio);

            Assert.IsTrue(
                adjustedRate.TimeSpan.TotalMilliseconds * ratio == line.Machines[0].ActualSpeed.TimeSpan.TotalMilliseconds &&
                adjustedRate.Quantity == line.Machines[0].ActualSpeed.Quantity
                );
        }

        [TestMethod]
        [Owner("Factory")]
        public void TestGetTimeForQuantity()
        {
            double expected = 30;
            double actual = line.Machines[0].ActualSpeed.GetTimeSpanForQuantity(50).TotalSeconds;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [Owner("Factory")]
        public void TestGetQuantityForTime()
        {
            double expected = 50;
            double actual = line.Machines[0].ActualSpeed.GetQuantityForTimeSpan(new TimeSpan(0,0,30));

            Assert.AreEqual(actual, expected);
        }


        [TestMethod]
        [Owner("Factory")]
        public void TestGetRateForTimeSpan1()
        {
            //double expected = 120;

            //Rate testRate = new Rate()
            //{
            //    TimeSpan = new TimeSpan(0, 1, 0),
            //    Quantity = 2
            //};

            //double actual = testRate.GetRateForTimeSpan(new TimeSpan(1, 0, 0)).Quantity;

            //Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [Owner("Factory")]
        public void TimeSpanFromHoursTest()
        {
            var t = Functions.TimeSpanFromHours(2.502f);
        }

        [TestMethod]
        [Owner("Factory")]
        public void GetOutputFromHoursTest()
        {
            var rate = new Rate() { Quantity = 100, TimeSpan = new TimeSpan(1, 0, 0) };

            var t = Functions.GetOutputFromHours(200f, rate);

            Assert.AreEqual(20000, t);
        }

        [TestMethod]
        [Owner("Factory")]
        public void LabourPotentialCostTest()
        {
            LabourPotential labourPotential = new LabourPotential(10, 8);

            double actualCost = labourPotential.PotentialCost(
                new Rate() {
                    Quantity = 10,
                    TimeSpan = new TimeSpan(1, 0, 0) },
                new TimeSpan(5, 0, 0));

            Assert.AreEqual(400, actualCost);
        }

        [TestMethod]
        [Owner("Factory")]
        public void LabourActualCostTest()
        {
            LabourPotential labourPotential = new LabourPotential(10, 8);

            double actualCost = labourPotential.ActualCost(
                new Rate()
                {
                    Quantity = 10,
                    TimeSpan = new TimeSpan(1, 0, 0)
                },
                new TimeSpan(5, 0, 0));

            Assert.AreEqual(500, actualCost);
        }

        [TestMethod]
        [Owner("Factory")]
        public void LabourPotentialActualCostTest1()
        {
            LabourPotential labourPotential = new LabourPotential(10, 8);

            double actualCost = labourPotential.PotentialSaving(
                new Rate()
                {
                    Quantity = 10,
                    TimeSpan = new TimeSpan(1, 0, 0)
                },
                new TimeSpan(5, 0, 0));

            Assert.AreEqual(100, actualCost);
        }

        [TestMethod]
        [Owner("Factory")]
        public void LabourPotentialActualCostTest2()
        {
            LabourPotential labourPotential = new LabourPotential(9.32, 8.12);

            double actualCost = labourPotential.PotentialSaving(
                new Rate()
                {
                    Quantity = 8.36,
                    TimeSpan = new TimeSpan(1, 0, 0)
                },
                new TimeSpan(8, 0, 0));

            Assert.AreEqual(80.26, Math.Round(actualCost, 2));
        }

        [TestMethod]
        [Owner("Factory")]
        public void WholeLineAssessmentPerHour()
        {
            //5 minutes of downtime per hour
            Downtime downTime = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 5, 0),
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //100 units of waste per hour
            Waste waste = new Waste()
            {
                Rate = new Rate()
                {
                    Quantity = 100,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };



            double opportunity = Opportunity(
                downTime.Rate,
                waste.Rate,
                potentialRate,
                actualRate,
                new TimeSpan(1, 0, 0),
                0.1);


            //10 pence per unit
            //We could make 3000 units per hour

            //We actually only have 55 minutes due to downtime
            //55 x 40 == 2,200 units
            //Waste == 2,200 - 100 == 2,100

            //Potential gain is 900 units at 0.10

            // £90 / hour

            double expected = 90;

            Assert.AreEqual(expected, opportunity);
        }


        [TestMethod]
        [Owner("Factory")]
        public void UnitConversionTest1()
        {
            UnitConversion unitConversion = new UnitConversion()
            {
                Unit1 = "Roll",
                Unit2 = "Case",
                Ratio = 1F / 36F
            };

            double actual = Math.Round(unitConversion.ConvertToUnit2(36), 2);
            double expected = 1;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [Owner("Factory")]
        public void UnitConversionTest2()
        {
            UnitConversion unitConversion = new UnitConversion()
            {
                Unit1 = "Roll",
                Unit2 = "Case",
                Ratio = 1F / 36F
            };

            double actual = Math.Round(unitConversion.ConvertToUnit1(1), 2);
            double expected = 36;

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        [Owner("Factory")]
        public void WholeLineAssessmentPerYear()
        {
            //5 minutes of downtime per hour
            Downtime downTime = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 5, 0),
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //100 units of waste per hour
            Waste waste = new Waste()
            {
                Rate = new Rate()
                {
                    Quantity = 100,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            Rate potentialRate = new Rate()
            {
                Quantity = 50,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            Rate actualRate = new Rate()
            {
                Quantity = 40,
                TimeSpan = new TimeSpan(0, 1, 0)
            };



            TimeSpan hoursPerYear = hoursPerWeek.GetQuantityForTimeSpan(new TimeSpan(365, 6, 0, 0, 0));

            double opportunity = Opportunity(
                downTime.Rate,
                waste.Rate,
                potentialRate,
                actualRate,
                hoursPerYear,
                0.1);

            //10 pence per unit
            //We could make 3000 units per hour

            //We actually only have 55 minutes due to downtime
            //55 x 40 == 2,200 units
            //Waste == 2,200 - 100 == 2,100

            //Potential gain is 900 units at 0.10

            // £90 / hour

            //Hours per year
            //2080 hours per year

            //£187,200 opportunity per year

            double expected = 188; // 188K

            Assert.AreEqual(expected, Math.Round(opportunity / 1000));
        }

        public double Opportunity(TimeSpanRate lostTimeRate,
                          Rate lostUnitsRate,
                          Rate potentialRate,
                          Rate actualRate,
                          TimeSpan timeSpan,
                          double valuePerUnit)
        {
            double potentialUnits = potentialRate.GetQuantityForTimeSpan(timeSpan);

            double actualUnits = actualRate.GetQuantityForTimeSpan(timeSpan) -
                                 lostTimeRate.ToRate(potentialRate).GetQuantityForTimeSpan(timeSpan) -
                                 lostUnitsRate.GetQuantityForTimeSpan(timeSpan);

            return (potentialUnits - actualUnits) * valuePerUnit;
        }



        [TestMethod]
        [Owner("Factory")]
        public void LineOpportunityPerYear()
        {
            LineOpportunity lineOpportunity = CreateLineOpportunity();

            //10 pence per unit
            //We could make 3000 units per hour

            //We actually only have 55 minutes due to downtime
            //55 x 40 == 2,200 units
            //Waste == 2,200 - 100 == 2,100

            //Potential gain is 900 units at 0.10

            // £90 / hour

            //Hours per year
            //2080 hours per year

            //£187,200 opportunity per year

            double expected = 188; // 188K
            double actual = lineOpportunity.TotalValue();


            Assert.AreEqual(expected, Math.Round(actual / 1000));
        }


        [TestMethod]
        [Owner("Factory")]
        public void CreatePareto()
        {
            LineOpportunity lineOpportunity = CreateLineOpportunity();
            lineOpportunity.TimeSpan = new TimeSpan(1, 0, 0);

            var dictionary = lineOpportunity.TimeSpanPareto();

            Assert.AreEqual(6, dictionary.Count);
        }


        [TestMethod]
        [Owner("Factory")]
        public void CreateParetoFullDowntime()
        {
            LineOpportunity lineOpportunity = CreateLineOpportunityFullDowntime();
            lineOpportunity.TimeSpan = new TimeSpan(1, 0, 0);
            
            var dictionary = lineOpportunity.TimeSpanPareto();

            Assert.AreEqual(6, dictionary.Count);
        }

        [TestMethod]
        [Owner("Factory")]
        public void CreatePareto80PercentDowntime()
        {
            LineOpportunity lineOpportunity = CreateLineOpportunity80PercentDowntime();
            lineOpportunity.TimeSpan = new TimeSpan(1, 0, 0);

            var dictionary = lineOpportunity.TimeSpanPareto();

            Assert.AreEqual(6, dictionary.Count);
        }

        [TestMethod]
        [Owner("Factory")]
        public void CreatePareto80PercentDowntime80PercentWaste()
        {
            LineOpportunity lineOpportunity = CreateLineOpportunity80PercentDowntime80PercentageWaste();
            lineOpportunity.TimeSpan = new TimeSpan(1, 0, 0);

            var dictionary = lineOpportunity.TimeSpanPareto();

            Assert.AreEqual(6, dictionary.Count);
        }

        public LineOpportunity CreateLineOpportunity()
        {
            //2 minutes of downtime per hour
            IDowntime downTime1 = new Downtime()
            {
                Title = "Stoppages",
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 2, 0),
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //3 minutes of downtime per hour
            IDowntime downTime2 = new Downtime()
            {
                Title = "Repair",
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 3, 0),
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //5 minutes of downtime per hour
            IDowntime downTime = new DowntimeComposite()
            {
                Downtimes = new IDowntime[] { downTime1, downTime2 }.ToList(),
                TimeSpan = new TimeSpan(1, 0, 0)
            };

            //20 units of waste per hour
            IWaste waste1 = new Waste()
            {
                Title = "Over Cooked",
                Rate = new Rate()
                {
                    Quantity = 20,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //30 units of waste per hour
            IWaste waste2 = new Waste()
            {
                Title = "On Floor",
                Rate = new Rate()
                {
                    Quantity = 30,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //50 units of waste per hour
            IWaste waste3 = new Waste()
            {
                Title = "Under sized",
                Rate = new Rate()
                {
                    Quantity = 50,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //100 units of waste per hour
            IWaste waste = new WasteComposite()
            {
                Wastes = new IWaste[] { waste1, waste2, waste3 }.ToList(),
                TimeSpan = new TimeSpan(1, 0, 0)
            };

            Rate potentialRate = new Rate()
            {
                Quantity = 50,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            Rate actualRate = new Rate()
            {
                Quantity = 40,
                TimeSpan = new TimeSpan(0, 1, 0)
            };



            TimeSpan hoursPerYear = hoursPerWeek.GetQuantityForTimeSpan(new TimeSpan(365, 6, 0, 0, 0));

            return new LineOpportunity()
            {
                Downtime = downTime,
                Waste = waste,
                BottleNeckRate = potentialRate,
                ActualRate = actualRate,
                TimeSpan = hoursPerYear,
                ValuePerUnit = 0.1
            };
        }

        public LineOpportunity CreateLineOpportunityFullDowntime()
        {
            //2 minutes of downtime per hour
            IDowntime downTime1 = new Downtime()
            {
                Title = "Stoppages",
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(1, 0, 0),
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //3 minutes of downtime per hour
            IDowntime downTime2 = new Downtime()
            {
                Title = "Repair",
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(1, 0, 0),
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //5 minutes of downtime per hour
            IDowntime downTime = new DowntimeComposite()
            {
                Downtimes = new IDowntime[] { downTime1, downTime2 }.ToList(),
                TimeSpan = new TimeSpan(1, 0, 0)
            };

            //20 units of waste per hour
            IWaste waste1 = new Waste()
            {
                Title = "Over Cooked",
                Rate = new Rate()
                {
                    Quantity = 20,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //30 units of waste per hour
            IWaste waste2 = new Waste()
            {
                Title = "On Floor",
                Rate = new Rate()
                {
                    Quantity = 30,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //50 units of waste per hour
            IWaste waste3 = new Waste()
            {
                Title = "Under sized",
                Rate = new Rate()
                {
                    Quantity = 50,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //100 units of waste per hour
            IWaste waste = new WasteComposite()
            {
                Wastes = new IWaste[] { waste1, waste2, waste3 }.ToList(),
                TimeSpan = new TimeSpan(1, 0, 0)
            };

            Rate potentialRate = new Rate()
            {
                Quantity = 50,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            Rate actualRate = new Rate()
            {
                Quantity = 40,
                TimeSpan = new TimeSpan(0, 1, 0)
            };



            TimeSpan hoursPerYear = hoursPerWeek.GetQuantityForTimeSpan(new TimeSpan(365, 6, 0, 0, 0));

            return new LineOpportunity()
            {
                Downtime = downTime,
                Waste = waste,
                BottleNeckRate = potentialRate,
                ActualRate = actualRate,
                TimeSpan = hoursPerYear,
                ValuePerUnit = 0.1
            };
        }

        public LineOpportunity CreateLineOpportunity80PercentDowntime()
        {
            //2 minutes of downtime per hour
            IDowntime downTime1 = new Downtime()
            {
                Title = "Stoppages",
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(1, 0, 0),
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //3 minutes of downtime per hour
            IDowntime downTime2 = new Downtime()
            {
                Title = "Repair",
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 36, 0),
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //5 minutes of downtime per hour
            IDowntime downTime = new DowntimeComposite()
            {
                Downtimes = new IDowntime[] { downTime1, downTime2 }.ToList(),
                TimeSpan = new TimeSpan(1, 0, 0)
            };

            //20 units of waste per hour
            IWaste waste1 = new Waste()
            {
                Title = "Over Cooked",
                Rate = new Rate()
                {
                    Quantity = 30,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //30 units of waste per hour
            IWaste waste2 = new Waste()
            {
                Title = "On Floor",
                Rate = new Rate()
                {
                    Quantity = 40,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //50 units of waste per hour
            IWaste waste3 = new Waste()
            {
                Title = "Under sized",
                Rate = new Rate()
                {
                    Quantity = 50,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //100 units of waste per hour
            IWaste waste = new WasteComposite()
            {
                Wastes = new IWaste[] { waste1, waste2, waste3 }.ToList(),
                TimeSpan = new TimeSpan(1, 0, 0)
            };

            Rate potentialRate = new Rate()
            {
                Quantity = 1,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            Rate actualRate = new Rate()
            {
                Quantity = 1,
                TimeSpan = new TimeSpan(0, 1, 0)
            };



            TimeSpan hoursPerYear = hoursPerWeek.GetQuantityForTimeSpan(new TimeSpan(365, 6, 0, 0, 0));

            return new LineOpportunity()
            {
                Downtime = downTime,
                Waste = waste,
                BottleNeckRate = potentialRate,
                ActualRate = actualRate,
                TimeSpan = hoursPerYear,
                ValuePerUnit = 0.1
            };
        }

        public LineOpportunity CreateLineOpportunity80PercentDowntime80PercentageWaste()
        {
            //2 minutes of downtime per hour
            IDowntime downTime1 = new Downtime()
            {
                Title = "Stoppages",
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(1, 0, 0),
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //3 minutes of downtime per hour
            IDowntime downTime2 = new Downtime()
            {
                Title = "Repair",
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 36, 0),
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //5 minutes of downtime per hour
            IDowntime downTime = new DowntimeComposite()
            {
                Downtimes = new IDowntime[] { downTime1, downTime2 }.ToList(),
                TimeSpan = new TimeSpan(1, 0, 0)
            };

            //20 units of waste per hour
            IWaste waste1 = new Waste()
            {
                Title = "Over Cooked",
                Rate = new Rate()
                {
                    Quantity = 6,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //30 units of waste per hour
            IWaste waste2 = new Waste()
            {
                Title = "On Floor",
                Rate = new Rate()
                {
                    Quantity = 40,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //50 units of waste per hour
            IWaste waste3 = new Waste()
            {
                Title = "Under sized",
                Rate = new Rate()
                {
                    Quantity = 50,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //100 units of waste per hour
            IWaste waste = new WasteComposite()
            {
                Wastes = new IWaste[] { waste1, waste2, waste3 }.ToList(),
                TimeSpan = new TimeSpan(1, 0, 0)
            };

            Rate potentialRate = new Rate()
            {
                Quantity = 1,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            Rate actualRate = new Rate()
            {
                Quantity = 1,
                TimeSpan = new TimeSpan(0, 1, 0)
            };



            TimeSpan hoursPerYear = hoursPerWeek.GetQuantityForTimeSpan(new TimeSpan(365, 6, 0, 0, 0));

            return new LineOpportunity()
            {
                Downtime = downTime,
                Waste = waste,
                BottleNeckRate = potentialRate,
                ActualRate = actualRate,
                TimeSpan = hoursPerYear,
                ValuePerUnit = 0.1
            };
        }

    }
}
