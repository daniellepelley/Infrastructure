using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    /// <summary>
    /// Class representing a link between two machines
    /// </summary>
    public class MachineLink : IMachineLink
    {
        private List<IMachine> aEndMachines;
        /// <summary>
        /// List of machines at the a-end of the link
        /// </summary>
        public List<IMachine> AEndMachines
        {
            get { return aEndMachines; }
            set { aEndMachines = value; }
        }

        private List<IMachine> zEndMachines;
        /// <summary>
        /// List of machines at the zend of the link
        /// </summary>
        public List<IMachine> ZEndMachines
        {
            get { return zEndMachines; }
            set { zEndMachines = value; }
        }
    }
}
