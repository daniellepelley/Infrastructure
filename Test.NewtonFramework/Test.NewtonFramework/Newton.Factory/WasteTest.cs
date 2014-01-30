using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newton.Factory;

namespace Newton.Factory.Test
{
    [TestClass]
    public class WasteTest
    {
        [TestMethod]
        [Owner("Factory")]
        public void TotalWasteTest()
        {
            //5 units per hour
            //10 units in 2 hours
            Waste waste1 = new Waste()
                {
                    Rate = new Rate()
                    {
                        Quantity = 5,
                        TimeSpan = new TimeSpan(1, 0, 0)
                    }
                };

            //6 units per half hour
            //24 units in 2 hours
            Waste waste2 = new Waste()
            {
                Rate = new Rate()
                {
                    Quantity = 6,
                    TimeSpan = new TimeSpan(0, 30, 0)
                }
            };

            //34 units in 2 hours
            var rate = new IWaste[] { waste1, waste2 }.TotalRate(new TimeSpan(2, 0, 0));

            double units = 34;

            Assert.AreEqual(units, rate.Quantity);
        }

        [TestMethod]
        [Owner("Factory")]
        public void AverageWasteTest()
        {
            //5 units per hour
            //10 units in 2 hours
            Waste waste1 = new Waste()
            {
                Rate = new Rate()
                {
                    Quantity = 5,
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //6 units per half hour
            //24 units in 2 hours
            Waste waste2 = new Waste()
            {
                Rate = new Rate()
                {
                    Quantity = 6,
                    TimeSpan = new TimeSpan(0, 30, 0)
                }
            };

            //Average 17 units in 2 hours
            var rate = new IWaste[] { waste1, waste2 }.AverageRate(new TimeSpan(2, 0, 0));

            double units = 17;

            Assert.AreEqual(units, rate.Quantity);
        }

        [TestMethod]
        [Owner("Factory")]
        public void WasteToTimeRateTest()
        {
            //200 unit of downtime per hour
            Waste waste1 = new Waste()
            {
                Rate = new Rate()
                {
                    Quantity = 200,
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //20 units per minute
            Rate unitRate = new Rate()
            {
                Quantity = 20,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            //var rate = waste1.Rate.ToTimeSpanRate(unitRate);

            var rate = waste1.TimeSpanRate(unitRate);


            TimeSpanRate expected = new TimeSpanRate()
            {
                Quantity = new TimeSpan(0, 10, 0),
                TimeSpan = new TimeSpan(1, 0, 0)
            };
            
            
            //TimeSpan minutes = new TimeSpan(0, 10, 0);

            Assert.AreEqual(expected, rate);
        }


        [TestMethod]
        [Owner("Factory")]
        public void WasteToTimeRateTest2()
        {
            //20 units of waste per 2 hours
            IWaste waste1 = new Waste()
            {
                Title = "Over Cooked",
                Rate = new Rate()
                {
                    Quantity = 20,
                    TimeSpan = new TimeSpan(2, 0, 0)
                }
            };

            //40 units per minute
            Rate unitRate = new Rate()
            {
                Quantity = 40,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            //var rate = waste1.Rate.ToTimeSpanRate(unitRate);

            var rate = waste1.TimeSpanRate(unitRate);

            //20 units in 2 hours
            //0.16666667 units of waste in 1 minute
            
            //How long does it take to make .166667 units at 40 per minute

            //Each unit take 1.5 seconds
            //.166667 units take .25 seconds to make

            TimeSpan minutes = new TimeSpan(0, 0, 0, 0, 250);

            Assert.AreEqual(minutes, rate.GetQuantityForTimeSpan(new TimeSpan(0, 1, 0)));
        }
    

        [TestMethod]
        [Owner("Factory")]
        public void WasteToTimeRateTest3()
        {
            //20 units of waste per 2 hours
            IWaste waste1 = new Waste()
            {
                Title = "Over Cooked",
                Rate = new Rate()
                {
                    Quantity = 20,
                    TimeSpan = new TimeSpan(0, 1, 0)
                }
            };

            //40 units per minute
            Rate unitRate = new Rate()
            {
                Quantity = 40,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            //var rate = waste1.Rate.ToTimeSpanRate(unitRate);
            var rate = waste1.TimeSpanRate(unitRate);
            TimeSpan minutes = new TimeSpan(0, 0, 0, 30);

            Assert.AreEqual(minutes, rate.Quantity);
        }

        [TestMethod]
        [Owner("Factory")]
        public void WasteToTimeRateTest4()
        {
            //20 units of waste per 2 hours
            IWaste waste1 = new Waste()
            {
                Title = "Over Cooked",
                Rate = new Rate()
                {
                    Quantity = 20,
                    TimeSpan = new TimeSpan(0, 2, 0)
                }
            };

            //40 units per minute
            Rate unitRate = new Rate()
            {
                Quantity = 40,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            //var rate = waste1.Rate.ToTimeSpanRate(unitRate);
            var rate = waste1.TimeSpanRate(unitRate);

            TimeSpan minutes = new TimeSpan(0, 0, 0, 15);

            Assert.AreEqual(minutes, rate.GetQuantityForTimeSpan(new TimeSpan(0,1,0)));
        }


        [TestMethod]
        [Owner("Factory")]
        public void WasteCompositeTest2()
        {
            //5 units per hour
            //10 units in 2 hours
            Waste downTime1 = new Waste()
            {
                Rate = new Rate()
                {
                    Quantity = 5,
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //6 units per half hour
            //24 units in 2 hours
            Waste downTime2 = new Waste()
            {
                Rate = new Rate()
                {
                    Quantity = 6,
                    TimeSpan = new TimeSpan(0, 30, 0)
                }
            };

            WasteComposite wasteComposite = new WasteComposite();
            wasteComposite.TimeSpan = new TimeSpan(3, 0, 0);

            wasteComposite.Wastes.AddRange(new IWaste[] { downTime1, downTime2 });

            //51 minutes in 3 hours

            double units = 51;

            Assert.AreEqual(units, wasteComposite.Rate.Quantity);
        }

    }
}