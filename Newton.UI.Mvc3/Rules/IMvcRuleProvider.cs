using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;

using Newton.Validation;

namespace Newton.UI.Mvc3
{
    /// <summary>
    /// An interface representing a rule provider that works with the Mvc framework
    /// </summary>
    public interface IMvcRuleProvider<T>
    {
        /// <summary>
        /// Validates the model and returns a ModelStateDictionary
        /// </summary>
        ModelStateDictionary ValidateForMvc(T model);

        /// <summary>
        /// Creates a collection of UnobstrutiveJava html attributes
        /// </summary>
        IDictionary<string, IDictionary<string, object>> GetUnobstrutiveJava();
    }
}