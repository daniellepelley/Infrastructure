using System;

namespace Newton.UI.Wpf
{
    public interface ICollectionViewModel
    {
        MainTemplateSelector DataTemplateSelector { get; set; }
        string EntityName { get; }
        //Action<object> NavigateAction { get; set; }
    }
}
