using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Newton.UI.Wpf
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class EditView : UserControl
    {
        public EditView()
        {
            InitializeComponent();
        }

        private void RadDataForm_ManipulationCompleted(object sender, ManipulationCompletedEventArgs e)
        {
            //ApplicationCommands.Save.Execute(e.Source, null);
        }

        private void RadDataForm_EditEnded(object sender, Telerik.Windows.Controls.Data.DataForm.EditEndedEventArgs e)
        {
            ApplicationCommands.Save.Execute(null, null);
        }
    }
}
