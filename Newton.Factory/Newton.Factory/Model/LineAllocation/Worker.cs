using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Factory.LineAllocation
{
    public class Worker
    {
        private ProcessStep processStep;
        /// <summary>
        /// The process step the worker belongs to
        /// </summary>
        public ProcessStep ProcessStep
        {
            get { return processStep; }
            set { processStep = value; }
        }
    }

    public class ProcessStep
    {
        private double unitsPerWorkerPerUnitOfTime;

        public double UnitsPerWorkerPerUnitOfTime
        {
            get { return unitsPerWorkerPerUnitOfTime; }
            set { unitsPerWorkerPerUnitOfTime = value; }
        }
    }

    public static class Functions
    {
        public static ProcessStep Next(IEnumerable<Worker> workers, IEnumerable<ProcessStep> processSteps)
        {
            Weightings<ProcessStep> weightings = CreateWeightings(workers, processSteps);
            return weightings.Lowest().Item;
        }

        public static Weightings<ProcessStep> CreateWeightings(IEnumerable<Worker> workers, IEnumerable<ProcessStep> processSteps)
        {
            Weightings<ProcessStep> weightings = new Weightings<ProcessStep>(processSteps);

            foreach (var processStep in weightings.Items)
            {
                processStep.Value = workers
                    .Where(w => w.ProcessStep == processStep.Item)
                    .Select(w => w.ProcessStep.UnitsPerWorkerPerUnitOfTime)
                    .Sum();
            }
            return weightings;
        }
    }

    public class Weightings<T>
    {
        private List<Weighting<T>> items = new List<Weighting<T>>();

        public List<Weighting<T>> Items
        {
            get { return items; }
            set { items = value; }
        }

        public Weightings(IEnumerable<T> items)
        {
            this.Items.AddRange(items.Select(i => new Weighting<T>() { Item = i }));
        }

        public Weighting<T> Highest()
        {
            return items
                .OrderByDescending(i => i.Value)
                .FirstOrDefault();
        }

        public Weighting<T> Lowest()
        {
            return items
                .OrderBy(i => i.Value)
                .FirstOrDefault();
        }
    }

    public class Weighting<T>
    {
        private T item;

        public T Item
        {
            get { return item; }
            set { item = value; }
        }

        private double value;

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
