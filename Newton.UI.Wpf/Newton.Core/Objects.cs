using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Newton.Extensions
{
    /// <summary>
    /// Class for managing objects.
    /// </summary>
    public static class ObjectMethods
    {


        #region Conversersion

        /// <summary>
        /// Converts a object to an integer, returning -1 if passed object
        /// is not a valid integer.
        /// </summary>
        public static int ToInteger(this object integer)
        {
            try
            {
                return Convert.ToInt32(integer);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Converts a object to a nullable integer, returning null if passed object
        /// is not a valid integer.
        /// </summary>
        public static int? ToNullableInteger(this object integer)
        {
            try
            {
                if (integer == null)
                    return null;
                return Convert.ToInt32(integer);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Converts a object to an short, returning -1 if passed object
        /// is not a valid short.
        /// </summary>
        public static short ToShort(this object integer)
        {
            try
            {
                return Convert.ToInt16(integer);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Converts a object to a nullable short, returning null if passed object
        /// is not a valid short.
        /// </summary>
        public static short? ToNullableShort(this object integer)
        {
            try
            {
                if (integer == null)
                    return null;
                return Convert.ToInt16(integer);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a object to an long, returning -1 if passed object
        /// is not a valid long.
        /// </summary>
        public static long ToLong(this object integer)
        {
            try
            {
                return Convert.ToInt64(integer);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Converts a object to a nullable long, returning null if passed object
        /// is not a valid long.
        /// </summary>
        public static long? ToNullableLong(this object integer)
        {
            try
            {
                if (integer == null)
                    return null;
                return Convert.ToInt64(integer);
            }
            catch
            {
                return null;
            }
        }









        /// <summary>
        /// Converts a object to a double, returning -1 if passed object
        /// is not a valid integer.
        /// </summary>
        public static double ToDouble(this object integer)
        {
            try
            {
                return Convert.ToDouble(integer);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Converts a object to a nullable double, returning null if passed object
        /// is not a valid integer.
        /// </summary>
        public static double? ToNullableDouble(this object integer)
        {
            try
            {
                if (integer == null)
                    return null;
                return Convert.ToDouble(integer);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a object to an decimal, returning -1 if passed object
        /// is not a valid integer.
        /// </summary>
        public static decimal ToDecimal(this object integer)
        {
            try
            {
                return Convert.ToDecimal(integer);
            }
            catch
            {
                return -1;
            }
        }

        /// <summary>
        /// Converts a object to a nullable decimal, returning null if passed object
        /// is not a valid integer.
        /// </summary>
        public static decimal? ToNullableDecimal(this object integer)
        {
            try
            {
                if (integer == null)
                    return null;
                return Convert.ToDecimal(integer);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts an object to a datetime, returning DateTime.MinValue if passed object
        /// is not a valid datetime.
        /// </summary>
        public static DateTime ToDate(this object dateObject)
        {
            try
            {
                if (dateObject == null)
                    return DateTime.MinValue;
                return Convert.ToDateTime(dateObject);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// Converts an object to a nullable datetime, returning null if passed object
        /// is not a valid datetime.
        /// </summary>
        public static DateTime? ToNullableDate(this object dateObject)
        {
            if (dateObject == null) { return null; }
            try
            {
                if (dateObject == null)
                    return null;
                return Convert.ToDateTime(dateObject);
            }
            catch
            {
                return null;
            }
        }




        #endregion

        #region Numerical

        /// <summary>
        /// Checks if a two objects are a numerical match.
        /// </summary>
        public static bool IsNumericalMatch(this object object1, object object2)
        {
            try
            {
                return (Convert.ToInt32(object1) - Convert.ToInt32(object2)) == 0;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}

