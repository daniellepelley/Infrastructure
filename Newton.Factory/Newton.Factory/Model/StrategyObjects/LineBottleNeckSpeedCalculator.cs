using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public class LineBottleNeckSpeedCalculator
    {
        public IMachine GetBottleNeckMachine(IEnumerable<IMachine> machines)
        {
            return machines.OrderByDescending(m => m.ActualSpeed.NumberOfMillisecondsPerUnit)
                            .FirstOrDefault();
        }

        public Rate GetBottleNeckRate(IEnumerable<IMachine> machines)
        {
            return GetBottleNeckMachine(machines).ActualSpeed;
        }
    }
}
