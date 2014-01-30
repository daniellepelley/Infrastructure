using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Newton.Dates
{
    /// <summary>
    /// Plugin adapter representing a period of time
    /// </summary>
    public class PeriodOfTimePlugIn<TPlugIn> : IPeriodOfTime
    {
        #region Properties

        private TPlugIn plugIn;
        /// <summary>
        /// The plugin that provides the data information
        /// </summary>
        public TPlugIn PlugIn
        {
            get { return plugIn; }
            set { plugIn = value; }
        }

        private Action<TPlugIn, DateTime?> startSet;
        /// <summary>
        /// The method to set the start date
        /// </summary>
        public Action<TPlugIn, DateTime?> StartSet
        {
            get { return startSet; }
            set { startSet = value; }
        }

        private Func<TPlugIn, DateTime?> startGet;
        /// <summary>
        /// The method to get the start date
        /// </summary>
        public Func<TPlugIn, DateTime?> StartGet
        {
            get { return startGet; }
            set { startGet = value; }
        }

        private Action<TPlugIn, DateTime?> endSet;
        /// <summary>
        /// The method to set the end date
        /// </summary>
        public Action<TPlugIn, DateTime?> EndSet
        {
            get { return endSet; }
            set { endSet = value; }
        }

        private Func<TPlugIn, DateTime?> endGet;
        /// <summary>
        /// The method to get the end date
        /// </summary>
        public Func<TPlugIn, DateTime?> EndGet
        {
            get { return endGet; }
            set { endGet = value; }
        }

        private DateTime? start;
        /// <summary>
        /// The start of the period of time
        /// </summary>
        public DateTime? Start
        {
            get
            {
                if (startGet != null)
                    return startGet(plugIn);
                return null;
            }
            set
            {
                if (startSet != null)
                    startSet(plugIn, value);
            }
        }

        private DateTime? end;
        /// <summary>
        /// The end of the period of time
        /// </summary>
        public DateTime? End
        {
            get
            {
                if (endGet != null)
                    return endGet(plugIn);
                return null;
            }
            set
            {
                if (endSet != null)
                    endSet(plugIn, value);
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Plugin adapter representing a period of time
        /// </summary>
        public PeriodOfTimePlugIn(
            TPlugIn plugIn,
            Action<TPlugIn, DateTime?> startSet,
            Func<TPlugIn, DateTime?> startGet,
            Action<TPlugIn, DateTime?> endSet,
            Func<TPlugIn, DateTime?> endGet)
        {
            this.plugIn = plugIn;
            this.startSet = startSet;
            this.startGet = startGet;
            this.endSet = endSet;
            this.endGet = endGet;
        }

        #endregion
    }
}
