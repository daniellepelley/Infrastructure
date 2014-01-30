using System;

namespace Newton.UI.Wpf
{
    public interface IEditViewModel
    {
        MainTemplateSelector DataTemplateSelector { get; set; }
        string EntityName { get; }
        //Action<object> NavigateAction { get; set; }
        //Action SaveAction { get; set; }
    }
}
