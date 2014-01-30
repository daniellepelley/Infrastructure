using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory
{
    public interface IDowntime :
        IConstraintDimension,
        ITimeSpanRateMeasure,
        IEfficiencyDelta
    { }
}
