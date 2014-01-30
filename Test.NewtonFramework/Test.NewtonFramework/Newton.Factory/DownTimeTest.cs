using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newton.Factory;

namespace Newton.Factory.Test
{
    [TestClass]
    public class DownTimeTest
    {
        [TestMethod]
        [Owner("Factory")]
        public void TotalDowntimeTest()
        {
            //5 minutes per hour
            //10 minutes in 2 hours
            Downtime downTime1 = new Downtime()
                {
                    Rate = new TimeSpanRate()
                    {
                        Quantity = new TimeSpan(0, 5, 0),
                        TimeSpan = new TimeSpan(1, 0, 0)
                    }
                };

            //6 minutes per half hour
            //24 minutes in 2 hours
            Downtime downTime2 = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 6, 0),
                    TimeSpan = new TimeSpan(0, 30, 0)
                }
            };

            //34 minutes in 2 hours
            var rate = new IDowntime[] { downTime1, downTime2 }.TotalRate(new TimeSpan(2, 0, 0));

            double minutes = 34;

            Assert.AreEqual(minutes, rate.Quantity.TotalMinutes);
        }

        [TestMethod]
        [Owner("Factory")]
        public void AverageDowntimeTest()
        {
            //5 minutes per hour
            //10 minutes in 2 hours
            Downtime downTime1 = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 5, 0),
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //6 minutes per half hour
            //24 minutes in 2 hours
            Downtime downTime2 = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 6, 0),
                    TimeSpan = new TimeSpan(0, 30, 0)
                }
            };

            //Average 17 minutes in 2 hours
            var rate = new IDowntime[] { downTime1, downTime2 }.AverageRate(new TimeSpan(2, 0, 0));

            double minutes = 17;

            Assert.AreEqual(minutes, rate.Quantity.TotalMinutes);
        }

        [TestMethod]
        [Owner("Factory")]
        public void DowntimeToUnitRateTest()
        {
            //10 minutes of downtime per hour
            Downtime downTime1 = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 10, 0),
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //20 units per minute
            Rate unitRate = new Rate()
            {
                Quantity = 20,
                TimeSpan = new TimeSpan(0, 1, 0)
            };

            //Average 17 minutes in 2 hours
            //var rate = downTime1.Rate.ToRate(unitRate);

            var rate = downTime1.UnitRate(unitRate);

            //return a rate of 200 units per hour
            //return a rate of 200 units per hour
            Rate expected = new Rate()
            {
                Quantity = 200,
                TimeSpan = new TimeSpan(1, 0, 0)
            };

            //double minutes = 200;

            Assert.AreEqual(expected, rate);

        }


        [TestMethod]
        [Owner("Factory")]
        public void DowntimeCompositeTest2()
        {
            //5 minutes per hour
            //10 minutes in 2 hours
            Downtime downTime1 = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 5, 0),
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //6 minutes per half hour
            //24 minutes in 2 hours
            Downtime downTime2 = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 6, 0),
                    TimeSpan = new TimeSpan(0, 30, 0)
                }
            };

            DowntimeComposite downtimeComposite = new DowntimeComposite();
            downtimeComposite.TimeSpan = new TimeSpan(3, 0, 0);

            downtimeComposite.Downtimes.AddRange(new IDowntime[] { downTime1, downTime2 });

            //51 minutes in 3 hours

            double minutes = 51;

            Assert.AreEqual(minutes, downtimeComposite.Rate.Quantity.TotalMinutes);
        }


        [TestMethod]
        [Owner("Factory")]
        public void DowntimeCompositeDelta1()
        {
            //5 minutes per hour
            Downtime downTime1 = new Downtime()
            {
                Delta = 0.5,
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 5, 0),
                    TimeSpan = new TimeSpan(1, 0, 0),
                }
            };

            //6 minutes per half hour
            //12 minutes in 1 hour
            Downtime downTime2 = new Downtime()
            {
                Delta = 0.5,
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 6, 0),
                    TimeSpan = new TimeSpan(0, 30, 0)
                }
            };

            DowntimeComposite downtimeComposite = new DowntimeComposite();
            downtimeComposite.Downtimes.AddRange(new IDowntime[] { downTime1, downTime2 });
            downtimeComposite.TimeSpan = new TimeSpan(1, 0, 0);

            double expected = 0.5;
            Assert.AreEqual(expected, downtimeComposite.Delta);
        }

        [TestMethod]
        [Owner("Factory")]
        public void DowntimeCompositeDelta2()
        {
            //5 minutes per hour
            Downtime downTime1 = new Downtime()
            {
                Delta = 0.4,
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 5, 0),
                    TimeSpan = new TimeSpan(1, 0, 0),
                }
            };

            //6 minutes per half hour
            //12 minutes in 1 hour
            Downtime downTime2 = new Downtime()
            {
                Delta = 0.2,
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 6, 0),
                    TimeSpan = new TimeSpan(0, 30, 0)
                }
            };

            DowntimeComposite downtimeComposite = new DowntimeComposite();
            downtimeComposite.Downtimes.AddRange(new IDowntime[] { downTime1, downTime2 });
            downtimeComposite.TimeSpan = new TimeSpan(1, 0, 0);

            double ratio1 = 0.294;
            double ratio2 = 0.706;



            double expected = Math.Round((ratio1 * downTime1.Delta) + (ratio2 * downTime2.Delta), 3);
            Assert.AreEqual(expected, Math.Round(downtimeComposite.Delta, 3));
        }
    }
}