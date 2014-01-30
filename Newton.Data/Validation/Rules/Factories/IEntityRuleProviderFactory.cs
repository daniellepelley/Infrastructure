using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// An interface representing a rule provider for entity of type T
    /// </summary>
    public interface IEntityRuleProviderFactory
    {
        /// <summary>
        /// Creates a rule provider for type T
        /// </summary>
        IEntityRuleProvider<T> Create<T>();
    }
}
