using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newton.Factory;

namespace Newton.Factory.Test
{
    [TestClass]
    public class ManufacturingExamplesTest
    {
        [TestMethod]
        [Owner("Factory")]
        public void Example1()
        {
            Line line = new Line();

            line.Machines.Add(new Machine()
            {
                Name = "Bretting",
                ActualSpeed = new Rate()
                {
                    Quantity = 100,
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            });

            line.Machines.Add(new Machine()
            {
                Name = "Bander",
                ActualSpeed = new Rate()
                {
                    Quantity = 110,
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            });

            line.Machines.Add(new Machine()
            {
                Name = "Boxer",
                ActualSpeed = new Rate()
                {
                    Quantity = 130,
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            });

            line.Machines.Add(new Machine()
            {
                Name = "Palletiser",
                ActualSpeed = new Rate()
                {
                    Quantity = 150,
                    TimeSpan = new TimeSpan(1, 0, 0)
                }
            });


            var actual =
                line.BottleNeckSpeed.GetRatio(
                new Rate() { Quantity = 600, TimeSpan = new TimeSpan(12, 0, 0) });
                
            double expected = .5; // 188K

            Assert.AreEqual(expected, actual);
        }
    }
}
