using System;
using System.Collections.Generic;
using System.Text;

namespace Newton.Validation
{
    /// <summary>
    /// A rule which states the date must be in the future.
    /// </summary>
    public class FutureRule : FieldRule<DateTime?>
    {
        #region Parameter

        private TimeUnit timeUnit = TimeUnit.Day;
        /// <summary>
        /// Unit of time counting as the cut off for a future date
        /// </summary>
        public TimeUnit TimeUnit
        {
            get { return timeUnit; }
            set { timeUnit = value; }
        }

        private bool includeCurrent = false;
        /// <summary>
        /// Include the current unit of time as valid
        /// </summary>
        public bool IncludeCurrent
        {
            get { return includeCurrent; }
            set { includeCurrent = value; }
        }

        #endregion

        #region Constructors

        public FutureRule()
        {
        }

        public FutureRule(TimeUnit timeUnit)
        {
            this.timeUnit = timeUnit;
        }

        public FutureRule(TimeUnit timeUnit, bool includeCurrent)
            : this(timeUnit)
        {
            this.includeCurrent = includeCurrent;
        }

        #endregion

        #region Methods

        /// <summary>
        /// A rule which states the date must be in the future.
        /// </summary>
        protected override string LogicMethod(DateTime? value)
        {
            if (value == null)
                return string.Empty;

            if (value.Value.Year >= DateTime.Now.Year)
            {
                if (value.Value.Year > DateTime.Now.Year)
                    return string.Empty;

                if (value.Value.Month >= DateTime.Now.Month)
                {
                    if (value.Value.Month > DateTime.Now.Month)
                        return string.Empty;

                    if (value.Value.Day >= DateTime.Now.Day)
                    {
                        if (value.Value.Day > DateTime.Now.Day)
                            return string.Empty;

                        if (TimeUnit == TimeUnit.Day)
                        {
                            if (value.Value.Day == DateTime.Now.Day &&
                                IncludeCurrent)
                            {
                                return string.Empty;
                            }
                        }

                        if (value.Value.Hour >= DateTime.Now.Hour)
                        {
                            if (value.Value.Hour > DateTime.Now.Hour)
                                return string.Empty;

                            if (TimeUnit == TimeUnit.Hour)
                            {
                                if (value.Value.Hour == DateTime.Now.Hour &&
                                    IncludeCurrent)
                                {
                                    return string.Empty;
                                }
                            }

                            if (value.Value.Minute >= DateTime.Now.Minute)
                            {
                                if (value.Value.Minute > DateTime.Now.Minute)
                                    return string.Empty;

                                if (TimeUnit == TimeUnit.Minute)
                                {
                                    if (value.Value.Minute == DateTime.Now.Minute &&
                                        IncludeCurrent)
                                    {
                                        return string.Empty;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return "{0} must be a future date";
        }

        #endregion
    }
}