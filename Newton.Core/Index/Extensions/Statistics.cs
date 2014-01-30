using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using Pluto.Framework;
//using Pluto.BI;

using Newton.Core;

namespace Newton.Extensions
{
    public static class Statistics
    {
        /// <summary>
        /// Calculates the mean of a collection of values
        /// </summary>
        public static double? Mean(this IEnumerable<double> source)
        {
            if (source == null ||
                source.Count() == 0)
            {
                return null;
            }
            return source.Average();
        }

        /// <summary>
        /// Calculates the mean of a collection of values
        /// </summary>
        public static double? Mean(this IEnumerable<double?> source)
        {
            if (source == null ||
                source.Count() == 0)
            {
                return null;
            }
            return source.RemoveNulls().Average();
        }

        /// <summary>
        /// Calculates the mean of a collection of values
        /// </summary>
        public static double? Mean(this IEnumerable<int> source)
        {
            if (source == null ||
                source.Count() == 0)
            {
                return null;
            }
            return source.Average();
        }

        ///// <summary>
        ///// Gets the indexed average for the passed records
        ///// </summary>
        //public static double Index<T, TKey>(this IEnumerable<T> records, Func<T, double> measure, Func<T, TKey> indexSelector, IGrouping<TKey, T> controlGroup)
        //{
        //    return records.Index(measure, indexSelector, records.GroupBy(indexSelector).AsWeightings());
        //}

        /// <summary>
        /// Gets the indexed average for the passed records
        /// </summary>
        public static double? Index<T, TKey>(this IEnumerable<T> records, Func<T, double> measure, Func<T, TKey> indexSelector, Dictionary<TKey, double> weightings)
        {
            var groups = records.GroupBy(indexSelector);

            Index index = new Index();

            //Splits the records into the same grouping as the control weightings
            foreach (var group in groups)
            {
                double controlWeighting;

                if (weightings.ContainsKey(group.Key))
                {
                    controlWeighting = weightings[group.Key];
                }
                else
                {
                    controlWeighting = weightings.Values.Average();
                }

                //Adds the measure for the group and the control weighting for the group
                index.Items.Add(new IndexItem(group.Select(measure).Average(), controlWeighting));
            }
            return index.Average();
        }

        /// <summary>
        /// Gets the indexed average for the passed records
        /// </summary>
        public static double? Index<T, TKey>(this IEnumerable<IGrouping<TKey, T>> groups, Func<T, double> measure, Dictionary<TKey, double> weightings)
        {
            Index index = new Index();

            //Splits the records into the same grouping as the control weightings
            foreach (var group in groups)
            {
                double controlWeighting;

                if (weightings.ContainsKey(group.Key))
                {
                    controlWeighting = weightings[group.Key];
                }
                else
                {
                    controlWeighting = weightings.Values.Average();
                }

                if (group.Count() == 0)
                    continue;

                //Adds the measure for the group and the control weighting for the group
                index.Items.Add(new IndexItem(group.Select(measure).Average(), controlWeighting));
            }
            return index.Average();
        }

        ///// <summary>
        ///// Gets the indexed average for the passed records
        ///// </summary>
        //public static double? CubicIndex<T, TKey>(this IEnumerable<IGrouping<TKey, T>> groups, Func<T, double> measure, IEnumerable<Func<T, string>> dimensions)
        //{
        //    //Calculate the multi-dimensional weightings
        //    Dictionary<int[], double> weightings = new Dictionary<int[], double>();

        //    foreach (var dimension in dimensions)
        //    {

        //    }


        //    //Split the records, calculate the index


        //    Index index = new Index();

        //    //Splits the records into the same grouping as the control weightings
        //    foreach (var group in groups)
        //    {
        //        double controlWeighting;

        //        if (weightings.ContainsKey(group.Key))
        //        {
        //            controlWeighting = weightings[group.Key];
        //        }
        //        else
        //        {
        //            controlWeighting = weightings.Values.Average();
        //        }

        //        if (group.Count() == 0)
        //            continue;

        //        //Adds the measure for the group and the control weighting for the group
        //        index.Items.Add(new IndexItem(group.Select(measure).Average(), controlWeighting));
        //    }
        //    return index.Average();
        //}



        ///// <summary>
        ///// Gets the indexed average for the passed records
        ///// </summary>
        //public static double Index<T, TKey>(this IEnumerable<IGrouping<TKey, T>> groups, Func<T, double?> measure, Dictionary<TKey, double> weightings)
        //{
        //    Index index = new Index();

        //    //Splits the records into the same grouping as the control weightings
        //    foreach (var group in groups)
        //    {
        //        double controlWeighting;

        //        if (weightings.ContainsKey(group.Key))
        //        {
        //            controlWeighting = weightings[group.Key];
        //        }
        //        else
        //        {
        //            controlWeighting = weightings.Values.Average();
        //        }

        //        //Adds the measure for the group and the control weighting for the group

        //        double? mean = group.Select(measure).Mean();

        //        if (mean.HasValue)
        //        {
        //            index.Items.Add(new IndexItem(mean.Value, controlWeighting));
        //        }
        //    }
        //    return index.Average();
        //}

        //public static SingleResult<IEnumerable<T>, double>[] Indexes<T>(this IEnumerable<T> records, Func<T, string> keySelector, Func<T, double> measure, Func<T, string> indexSelector, Dictionary<string, double> controlWeightings)
        //{
        //    List<SingleResult<IEnumerable<T>, double>> list = new List<SingleResult<IEnumerable<T>, double>>();

        //    var groups = records.GroupBy(keySelector);

        //    foreach (var gr in groups)
        //    {
        //        double? index = gr.Index(measure, indexSelector, controlWeightings);
                
        //        if (index.HasValue)
        //            list.Add(new SingleResult<IEnumerable<T>,double>(gr.ToArray(), index.Value));
        //    }

        //    return list.ToArray();
        //}

        //public static SingleResult<IEnumerable<T>, double>[] Indexes<T>(this IEnumerable<T> records, Func<T, string> keySelector, Func<T, string> indexSelector, Func<T, double> measure)
        //{
        //    Dictionary<string, double> controlWeightings = records.GroupBy(indexSelector).AsWeightings();

        //    return Indexes(records, keySelector, measure, indexSelector, controlWeightings);
        //}

        /// <summary>
        /// Creates a dictionary of control weightings based on a collection of IGrouping
        /// </summary>
        public static Dictionary<TKey, double> AsWeightings<TKey, T>(this IEnumerable<IGrouping<TKey, T>> groups)
        {
            Dictionary<TKey, double> controlWeightings = new Dictionary<TKey, double>();

            double total = groups.Select(g => g.Count()).Sum();

            foreach (var group in groups)
            {
                controlWeightings.Add(group.Key, group.Count() / total);
            }

            return controlWeightings;
        }

        /// <summary>
        /// Calculates the mean of a collection of values
        /// </summary>
        public static double? Median(this IEnumerable<double> source)
        {
            if (source == null ||
                source.Count() == 0)
            {
                return null;
            }

            int count = source.Count();

            if (count.IsEven())
            {
                return source.OrderBy(s => s).Skip((count / 2) -1).Take(2).Average();
            }
            else
            {
                return source.OrderBy(s => s).Skip(count / 2).FirstOrDefault();
            }
        }

        /// <summary>
        /// The difference between the highest and lowest value
        /// </summary>
        public static double? Range(this IEnumerable<double> source)
        {
            if (source == null ||
                source.Count() == 0)
            {
                return null;
            }
            return source.Max() - source.Min();
        }

        /// <summary>
        /// Returns the rolling mean of a collection of doubles
        /// </summary>
        public static double[] RollingMeans(this IEnumerable<double> source, int range)
        {
            List<double> output = new List<double>();

            for (int i = 0; i < source.Count(); i++)
            {
                output.Add(RollingMean(source, i, range));
            }
            return output.ToArray();
        }

        /// <summary>
        /// Returns the mean of the range around the indexed value.
        /// </summary>
        public static double RollingMean(this IEnumerable<double> source, int index, int range)
        {
            int tempRange = Math.Min(Math.Min(index, range), source.Count() - index);
            return source.Skip(index - tempRange).Take(1 + tempRange * 2).Average(d => d);
        }

        /// <summary>
        /// The right skew, the difference between the mean and the median
        /// </summary>
        public static double? RightSkew(this IEnumerable<double> source)
        {
            return source.Mean() / source.Median();
        }

        /// <summary>
        /// The left skew, the difference between the mean and the median
        /// </summary>
        public static double? LeftSkew(this IEnumerable<double> source)
        {
            return source.Median() / source.Mean();
        }

        /// <summary>
        /// The skew, the difference between the mean and the median
        /// </summary>
        public static double? Skew(this IEnumerable<double> source)
        {
            return 1 - source.LeftSkew();
        }

        /// <summary>
        /// The skew, the difference between the mean and the median
        /// </summary>
        public static double CyhelskySkew(this IEnumerable<double> source)
        {
            return ((double)source.BelowAverage().Count() - (double)source.AboveAverage().Count()) / (double)source.Count();
        }

        /// <summary>
        /// Returns the values that are above the average
        /// </summary>
        public static double[] AboveAverage(this IEnumerable<double> source)
        {
            double average = source.Average();
            return source.Where(s => s > average).ToArray();
        }

        /// <summary>
        /// Returns the values that are below the average
        /// </summary>
        public static double[] BelowAverage(this IEnumerable<double> source)
        {
            double average = source.Average();
            return source.Where(s => s < average).ToArray();
        }

        /// <summary>
        /// The variance of a collection of values
        /// </summary>
        public static double? Variance(this IEnumerable<double> source)
        {
            if (source.Count() < 2)
                return null;

            double? mean = source.Mean();

            if (!mean.HasValue)
                return null;

            return (from d in source
                    select Math.Pow(mean.Value - d, 2)).Sum() / (source.Count() - 1);
        }

        /// <summary>
        /// The standard deviation of a collection of values
        /// </summary>
        public static double? StandardDeviation(this IEnumerable<double> source)
        {
            double? variance = source.Variance();

            if (!variance.HasValue)
                return null;

            return Math.Sqrt(variance.Value);
        }

        /// <summary>
        /// The standard error of a collection of values
        /// </summary>
        public static double? StandardError(this IEnumerable<double> source)
        {
            if (source.Count() == 0)
                return null;
            
            double? variance = source.Variance();

            if (!variance.HasValue)
                return null;

            return Math.Sqrt(variance.Value / source.Count());
        }

        /// <summary>
        /// The standard error of a collection of values
        /// </summary>
        public static double? StandardError(double variance, double numberOfItems)
        {
            if (variance == 0 ||
                numberOfItems == 0)
            {
                return null;
            }

            return Math.Sqrt(variance / numberOfItems);
        }

        /// <summary>
        /// Returns 1 sigma control difference
        /// </summary>
        public static double ControlDifference1Sigma(double standardDeviation, int numberOfRecords)
        {
            return standardDeviation / Math.Sqrt(numberOfRecords);
        }

        /// <summary>
        /// Returns the 2 sigma lower control limit
        /// </summary>
        public static double LowerControlLimit2Sigma(double mean, double standardDeviation, int numberOfRecords)
        {
            return mean - (2 * ControlDifference1Sigma(standardDeviation, numberOfRecords));
        }

        /// <summary>
        /// Returns the 2 sigma lower control limit
        /// </summary>
        public static double UpperControlLimit2Sigma(double mean, double standardDeviation, int numberOfRecords)
        {
            return mean + (2 * ControlDifference1Sigma(standardDeviation, numberOfRecords));
        }

        /// <summary>
        /// Returns the 3 sigma lower control limit
        /// </summary>
        public static double LowerControlLimit3Sigma(double mean, double standardDeviation, int numberOfRecords)
        {
            return mean - (3 * ControlDifference1Sigma(standardDeviation, numberOfRecords));
        }

        /// <summary>
        /// Returns the 3 sigma lower control limit
        /// </summary>
        public static double UpperControlLimit3Sigma(double mean, double standardDeviation, int numberOfRecords)
        {
            return mean + (3 * ControlDifference1Sigma(standardDeviation, numberOfRecords));
        }

        /// <summary>
        /// Returns the lower control limit
        /// </summary>
        public static double LowerControlLimit(double mean, double standardDeviation, int numberOfRecords, double numberSigma)
        {
            return mean - (numberSigma * ControlDifference1Sigma(standardDeviation, numberOfRecords));
        }

        /// <summary>
        /// Returns the upper control limit
        /// </summary>
        public static double UpperControlLimit(double mean, double standardDeviation, int numberOfRecords, double numberSigma)
        {
            return mean + (numberSigma * ControlDifference1Sigma(standardDeviation, numberOfRecords));
        }

        /// <summary>
        /// Returns the lower 95% confidence limit
        /// </summary>
        public static double? LowerConfidenceLimit(double? mean, double? variance, int numberOfRecords)
        {
            if (!variance.HasValue ||
                !mean.HasValue)
            {
                return null;
            }

            return mean - (1.96 * Math.Sqrt((variance.Value / numberOfRecords)));
        }

        /// <summary>
        /// Returns the upper 95% confidence limit
        /// </summary>
        public static double? UpperConfidenceLimit(double? mean, double? variance, int numberOfRecords)
        {
            if (!variance.HasValue ||
                !mean.HasValue)
            {
                return null;
            }

            return mean.Value + (1.96 * Math.Sqrt((variance.Value / numberOfRecords)));
        }

        /// <summary>
        /// Returns the lower 95% confidence limit
        /// </summary>
        public static double? LowerConfidenceLimit(IEnumerable<double> values, double? variance)
        {
            if (!variance.HasValue)
            {
                return null;
            }

            return LowerConfidenceLimit(values.Mean(), variance.Value, values.Count());
        }

        /// <summary>
        /// Returns the upper 95% confidence limit
        /// </summary>
        public static double? UpperConfidenceLimit(IEnumerable<double> values, double? variance)
        {
            if (!variance.HasValue)
            {
                return null;
            }

            return UpperConfidenceLimit(values.Mean(), variance, values.Count());
        }

        /// <summary>
        /// Checks the sample
        /// </summary>
        public static bool WithinConfidenceLimits(double mean, double variance, IEnumerable<double> values)
        {
            return mean > LowerConfidenceLimit(values, variance) &&
                mean < UpperConfidenceLimit(values, variance);
        }

        /// <summary>
        /// Returns the ratio that will sit within k x the standard deviation
        /// </summary>
        public static double ChebysheffRatio(this double k)
        {
            return 1 - (1 / (Math.Pow(k, 2)));
        }

        /// <summary>
        /// Returns the ratio that will sit within k x the standard deviation
        /// </summary>
        public static double ReserveChebysheffRatio(this double probability)
        {
            return System.Math.Sqrt(1 / (1 - probability));
        }

        public static double LowerQuartile(this IEnumerable<double> source)
        {
            return source.GetSigma(4, 1);
        }

        public static double UpperQuartile(this IEnumerable<double> source)
        {
            return source.GetSigma(4, 3);
        }

        public static double Percentile(this IEnumerable<double> source, double percentage)
        {
            return source.GetSigma(100, percentage);
        }

        public static double[] PercentileTrim(this IEnumerable<double> source, double percentage)
        {
            return source.PercentileTrim(percentage, d => d);
        }

        public static T[] PercentileTrim<T>(this IEnumerable<T> source, double percentage, Func<T, T> keySelector)
        {
            int index = (int)((double)source.Count() * percentage) / 100;
            return source.OrderBy(keySelector).Skip(index).Take(source.Count() - (2 * index)).ToArray();
        }

        public static double GetSigma(this IEnumerable<double> source, double sigmaCount, double sigmaIndex)
        {
            double ratio = sigmaIndex * (1 / sigmaCount);
            return source.Interpol(ratio * (source.Count() + 1));
        }

        public static double Interpol(this IEnumerable<double> source, double index)
        {
            IEnumerable<double> orderedSource = source.OrderBy(s => s).ToArray();
            int wholeNumber = (int)Math.Truncate(index);

            double dec = index.GetDecimal();

            if (dec == 0)
            {
                return orderedSource.Skip(wholeNumber - 1).FirstOrDefault();
            }
            else
            {
                double value1 = orderedSource.Skip(wholeNumber - 1).FirstOrDefault();
                double value2 = orderedSource.Skip(wholeNumber).FirstOrDefault();

                return value1 + ((value2 - value1) * dec);
            }
        }

        /// <summary>
        /// The factorial of an integer (factorial 4 = 1 x 2 x 3 x 4)
        /// </summary>
        public static int Factorial(this int value)
        {
            int sum = 1;
            for (int i = 1; i <= value; i++)
                sum *= i;

            return sum;
        }

        /// <summary>
        /// The factorial between 2 integers
        /// </summary>
        public static int Factorial(int minValue, int maxValue)
        {
            int sum = 1;
            for (int i = minValue; i <= maxValue; i++)
                sum *= i;

            return sum;
        }


        /// <summary>
        /// The probability of the variable 
        /// </summary>
        public static double PoissonProbability(double average, int variable)
        {
            return
                (Math.Pow(average, variable) *
                Math.Pow(Math.E, (double)-1 * average))
                / variable.Factorial(); 
        }

        /// <summary>
        /// The probability of the variable 
        /// </summary>
        public static double PoissonProbabilityLessThan(double average, int variable)
        {
            double sum = 0;
            for (int i = 0; i < variable; i++)
                sum += PoissonProbability(average, i);

            return sum;
        }

        /// <summary>
        /// The probability of the variable 
        /// </summary>
        public static double PoissonProbabilityMoreThan(double average, int variable)
        {
            return 1 - PoissonProbabilityLessThan(average, variable + 1);
        }

        /// <summary>
        /// Counts the number of outcomes.
        /// </summary>
        public static int BiNominalOutcomes(int total, int chosen)
        {
            return
                Factorial(chosen + 1, total)
                /
                Factorial(1, total - chosen);
        }

        public static double BiNominalProbability(double probability, int total, int outcome)
        {
            return
                BiNominalOutcomes(total, outcome) *
                Math.Pow(probability, outcome) *
                Math.Pow((1 - probability), (total - outcome));
        }

        /// <summary>
        /// The probability of the variable 
        /// </summary>
        public static double BiNominalProbabilityLessThan(double probability, int total, int variable)
        {
            double sum = 0;
            for (int i = 0; i < variable; i++)
                sum += BiNominalProbability(probability, total, i);

            return sum;
        }

        /// <summary>
        /// The probability of the variable 
        /// </summary>
        public static double BiNominalProbabilityMoreThan(double probability, int total, int variable)
        {
            return 1 - BiNominalProbabilityLessThan(probability, total, variable + 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="u">Mean</param>
        /// <param name="o">Variance</param>
        /// <param name="x">X</param>
        /// <returns></returns>
        public static double NormalDistribution(double mean, double variance, double x)
        {
            return
                (1 / Math.Sqrt(2 * Math.PI * Math.Pow(variance, 2)))
                * (Math.Pow(
                Math.E,
                (-1 * Math.Pow((x - mean), 2)
                /
                (2 * Math.Pow(variance, 2))))); 
        }

        public static double ExponentialDistribution(double mean, double time)
        {
            return System.Math.Pow((System.Math.E), mean * time * -1);
        }

        /// <summary>
        /// Creates a pascals triangle using the passed number of levels and the unit, as a default this is 1.
        /// </summary>
        public static double[] CreatePascalsTriangle(int number)
        {
            return CreatePascalsTriangle(number, 1);
        }

        /// <summary>
        /// Creates a pascals triangle using the passed number of levels and the unit, typically this would be 1.
        /// </summary>
        public static double[] CreatePascalsTriangle(int levels, double unit)
        {
            List<double> results = new List<double>();

            results.Add(unit);

            for (int i = 1; i < levels; i++)
            {
                List<double> oldResults = new List<double>();
                oldResults.Add(0);
                oldResults.AddRange(results);
                oldResults.Add(0);

                results.Clear();

                for (int j = 0; j < oldResults.Count - 1; j++)
                {
                    results.Add(oldResults[j] + oldResults[j + 1]);
                }
            }
            return results.ToArray();
        }

        /// <summary>
        /// Creates a pascals triangle using the passed number of levels and the total volume of items.
        /// </summary>
        public static double[] CreatePascalsTriangleFromVolume(int levels, int volume)
        {
            return CreatePascalsTriangle(levels, (volume / Math.Pow(2, levels)) * 2);
        }

        public static double[] BellCurveX(this IEnumerable<double> results, int segments)
        {
            List<double> list = new List<double>();

            List<double> ranges = new List<double>();

            ranges.Add(results.Min());
            ranges.AddRange(Newton.Core.Ranges.DoubleRange(results.Min() - 1, results.Max() + 1, segments));

            for (int i = 0; i < ranges.Count - 1; i++)
            {
                int count = (from r in results
                             where r > ranges[i] &&
                             r < ranges[i + 1]
                             select r).Count();

                list.Add(count);
            }
            return list.ToArray();
        }

        public static double[] BellCurveX(this IEnumerable<double> results, int segments, int range)
        {
            return BellCurveX(results, segments).RollingMeans(range);
        }

        public static double[][] BellCurveXY(double[] results, int segments)
        {
            List<double[]> list = new List<double[]>();

            double[] ranges = Newton.Core.Ranges.DoubleRange(results.Min(), results.Max(), segments);

            for (int i = 0; i < ranges.Length - 1; i++)
            {
                double[] result = new double[2];

                int count = (from r in results
                             where r > ranges[i] &&
                             r < ranges[i + 1]
                             select r).Count();

                result[0] = ranges[i];
                result[1] = count;
                list.Add(result);
            }
            return list.ToArray();
        }


    }
}
