using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Xml;
using System.Xml.Linq;
using System.Windows.Controls;

using System.Reflection;
using Newton.Extensions;

namespace Newton.UI.Wpf
{
    public static class EntityToDataTemplate
    {
        public static string CreateXamlDataTemplate(object entity)
        {
            if (entity == null)
                return string.Empty;

            List<PropertyInfo> valueTypes = new List<PropertyInfo>();
            List<PropertyInfo> objects = new List<PropertyInfo>();

            foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType.IsValueType ||
                    propertyInfo.PropertyType == typeof(string))
                {
                    valueTypes.Add(propertyInfo);
                }
                else
                {
                    objects.Add(propertyInfo);
                }
            }

            XNamespace ns = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
            XElement xDataTemplate =
                new XElement(ns + "DataTemplate",
                    new XElement(ns + "StackPanel",
                        CreateTextBlocks(ns, valueTypes),
                        CreateCommandButtons(ns, objects)
                        ));

            return xDataTemplate.ToString();
        }

        public static XElement[] CreateTextBlocks(XNamespace ns, IEnumerable<PropertyInfo> propertyInfos)
        {
            List<XElement> elements = new List<XElement>();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                elements.Add(
                    new XElement(ns + "StackPanel",
                        new XAttribute("Orientation", "Horizontal"),
                        new XElement(ns + "TextBlock",
                            new XAttribute("Text", CleanUpFieldName(propertyInfo.Name) + ":"),
                            new XAttribute("Width", 180)),

                            CreateInputField(ns, propertyInfo)
                            ));
            }
            return elements.ToArray();
        }

        public static XElement CreateInputField(XNamespace ns, PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite)
            {
                return CreateTextReadOnlyField(ns, propertyInfo);
            }
            else if (propertyInfo.PropertyType == typeof(DateTime) ||
                propertyInfo.PropertyType == typeof(DateTime?))
            {
                return CreateDateInputField(ns, propertyInfo);
            }
            return CreateTextInputField(ns, propertyInfo);
        }

        public static XElement CreateDateInputField(XNamespace ns, PropertyInfo propertyInfo)
        {
            return new XElement(ns + "DatePicker",
                new XAttribute("SelectedDate", "{Binding Path=" + propertyInfo.Name + ", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"),
                new XAttribute("Width", 250));
        }

        public static XElement CreateTextInputField(XNamespace ns, PropertyInfo propertyInfo)
        {
            return new XElement(ns + "TextBox",
                new XAttribute("Text", "{Binding Path=" + propertyInfo.Name + ", Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"),
                new XAttribute("Width", 250));
        }

        public static XElement CreateTextReadOnlyField(XNamespace ns, PropertyInfo propertyInfo)
        {
            return new XElement(ns + "TextBox",
                new XAttribute("Text", "{Binding Path=" + propertyInfo.Name + ", Mode=OneWay, UpdateSourceTrigger=PropertyChanged}"),
                new XAttribute("Width", 250));
        }

        public static string CleanUpFieldName(string rawFieldName)
        {
            return rawFieldName.Replace("_", " ").ToTitleCase();
        }

        public static XElement[] CreateCommandButtons(XNamespace ns, IEnumerable<PropertyInfo> propertyInfos)
        {
            List<XElement> elements = new List<XElement>();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                elements.Add(
                    new XElement(ns + "StackPanel",
                        new XAttribute("Orientation", "Horizontal"),
                        new XElement(ns + "Button",
                            new XAttribute("Command", "Open"),
                            new XAttribute("CommandParameter", "{Binding Path=" + propertyInfo.Name + ", Mode=OneWay, UpdateSourceTrigger=PropertyChanged}"),
                            
                            (propertyInfo.PropertyType.IsGenericType
                            ?
                            new XAttribute("Content", "Collection of " + CleanUpFieldName(propertyInfo.PropertyType.GetGenericArguments().FirstOrDefault().Name) + "s")
                            :
                            new XAttribute("Content", CleanUpFieldName(propertyInfo.PropertyType.Name)))
                            
                            )));
            }
            return elements.ToArray();
        }

        public static DataTemplate XamlToDataTemplate(string xaml)
        {
            if (string.IsNullOrEmpty(xaml))
                return null;

            StringReader sr = new StringReader(xaml);
            XmlReader xr = XmlReader.Create(sr);
            return System.Windows.Markup.XamlReader.Load(xr) as DataTemplate;
        }
    }
}
