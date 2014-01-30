using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Newton.Dev.BusinessObjects
{
    public class IBusinessObject : IBusinessEntity
    {
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public interface IBusinessEntity : INotifyPropertyChanged 
    { }
}
