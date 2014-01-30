using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newton.Factory;

namespace Newton.Factory.Test
{
    [TestClass]
    public class CubeTest
    {
        [TestMethod]
        [Owner("Factory")]
        public void CubeTest1()
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

            IWaste waste = new WasteComposite(
                new IWaste[] {
                    waste1, waste2 });

            //10 minutes of downtime per hour
            Downtime downtime1 = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 10, 0),
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            //10 minutes of downtime per hour
            Downtime downtime2 = new Downtime()
            {
                Rate = new TimeSpanRate()
                {
                    Quantity = new TimeSpan(0, 20, 0),
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            };

            IDowntime downtime = new DowntimeComposite(
                new IDowntime[] {
                    downtime1, downtime2 });

            //2 units per minute of slow running
            SlowRunning slowRunning = new SlowRunning()
            {
                Rate = new Rate()
                {
                    Quantity = 2,
                    TimeSpan = new TimeSpan(0, 1, 0)
                }
            };

            //34 units in 2 hours
            var rate = new IWaste[] { waste1, waste2 }.TotalRate(new TimeSpan(2, 0, 0));

            ConstraintCubeTest cube = new ConstraintCubeTest();
            cube.Dimensions.Add(waste);
            cube.Dimensions.Add(downtime);
            cube.Dimensions.Add(slowRunning);
            cube.Dimensions.Add(slowRunning);
            cube.OptimumRate = new Rate() { Quantity = 10, TimeSpan = new TimeSpan(0, 1, 0) };
            var u1 = cube.Efficiency(waste);
            var u2 = cube.Efficiency(downtime);
            var u4 = cube.Efficiency(slowRunning);
            var r1 = cube.MaximumUtilisation(waste);
            var e = cube.Efficiency();


            ConstraintCubeTest waste1Dimension = new ConstraintCubeTest();
            waste1Dimension.Dimensions.Add(downtime);
            waste1Dimension.OptimumRate = new Rate() { Quantity = 10, TimeSpan = new TimeSpan(0, 1, 0) };
            var eWaste1Dimension = waste1Dimension.Efficiency();

            ConstraintCubeTest waste2Dimension = new ConstraintCubeTest();
            waste2Dimension.Dimensions.Add(downtime1);
            waste2Dimension.Dimensions.Add(downtime2);
            waste2Dimension.OptimumRate = new Rate() { Quantity = 10, TimeSpan = new TimeSpan(0, 1, 0) };
            var eWaste2Dimension = waste2Dimension.Efficiency();

            Dictionary<double, double> dictionary = new Dictionary<double, double>();

            //d is a measure of how mutually exclusive the 2 efficiencies are
            for (double d = 0; d <= 1; d += 0.01)
            {
                dictionary.Add(Math.Round(d, 2),
                Math.Round(ConstraintCalculations.CumlativeEfficiency(waste2Dimension.Efficiency(downtime1), waste2Dimension.Efficiency(downtime2), d), 2));
            }

        }

    }
}