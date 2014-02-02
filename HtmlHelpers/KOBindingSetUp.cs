﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Expressions;
using System.Text;

namespace HtmlHelpers
{
    public class KOBindingSetUp : IAttributeBuilder
    {
        private string property;
        private string bindingType;
        private string valueUpdate;

        public KOBindingSetUp Property(string property)
        {
            this.property = property;
            return this;
        }

        public KOBindingSetUp BindingType(string bindingType)
        {
            this.bindingType = bindingType;
            return this;
        }

        public KOBindingSetUp ValueUpdate(string valueUpdate)
        {
            this.valueUpdate = valueUpdate;
            return this;
        }

        public string Key()
        {
            return "data-bind";
        }

        public string Value()
        {
            var str = string.Format(@"{0}: {1}", bindingType, property);

            if (!string.IsNullOrEmpty(valueUpdate))
            {
                str += string.Format(@", valueUpdate: ""{0}""", valueUpdate);
            }

            return str;
        }
    }
}