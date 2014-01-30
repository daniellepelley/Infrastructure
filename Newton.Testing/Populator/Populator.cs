using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Newton.Testing
{
    /// <summary>
    /// Populates an object with test data
    /// </summary>
    public class Populator
    {
        #region Properties

        private Random random = new Random(DateTime.Now.Millisecond);

        private List<IFieldPopulator> populators = new List<IFieldPopulator>();

        public List<IFieldPopulator> Populators
        {
            get { return populators; }
        }

        //private IFieldPopulator stringFieldPopulator = new StringFieldPopulator();
        ///// <summary>
        ///// Populates fields with auto generated strings
        ///// </summary>
        //public IFieldPopulator StringFieldPopulator
        //{
        //    get { return stringFieldPopulator; }
        //    set { stringFieldPopulator = value; }
        //}

        //private IFieldPopulator intFieldPopulator = new IntFieldPopulator();
        ///// <summary>
        ///// Populates fields with auto generated ints
        ///// </summary>
        //public IFieldPopulator IntFieldPopulator
        //{
        //    get { return intFieldPopulator; }
        //    set { intFieldPopulator = value; }
        //}

        //private IFieldPopulator dateFieldPopulator = new DateFieldPopulator();
        ///// <summary>
        ///// Populates fields with auto generated dates
        ///// </summary>
        //public IFieldPopulator DateFieldPopulator
        //{
        //    get { return dateFieldPopulator; }
        //    set { dateFieldPopulator = value; }
        //}

        #endregion

        public Populator()
        {
            populators.Add(new RandomNullPopulator(new StringFieldPopulator(), 0.2));
            populators.Add(new RandomNullPopulator(new IntFieldPopulator(), 0.2));
            populators.Add(new RandomNullPopulator(new DateFieldPopulator(), 0.2));
        }

        #region Methods

        /// <summary>
        /// Creates an object of type T and populates it with random data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T Create<T>()
        {
            T item = (T)typeof(T).GetConstructor(new Type[0]).Invoke(null);
            return Populate<T>(item);
        }

        /// <summary>
        /// Populates item T with random data
        /// </summary>
        public T Populate<T>(T item)
        {
            foreach (var property in item.GetType().GetProperties().Where(p => p.CanWrite))
            {
                Type propertyType = property.PropertyType;
                if (propertyType.IsGenericType &&
                    propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyType = propertyType.GetGenericArguments()[0];
                }


                foreach (var populator in populators)
                {
                    if (populator.CheckType(propertyType))
                    {
                        object value = populator.Create(random);

                        if (value != null)
                        {
                            try
                            {
                                property.SetValue(item, Convert.ChangeType(value, propertyType), null);
                            }
                            catch { }
                        }
                        break;
                    }
                }
            }
            return item;
        }

        #endregion
    }

    public class StringFieldPopulator : IFieldPopulator
    {
        private int minLength = 5;

        public int MinLength
        {
            get { return minLength; }
            set { minLength = value; }
        }

        private int maxLength = 20;

        public int MaxLength
        {
            get { return maxLength; }
            set { maxLength = value; }
        }

        public StringFieldPopulator()
        { }

        public object Create(Random random)
        {
            var length = MaxLength - random.Next(MaxLength - MinLength);

            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < length; i++)
                stringBuilder.Append((char)(97 + random.Next(26)));

            return stringBuilder.ToString();
        }

        public bool CheckType(Type type)
        {
            return type == typeof(string);
        }
    }

    public class IntFieldPopulator : IFieldPopulator
    {
        private int minValue;

        public int MinValue
        {
            get { return minValue; }
            set { minValue = value; }
        }

        private int maxValue = 100;

        public int MaxValue
        {
            get { return maxValue; }
            set { maxValue = value; }
        }

        public IntFieldPopulator()
        { }

        public object Create(Random random)
        {
            return MaxValue - random.Next(MaxValue - MinValue);
        }

        public bool CheckType(Type type)
        {
            return
                type == typeof(Byte?) ||
                type == typeof(Byte) ||
                type == typeof(Int16?) ||
                type == typeof(Int16) ||
                type == typeof(Int32?) ||
                type == typeof(Int32) ||
                type == typeof(Int64?) ||
                type == typeof(Int64);
        }
    }

    public class DateFieldPopulator : IFieldPopulator
    {
        private int range = 30;

        public int Range
        {
            get { return range; }
            set { range = value; }
        }

        public DateFieldPopulator()
        { }

        public object Create(Random random)
        {
            DateTime dateTime = DateTime.Now;

            return DateTime.Now
                .AddMonths(random.Next(Range * 2) - Range)
                .AddDays(random.Next(Range * 2) - Range)
                .AddHours(random.Next(Range * 2) - Range)
                .AddMinutes(random.Next(Range * 2) - Range)
                .AddSeconds(random.Next(Range * 2) - Range);
        }


        public bool CheckType(Type type)
        {
            return
                type == typeof(DateTime?) ||
                type == typeof(DateTime);
        }
    }
}
