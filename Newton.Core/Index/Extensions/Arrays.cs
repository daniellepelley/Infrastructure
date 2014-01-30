using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Newton.Extensions
{
    /// <summary>
    /// Class for managing arrays.
    /// </summary>
    public static class ArrayCompression
    {
        /// <summary>
        /// Checks if an item in the search array can be found in a string array.
        /// </summary>
        public static bool FoundInArray(this string[] searchArray, string[] stringArray)
        {
            return FoundInArray(searchArray, stringArray, true);
        }

        /// <summary>
        /// Checks if an item in the search array can be found in a string array.
        /// </summary>
        public static bool FoundInArray(this string[] searchArray, string[] stringArray, bool caseSensitive)
        {
            foreach (string searchItem in searchArray)
            {
                foreach (string existingItem in stringArray)
                {
                    if ((caseSensitive && searchItem == existingItem)
                        || (!caseSensitive && searchItem.ToUpper() == existingItem.ToUpper()))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if each item in an array can be found in a string array.
        /// </summary>
        public static bool FoundAllInArray(this string[] itemsToSearchFor, string[] itemsToSearch)
        {
            return FoundAllInArray(itemsToSearchFor, itemsToSearch, true);
        }

        /// <summary>
        /// Checks if each item in an array can be found in a string array.
        /// </summary>
        public static bool FoundAllInArray(this string[] itemsToSearchFor, string[] itemsToSearch, bool caseSensitive)
        {
            foreach (string searchItem in itemsToSearchFor)
            {
                bool found = false;
                foreach (string existingItem in itemsToSearch)
                {
                    if ((caseSensitive && searchItem == existingItem)
                        || (!caseSensitive && searchItem.ToUpper() == existingItem.ToUpper()))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                { return false; }
            }
            return true;
        }

        /// <summary>
        /// Checks if an item in the search array can be found in a string array.
        /// </summary>
        public static bool ContainsAny(this string[] stringArray, string[] list)
        {
            foreach (string searchItem in list)
            {
                foreach (string existingItem in stringArray)
                {
                    if (searchItem == existingItem)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if each item in an array can be found in a string array.
        /// </summary>
        public static bool ContainsAll(this string[] stringArray, string[] list)
        {
            foreach (string item in list)
            {
                bool found = false;
                foreach (string existingItem in stringArray)
                {
                    if (item == existingItem)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) { return false; }
            }
            return true;
        }

        /// <summary>
        /// Checks if each item in an array can be found in a string array.
        /// </summary>
        public static bool AreMatch(this string[] stringArray, string[] list)
        {
            return stringArray.ContainsAll(list) &&
                stringArray.Length == list.Length;
        }


        /// <summary>
        /// Wraps the text to fit the maximum line length passed.
        /// </summary>
        public static string[] Wrap(this string[] lines, int maxLength, int maximumLines)
        {
            List<string> linesOutput = new List<string>();
            foreach (string line in lines)
            {
                linesOutput.AddRange(line.Wrap(maxLength, maximumLines));
            }
            return linesOutput.ToArray();
        }

        /// <summary>
        /// Wraps the text to fit the maximum line length passed.
        /// </summary>
        public static string[] Wrap(this string[] lines, int maxLength)
        {
            return lines.Wrap(maxLength, -1);
        }

        /// <summary>
        /// Builds a regex only allowing a string to be one of the items in the list.
        /// </summary>
        public static string RegexFromList(this string[] list)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string item in list)
            {
                stringBuilder.Append(string.Format("//b{0}//b|", item));
            }
            if (stringBuilder.Length > 0)
                stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Converts an array into a single string.
        /// </summary>
        public static string ToString(this string[] passedArray, string sepeartor)
        {
            if (passedArray.Length == 0)
                return string.Empty;
            return FromArray(passedArray, false, sepeartor).RightTrim(1);
        }


        /// <summary>
        /// Converts an array into a single string.
        /// </summary>
        public static string FromArray(this string[] passedString)
        {
            return FromArray(passedString, true, string.Empty);
        }

        /// <summary>
        /// Converts an array into a single string.
        /// </summary>
        public static string FromArray(this string[] passedString, bool isMultiline)
        {
            return FromArray(passedString, isMultiline, string.Empty);
        }

        /// <summary>
        /// Converts an array into a single string.
        /// </summary>
        public static string FromArray(this string[] passedString, string endOfLineString)
        {
            return FromArray(passedString, true, endOfLineString);
        }

        /// <summary>
        /// Converts an array into a single string.
        /// </summary>
        public static string FromArray(this string[] passedString, bool isMultiline, string endOfLineString)
        {
            if (passedString == null) { return string.Empty; }
            StringBuilder stringBuilder = new StringBuilder();
            foreach (string item in passedString)
            {
                if (isMultiline)
                    stringBuilder.AppendLine(item + endOfLineString);
                else
                    stringBuilder.Append(item + endOfLineString);
            }
            return stringBuilder.ToString();
        }

        public static string[] RemoveBlanks(this string[] passedArray)
        {
            return (from a in passedArray
                    where !string.IsNullOrEmpty(a)
                    select a).ToArray();
        }

        public static List<string> RemoveBlanks(this List<string> passedList)
        {
            return (from a in passedList
                    where !string.IsNullOrEmpty(a)
                    select a).ToList();
        }

        /// <summary>
        /// Removes the passed strings from the list
        /// </summary>
        public static string[] Remove(this string[] source, string[] removeStrings)
        {
            return source.Remove(removeStrings, false);
        }

        /// <summary>
        /// Removes the passed strings from the array
        /// </summary>
        public static string[] Remove(this string[] source, string[] removeStrings, bool isCaseSensitive)
        {
            return Remove(source.ToList(), removeStrings, isCaseSensitive).ToArray();
        }

        /// <summary>
        /// Removes the passed strings from the list
        /// </summary>
        public static List<string> Remove(this List<string> source, string[] removeStrings)
        {
            return source.Remove(removeStrings, false);
        }

        /// <summary>
        /// Removes the passed strings from the list
        /// </summary>
        public static List<string> Remove(this List<string> source, string[] removeStrings, bool isCaseSensitive)
        {
            List<string> returnList = new List<string>();
            foreach (string item in source)
            {
                bool found = false;
                foreach (string removeString in removeStrings)
                {
                    if (!isCaseSensitive)
                    {
                        if (item.ToUpper() == removeString.ToUpper())
                        {
                            found = true;
                            break;
                        }
                    }
                    else if (item == removeString)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    returnList.Add(item);
                }
            }
            return returnList;
        }


        # region Convert

        /// <summary>
        /// Gets a list of the distinct values found in a string array.
        /// </summary>
        public static string[] FromString(this string str, int splitLength)
        {
            if (str == null
                || splitLength < 1) { return new string[0]; }
            List<string> returnList = new List<string>();
            for (int i = 0; i < str.Length; i = i + splitLength)
            {
                //If not yet at end segment of the string.
                if (str.Length > i + splitLength)
                {
                    returnList.Add(str.Substring(i, splitLength));
                }
                else
                {
                    returnList.Add(str.Substring(i, str.Length - i));
                    break;
                }

            }
            return returnList.ToArray();
        }



        # endregion

        # region Resizing

        ///// <summary>
        ///// Resizes an array maintaining the data contained within.
        ///// </summary>
        //public static System.Array ResizeArray(System.Array oldArray, int newSize)
        //{
        //    int oldSize = oldArray.Length;
        //    System.Type elementType = oldArray.GetType().GetElementType();
        //    System.Array newArray = System.Array.CreateInstance(elementType, newSize);
        //    int preservelength = System.Math.Min(oldSize, newSize);
        //    if (preservelength > 0)
        //        System.Array.Copy(oldArray, newArray, preservelength);
        //    return newArray;
        //}

        ///// <summary>
        ///// Adds an item to an existing array.
        ///// </summary>
        //public static System.Array ArrayAdd(System.Array oldArray, string newValue)
        //{
        //    int oldSize = oldArray.Length;
        //    System.Type elementType = oldArray.GetType().GetElementType();
        //    System.Array newArray = System.Array.CreateInstance(elementType, oldSize + 1);
        //    int preservelength = System.Math.Min(oldSize, oldSize + 1);
        //    if (preservelength > 0)
        //        System.Array.Copy(oldArray, newArray, preservelength);
        //    //newArray.SetValue(newValue, newArray.Length - 1);
        //    return newArray;
        //}

        # endregion

        # region Distinct Values

        ///// <summary>
        ///// Gets a list of the distinct values found in a string array.
        ///// </summary>
        //public static string[] GetDistinctValues(this string[] list)
        //{
        //    List<string> returnList = new List<string>();
        //    foreach (string item in list)
        //    {
        //        bool found = false;
        //        foreach (string returnItem in returnList)
        //        {
        //            if (returnItem == item)
        //            {
        //                found = true;
        //                break;
        //            }
        //        }
        //        if (!found)
        //        {
        //            returnList.Add(item);
        //        }
        //    }
        //    return returnList.ToArray();
        //}

        ///// <summary>
        ///// Gets a list of the distinct values found in a string list.
        ///// </summary>
        //public static List<string> GetDistinctValues(List<string> list)
        //{
        //    List<string> returnList = new List<string>();
        //    returnList.AddRange(GetDistinctValues(list.ToArray()));
        //    return returnList;
        //}

        # endregion

        # region Duplicate Values

        /// <summary>
        /// Gets a list of the distinct values found in a string array.
        /// </summary>
        public static string[] GetDuplicateValues(string[] list)
        {
            List<string> returnList = new List<string>();
            foreach (string item in list)
            {
                bool found = false;
                foreach (string returnItem in returnList)
                {
                    if (returnItem == item)
                    {
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    returnList.Add(item);
                }
            }
            return returnList.ToArray();
        }

        /// <summary>
        /// Gets a list of the distinct values found in a string list.
        /// </summary>
        public static List<string> GetDuplicateValues(List<string> list)
        {
            List<string> returnList = new List<string>();
            returnList.AddRange(list.ToArray().Distinct());
            return returnList;
        }

        # endregion

        # region Compress

        /// <summary>
        /// Compresses a string array into a single string.
        /// </summary>
        public static string CompressList(this string[] list)
        {
            return CompressList(list, "/", new int[0]);
        }

        /// <summary>
        /// Compresses a string array into a single string.
        /// </summary>
        public static string CompressList(this string[] list, string separator)
        {
            return CompressList(list, separator, new int[0]);
        }

        /// <summary>
        /// Compresses a string array into a single string.
        /// </summary>
        public static string CompressList(this string[] list, int[] avoidLength)
        {
            return CompressList(list, "/", avoidLength);
        }

        /// <summary>
        /// Compresses a string array into a single string.
        /// </summary>
        public static string CompressList(this string[] list, string separator, int[] avoidLength)
        {
            list = list.RemoveBlanks();

            if (list.Length == 0)
            {
                return string.Empty;
            }
           
            if ((from l in list select l.Length).Distinct().Count() > 1)
                return string.Empty;
            
            
            List<string> newList = new List<string>();
            newList.AddRange(list);
            newList.Sort();
            string returnString = newList[0];
            for (int i = 1; i < newList.Count; i++)
            {
                for (int j = 0; j < newList[i].Length; j++)
                {
                    if (newList[i - 1].Length > j)
                    {
                        if (newList[i - 1][j].ToString() != newList[i][j].ToString())
                        {

                            while (avoidLength.Contains(newList[i].Length - j) && j > 0)
                            {
                                j--;
                            }

                            returnString += separator + newList[i].Substring(j, newList[i].Length - j);
                            break;
                        }
                    }
                    else
                    {
                        returnString += separator + newList[i];
                    }
                }
            }
            return returnString;
        }

        /// <summary>
        /// Uncompresses a string into an string array.
        /// </summary>
        public static string[] UnCompressList(this string list)
        {
            return UnCompressList(list, new string[] { "/", " " });
        }

                /// <summary>
        /// Uncompresses a string into an string array.
        /// </summary>
        public static string[] UnCompressList(this string list, string separator)
        {
            return UnCompressList(list, new string[] { separator });
        }

        /// <summary>
        /// Uncompresses a string into an string array.
        /// </summary>
        public static string[] UnCompressList(this string list, string[] separators)
        {
            if (string.IsNullOrEmpty(list)) { return new string[0]; }
            List<string> returnList = new List<string>();
            string[] splitString = list.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            returnList.Add(splitString[0]);
            for (int i = 1; i < splitString.Length; i++)
            {
                if (splitString[i].Length > returnList[i - 1].Length)
                {
                    return new string[] { list};
                }
                
                returnList.Add(
                    returnList[i - 1].Substring(0, returnList[i - 1].Length - splitString[i].Length)
                    + splitString[i]);
            }
            return returnList.ToArray();
        }

        # endregion
    }
}
