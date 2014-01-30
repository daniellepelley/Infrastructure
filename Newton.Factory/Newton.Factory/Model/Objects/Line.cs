using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public class Line
    {
        private LineBottleNeckSpeedCalculator lineBottleNeckSpeedCalculator = new LineBottleNeckSpeedCalculator();

        private List<IMachine> machines = new List<IMachine>();

        public List<IMachine> Machines
        {
            get { return machines; }
            set { machines = value; }
        }

        private List<IMachineLink> machineLinks = new List<IMachineLink>();

        public List<IMachineLink> MachineLinks
        {
            get { return machineLinks; }
            set { machineLinks = value; }
        }

        public Rate BottleNeckSpeed
        {
            get { return lineBottleNeckSpeedCalculator.GetBottleNeckRate(machines); }
        }

        public Rate PotentialSpeed
        {
            get { return machines.Min(m => m.PotentialSpeed); }
        }
    }
}
