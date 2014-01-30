using System;
using System.Collections.Generic;

namespace Newton.Factory
{
    public interface IMachineLink
    {
        List<IMachine> AEndMachines { get; set; }
        List<IMachine> ZEndMachines { get; set; }
    }
}
