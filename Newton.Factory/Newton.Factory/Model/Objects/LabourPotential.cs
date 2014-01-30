using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public class LabourPotential : ILabourPotential
    {
        private double actualLabour;

        public double ActualLabour
        {
            get { return actualLabour; }
        }

        private double potentialLabour;

        public double PotentialLabour
        {
            get { return potentialLabour; }
        }

        public LabourPotential(double actualLabour, double potentialLabour)
        {
            this.actualLabour = actualLabour;
            this.potentialLabour = potentialLabour;
        }
    }
}
