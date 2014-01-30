using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Newton.Extensions;

namespace Newton.Core
{
    /// <summary>
    /// Class for managing arrays.
    /// </summary>
    public static class Ranges
    {
        /// <summary>
        /// Creates a range of references
        /// </summary>
        public static string[] CreateRange(string reference, int start, int count)
        {
            List<string> list = new List<string>();

            IEnumerable<int> range = Enumerable.Range(start, count);

            foreach (int i in range)
            {
                list.Add(reference.RightOverlay(i.ToString()));
            }
            return list.ToArray();
        }

        /// <summary>
        /// Builds a range of doubles
        /// </summary>
        public static double[] DoubleRange(double minimum, double maximum, int amount)
        {
            if (amount == 1)
                return new double[] { minimum + ((maximum - minimum) / 2) };

            double interval = (maximum - minimum) / amount;

            List<double> list = new List<double>();
            for (int i = 1; i <= amount; i++)
            {
                list.Add(minimum + (i * interval));
            }
            return list.ToArray();
        }

        /// <summary>
        /// Builds a range of dates
        /// </summary>
        public static DateTime[] DateTimeRange(DateTime start, DateTime end, int amount)
        {
            double interval = (end - start).TotalSeconds / amount;

            List<DateTime> list = new List<DateTime>();
            for (int i = 0; i < amount; i++)
            {
                list.Add(start.AddSeconds(interval * i));
            }
            return list.ToArray();
        }

        /// <summary>
        /// Builds a range based on a rounded log 10
        /// </summary>
        public static double?[] RoundedLogRange(double min, double max)
        {
            return RoundedLogBands(min, max, 1).Select(d => d[0]).ToArray();
        }

        /// <summary>
        /// Builds a range based on a rounded log 10
        /// </summary>
        public static double?[] RoundedLogRange(double min, double max, double multiplier)
        {
            return RoundedLogBands(min, max, multiplier).Select(d => d[0]).ToArray();
        }

        /// <summary>
        /// Build a collection of bands based on log 10
        /// </summary>
        public static double?[][] RoundedLogBands(double min, double max)
        {
            return RoundedLogBands(min, max, 1);
        }

        /// <summary>
        /// Build a collection of bands based on log 10
        /// </summary>
        public static double?[][] RoundedLogBands(double min, double max, double multiplier)
        {
            if (min == 0 || max == 0)
                return new double?[0][];

            List<double?[]> output = new List<double?[]>();

            List<double> list = new List<double>();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            list.Add(5);

            for (double i = Math.Round(Math.Log10(min), 0); i < Math.Round(Math.Log10(max), 0) + 1; i++)
            {
                double pow = Math.Pow(10, i);

                for (int j = 0; j < list.Count; j++)
                {
                    double?[] band = new double?[2];
                    band[0] = pow * list[j];

                    if (list.Count > j + 1)
                        band[1] = pow * list[j + 1];
                    else
                        band[1] = pow * 10;

                    if (band[1] < min ||
                        band[0] > max)
                        continue;

                    band[0] = band[0] * multiplier;
                    band[1] = band[1] * multiplier;
                    output.Add(band);
                }
            }
            return output.ToArray();
        }

        /// <summary>
        /// Build a collection of bands based on log 10
        /// </summary>
        public static double?[][] RoundedOpenLogBands(double min, double max, double multiplier)
        {
            List<double?[]> output = RoundedLogBands(min, max, multiplier).ToList();
            output.FirstOrDefault()[0] = null;
            output.LastOrDefault()[1] = null;
            return output.ToArray();
        }
    }

    /// <summary>
    /// Class for managing arrays.
    /// </summary>
    public static class RandomRanges
    {
        public static DateTime[] DateTimeRange(DateTime start, DateTime end, int amount)
        {
            double totalTime = (end - start).TotalSeconds;
            Random random = new Random(DateTime.Now.Millisecond);

            List<DateTime> list = new List<DateTime>();
            for (int i = 0; i < amount; i++)
            {
                list.Add(start.AddSeconds(random.NextDouble() * totalTime));
            }
            list.Sort();
            return list.ToArray();
        }
    }
}
