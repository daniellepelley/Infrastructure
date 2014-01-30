using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.UI.Wpf;

namespace Newton.UI.Wpf
{
    public class MainTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            string xaml = EntityToDataTemplate.CreateXamlDataTemplate(item);
            return EntityToDataTemplate.XamlToDataTemplate(xaml); ;
        }
    }

    public class ContainerTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
        {
            if (item is IEditViewModel)
                return (System.Windows.DataTemplate)new ViewsDictionary()["EditViewDataTemplate"];
            else if (item is ICollectionViewModel)
                return (System.Windows.DataTemplate)new ViewsDictionary()["CollectionViewDataTemplate"];
            return null;
        }
    }

}
