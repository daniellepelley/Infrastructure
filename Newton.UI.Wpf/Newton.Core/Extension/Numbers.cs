using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluto.Extensions
{
    public static class Numbers
    {
        #region Rounding

        /// <summary>
        /// Rounds the number to the number of decimal places
        /// </summary>
        public static double? Round(this double? value, int decimals)
        {
            if (value.HasValue)
                return (Math.Round(value.Value, decimals));
            return null;
        }

        #endregion

        /// <summary>
        /// Comes up with all ways to distribute the passed number
        /// </summary>
        /// <example>
        /// 4 would return 4, 2+2, 3+1, 1+1+2, 1+1+1+1
        /// </example>
        public static List<List<int>> EnumerateAll(this int source)
        {
            /* Fastest known algorithim for enumerating partitons
             * (not counting the re-ordering that I added)
             * Based on the Python code from http://homepages.ed.ac.uk/jkellehe/partitions.php
             */
            List<List<int>> lst = new List<List<int>>();
            int[] aa = new int[source + 1];
            List<int> a = new List<int>(aa.ToList<int>());
            List<int> tmp;
            int k = 1;

            a[0] = 0;
            int y = source - 1;

            while (k != 0)
            {
                int x = a[k - 1] + 1;
                k -= 1;
                while (2 * x <= y)
                {
                    a[k] = x;
                    y -= x;
                    k += 1;
                }
                int l = k + 1;
                while (x <= y)
                {
                    a[k] = x;
                    a[l] = y;

                    // copy just the part that we want
                    tmp = (new List<int>(a.GetRange(0, k + 2)));

                    // insert at the beginning to return partions in the expected order
                    lst.Insert(0, tmp);
                    x += 1;
                    y -= 1;
                }
                a[k] = x + y;
                y = x + y - 1;

                // copy just the part that we want
                tmp = (new List<int>(a.GetRange(0, k + 1)));

                // insert at the beginning to return partions in the expected order
                lst.Insert(0, tmp);
            }

            return lst;
        }

        /// <summary>
        /// Returns a percentage of the total.
        /// </summary>
        public static int PercentageToNumber(this int percentage, int total)
        {
            return (int)Convert.ToDouble(percentage).PercentageToNumber((double)total); //(int)(Convert.ToDouble(total) * ((double)percentage / 100));
        }

        /// <summary>
        /// Returns a percentage of the total.
        /// </summary>
        public static double PercentageToNumber(this double percentage, double total)
        {
            return total * (percentage / 100);
        }

        /// <summary>
        /// Returns a percentage of the total.
        /// </summary>
        public static int NumberToPercentage(this int number, int total)
        {
            return (int)Convert.ToDouble(number).PercentageToNumber((double)total); //(int)(Convert.ToDouble(total) * ((double)percentage / 100));
        }

        /// <summary>
        /// Returns a percentage of the total.
        /// </summary>
        public static double NumberToPercentage(this double number, double total)
        {
            if (total == 0)
                return 0;
            return number * 100 / total;
        }

        /// <summary>
        /// Splits the number into an array simular to a for loop 
        /// </summary>
        /// <example>
        /// An integer of 5 would convert into int[] { 0, 1, 2, 3, 4 }
        /// </example>
        public static int[] Split(this int number)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < number; i++)
                list.Add(i);
            return list.ToArray();
        }

        /// <summary>
        /// Checks whether the value is even
        /// </summary>
        public static bool IsEven(this int source)
        {
            return source % 2 == 0;
        }

        /// <summary>
        /// Checks whether the value is odd
        /// </summary>
        public static bool IsOdd(this int source)
        {
            return source % 2 == 1;
        }

        /// <summary>
        /// Checks whether the value is a whole number
        /// </summary>
        public static bool IsWholeNumber(this double source)
        {
            return (Math.Round(source, 0) == source);
        }

        /// <summary>
        /// Checks whether the value is a whole number
        /// </summary>
        public static double GetDecimal(this double source)
        {
            return source - Math.Truncate(source);
        }

    }
}