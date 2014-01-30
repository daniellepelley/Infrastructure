using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    public interface IRuleCollection
    {
        IEnumerable<IRule> GetIRules();
        void Add(IRule rule);
    }
}