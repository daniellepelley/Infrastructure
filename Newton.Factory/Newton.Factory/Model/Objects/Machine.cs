using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// Class representing a machine in a production line
    /// </summary>
    public class Machine : IMachine
    {
        #region Properties

        private Rate actualSpeed;
        /// <summary>
        /// The actual speed of the machine
        /// </summary>
        public Rate ActualSpeed
        {
            get { return actualSpeed; }
            set { actualSpeed = value; }
        }

        private Rate potentialSpeed;
        /// <summary>
        /// The potential speed of the machine
        /// </summary>
        public Rate PotentialSpeed
        {
            get { return potentialSpeed; }
            set { potentialSpeed = value; }
        }

        private double utilization = 1;
        /// <summary>
        /// The ratio of utilization for the machine
        /// </summary>
        public double Utilization
        {
            get { return utilization; }
            set { utilization = value; }
        }

        /// <summary>
        /// The efficiency of the machine
        /// </summary>
        public double Efficency
        {
            get { return actualSpeed.GetRatio(potentialSpeed) * utilization; }
        }

        private string name;
        /// <summary>
        /// The name of the machine
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        #endregion
    }
}
