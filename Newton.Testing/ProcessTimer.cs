using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Testing
{
    /// <summary>
    /// Measures the amount of time a process takes to complete
    /// </summary>
    public static class ProcessTimer
    {
        #region Methods

        /// <summary>
        /// Measures the amount of time a process takes to complete
        /// </summary>
        public static TimeSpan Time(this Action action, int iterations)
        {
            var start = DateTime.Now;

            for (int i = 0; i < iterations; i++)
            {
                action();
            }
            return DateTime.Now - start;
        }

        /// <summary>
        /// Measures the amount of time a process takes to complete
        /// </summary>
        public static TimeSpan Time<TResult, T1>(this Func<T1, TResult> func, int iterations, T1 t1)
        {
            return Time(
                () =>
                {
                    func(t1);
                },
                iterations);
        }

        /// <summary>
        /// Measures the amount of time a process takes to complete
        /// </summary>
        public static TimeSpan Time<TResult, T1, T2>(this Func<T1, T2, TResult> func, int iterations, T1 t1, T2 t2)
        {
            return Time(
                () =>
                {
                    func(t1, t2);
                },
                iterations);
        }

        /// <summary>
        /// Measures the amount of time a process takes to complete
        /// </summary>
        public static TimeSpan Time<TResult, T1, T2, T3>(this Func<T1, T2, T3, TResult> func, int iterations, T1 t1, T2 t2, T3 t3)
        {
            return Time(
                () =>
                {
                    func(t1, t2, t3);
                },
                iterations);
        }

        /// <summary>
        /// Measures the amount of time a process takes to complete
        /// </summary>
        public static TimeSpan Time<TResult, T1, T2, T3, T4>(this Func<T1, T2, T3, T4, TResult> func, int iterations, T1 t1, T2 t2, T3 t3, T4 t4)
        {
            return Time(
                () =>
                {
                    func(t1, t2, t3, t4);
                },
                iterations);
        }

        #endregion
    }
}