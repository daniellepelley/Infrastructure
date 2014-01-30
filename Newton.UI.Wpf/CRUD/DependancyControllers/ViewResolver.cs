using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.UI.Wpf
{
    public interface IViewResolver
    {
        System.Windows.FrameworkElement Resolve(IViewModelBase viewModel);
    }

    public class ViewResolver : IViewResolver
    {
        public System.Windows.FrameworkElement Resolve(IViewModelBase viewModel)
        {
            if (viewModel is IEditViewModel)
                return new EditView();
            else if (viewModel is CollectionViewModel)
                return new CollectionView();
            return null;
        }
    }

    public class BindingViewResolver : IViewResolver
    {
        private Action<IViewModelBase> setViewModelAction;

        public Action<IViewModelBase> SetViewModelAction
        {
            get { return setViewModelAction; }
            set { setViewModelAction = value; }
        }

        public BindingViewResolver(Action<IViewModelBase> setViewModelAction)
        {
            this.setViewModelAction = setViewModelAction;
        }

        public System.Windows.FrameworkElement Resolve(IViewModelBase viewModel)
        {
            setViewModelAction(viewModel);
            return null;
        }
    }
}
