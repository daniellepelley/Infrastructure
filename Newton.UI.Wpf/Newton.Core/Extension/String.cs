using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace Pluto.Extensions
{
    /// <summary>
    /// Class for managing strings.
    /// </summary>
    public static class StringMethods
    {
        /// <summary>
        /// Strings used to split strings
        /// </summary>
        private static string[] SplitStrings
        {
            get
            {
                return new string[] {
                "|",
                "\\",
                ",",
                ".",
                "<",
                ">",
                "/",
                "?",
                ":",
                ";",
                "'",
                "@",
                "#",
                "~",
                "{",
                "[",
                "]",
                "}",
                "=",
                "+",
                "_",
                "-",
                ")",
                "(",
                "*",
                "&",
                "^",
                "%",
                "$",
                "£",
                "!",
                "`",
                "¬" };
            }
        }

        /// <summary>
        /// If this datetime in null, will set this to the passed datetime.
        /// </summary>
        public static string Overlay(this string thisString, string str)
        {
            if (string.IsNullOrEmpty(thisString))
                return str;
            else
                return thisString;
        }

        #region Conversersion

        /// <summary>
        /// Converts a string to an integer, returning -1 if passed string
        /// is not a valid integer.
        /// </summary>
        public static int ToInteger(this string integer)
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
        /// Converts a string to a nullable integer, returning null if passed string
        /// is not a valid integer.
        /// </summary>
        public static int? ToNullableInteger(this string integer)
        {
            try
            {
                if (string.IsNullOrEmpty(integer))
                    return null;
                return Convert.ToInt32(integer);
            }
            catch
            {
                return null;
            }
        }


        /// <summary>
        /// Converts a string to an short, returning -1 if passed string
        /// is not a valid short.
        /// </summary>
        public static short ToShort(this string integer)
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
        /// Converts a string to a nullable short, returning null if passed string
        /// is not a valid short.
        /// </summary>
        public static short? ToNullableShort(this string integer)
        {
            try
            {
                if (string.IsNullOrEmpty(integer))
                    return null;
                return Convert.ToInt16(integer);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a string to an long, returning -1 if passed string
        /// is not a valid long.
        /// </summary>
        public static long ToLong(this string integer)
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
        /// Converts a string to a nullable long, returning null if passed string
        /// is not a valid long.
        /// </summary>
        public static long? ToNullableLong(this string integer)
        {
            try
            {
                if (string.IsNullOrEmpty(integer))
                    return null;
                return Convert.ToInt64(integer);
            }
            catch
            {
                return null;
            }
        }









        /// <summary>
        /// Converts a string to a double, returning -1 if passed string
        /// is not a valid integer.
        /// </summary>
        public static double ToDouble(this string integer)
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
        /// Converts a string to a nullable double, returning null if passed string
        /// is not a valid integer.
        /// </summary>
        public static double? ToNullableDouble(this string integer)
        {
            try
            {
                if (string.IsNullOrEmpty(integer))
                    return null;
                return Convert.ToDouble(integer);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a string to an decimal, returning -1 if passed string
        /// is not a valid integer.
        /// </summary>
        public static decimal ToDecimal(this string integer)
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
        /// Converts a string to a nullable decimal, returning null if passed string
        /// is not a valid integer.
        /// </summary>
        public static decimal? ToNullableDecimal(this string integer)
        {
            try
            {
                if (string.IsNullOrEmpty(integer))
                    return null;
                return Convert.ToDecimal(integer);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Converts a string to a datetime, returning DateTime.MinValue if passed string
        /// is not a valid datetime.
        /// </summary>
        public static DateTime ToDate(this string dateString)
        {
            DateTime outDate;

            if (string.IsNullOrEmpty(dateString))
                return DateTime.MinValue;

            if (DateTime.TryParse(dateString, out outDate))
                return outDate;
            return DateTime.MinValue;
        }

        /// <summary>
        /// Converts a string to a datetime, returning DateTime.MinValue if passed string
        /// is not a valid datetime.
        /// </summary>
        public static DateTime ToDate(this string dateString, string format)
        {
            DateTime outDate;

            if (string.IsNullOrEmpty(dateString))
                return DateTime.MinValue;

            if (DateTime.TryParseExact(
                dateString,
                format,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AdjustToUniversal,
                out outDate))
            {
                return outDate;
            } 
            return DateTime.MinValue;
        }

        /// <summary>
        /// Converts a string to a nullable datetime, returning null if passed string
        /// is not a valid datetime.
        /// </summary>
        public static DateTime? ToNullableDate(this string dateString)
        {
            DateTime outDate;

            if (string.IsNullOrEmpty(dateString))
                return null;

            if (DateTime.TryParse(dateString, out outDate))
                return outDate;
            return null;
        }

        /// <summary>
        /// Converts a string to a nullable datetime, returning null if passed string
        /// is not a valid datetime.
        /// </summary>
        public static DateTime? ToNullableDate(this string dateString, string format)
        {
            DateTime outDate;

            if (string.IsNullOrEmpty(dateString))
                return null;

            if (DateTime.TryParseExact(
                dateString,
                format,
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.AdjustToUniversal,
                out outDate))
            {
                return outDate;
            }
            return null;
        }

        #endregion

        #region Comparison

        /// <summary>
        /// Computes the distance between two strings.
        /// </summary>
        public static int Distance(this string thisStr, string str)
        {
            int n = thisStr.Length;
            int m = str.Length;
            int[,] d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0)
            {
                return m;
            }

            if (m == 0)
            {
                return n;
            }

            // Step 2
            for (int i = 0; i <= n; d[i, 0] = i++)
            {
            }

            for (int j = 0; j <= m; d[0, j] = j++)
            {
            }

            // Step 3
            for (int i = 1; i <= n; i++)
            {
                //Step 4
                for (int j = 1; j <= m; j++)
                {
                    // Step 5
                    int cost = (str[j - 1] == thisStr[i - 1]) ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }



        #endregion

        #region Split

        /// <summary>
        /// Splits the string on any symbols.
        /// </summary>
        public static string[] SplitSymbols(this string str)
        {
            return str.Split(SplitStrings, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits the string on any symbols.
        /// </summary>
        public static string[] SplitOnSpaces(this string str)
        {
            return str.Split(new string[] { " "}, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits the string on carriage returns.
        /// </summary>
        public static string[] SplitOnAllCarriageReturns(this string str)
        {
            return str.Split(new char[] { (char)9, (char)10, (char)13, }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits the string on carriage returns.
        /// </summary>
        public static string[] SplitOnCarriageReturnsOnly(this string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append((char)13);
            sb.Append((char)10);
            return str.Split(new string[] { sb.ToString() }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits the string on line feeds.
        /// </summary>
        public static string[] SplitOnLineFeeds(this string str)
        {
            return str.Split(new char[] { (char)10 }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Splits the string on horizontal tabs.
        /// </summary>
        public static string[] SplitOnTabs(this string str)
        {
            return str.Split(new char[] { (char)9 }, StringSplitOptions.RemoveEmptyEntries);
        }


        #endregion

        #region Remove / Replace

        /// <summary>
        /// Removes the value passed from the string.
        /// </summary>
        public static string Remove(this string str, string oldValue)
        {
            return str.Replace(oldValue, string.Empty);
        }

        /// <summary>
        /// Removes the values passed from the string.
        /// </summary>
        public static string Remove(this string str, string[] oldValues)
        {
            return str.Replace(oldValues, string.Empty);
        }

        /// <summary>
        /// Removes the values passed from the string.
        /// </summary>
        public static string Replace(this string str, string[] oldValues, string newValue)
        {
            if (oldValues == null) { return str; }
            foreach (string oldValue in oldValues)
            {
                if (oldValue != null)
                    str = str.Replace(oldValue, newValue);
            }
            return str;
        }

        /// <summary>
        /// Replaces all occurrences of a specified Unicode character in this instance, with
        //  another specified System.String.
        /// </summary>
        public static string Replace(this string str, char oldValue, string newValue)
        {
            return str.Replace(oldValue.ToString(), newValue);
        }

        /// <summary>
        /// Replaces all occurrences of a char that matches the predicate with the new value
        /// </summary>
        public static string Replace(this string str, Func<char, bool> predicate, string newValue)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in str)
            {
                if (predicate(ch))
                    sb.Append(newValue);
                else
                    sb.Append(ch);
            }
            return sb.ToString();
        }

        #endregion

        /// <summary>
        /// Checks if passed string can be found in a string array.
        /// </summary>
        public static bool FoundInArray(this string str, string[] array)
        {
            foreach (string part in array)
            {
                if (part == str)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if passed string can be found in a string array.
        /// </summary>
        public static string FoundBetween(this string searchString, string string1, string string2)
        {
            int position1 = searchString.IndexOf(string1);
            int position2 = searchString.IndexOf(string2);
            return Mid(searchString, position1 + string1.Length, position2 - position1 - string1.Length);
        }

        #region Contains

        /// <summary>
        /// Return whether the string contains any symbols.
        /// </summary>
        public static bool ContainsAnySymbols(this string str)
        {
            foreach (char c in str.ToCharArray())
            {
                if (c.IsAnySymbol())
                {
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Numerical

        /// <summary>
        /// Checks if a two strings are a numerical match.
        /// </summary>
        public static bool IsNumericalMatch(this string string1, string string2)
        {
            try
            {
                return (Convert.ToInt32(string1) - Convert.ToInt32(string2)) == 0;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Removes none numerical characters from the passed string, leaving a number.
        /// </summary>
        public static string TrimToNumerical(this string passedString)
        {
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (char c in passedString.ToCharArray())
                {
                    if ((int)c >= 48 && (int)c <= 57)
                    {
                        stringBuilder.Append(c.ToString());
                    }
                }
                return stringBuilder.ToString();
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Removes none numerical characters from the passed string, leaving a number.
        /// </summary>
        public static string ToIntegerString(this string passedString)
        {
            try
            {
                string str = passedString.TrimToNumerical();

                if (string.IsNullOrEmpty(str))
                    return string.Empty;

                long number = Convert.ToInt64(str);
                return number.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Checks if a string only contains numeric characters.
        /// </summary>
        public static bool IsNumerical(this string passedString)
        {
            foreach (char c in passedString)
            {
                if ((int)c < 48 || (int)c > 57)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion

        # region SubString

        /// <summary>
        /// Trims the passed string.
        /// </summary>
        public static string Trim(this string passedString)
        {
            if (passedString == null) { return string.Empty; }
            return passedString.Trim();
        }

        /// <summary>
        /// Searches for the end string, and if found trims the passed string up to this point.
        /// </summary>
        public static string Trim(this string passedString, string endString)
        {
            return Trim(passedString, endString, passedString.Length);
        }

        /// <summary>
        /// Searches for the end string, and if found trims the passed string up to this point.
        /// </summary>
        public static string Trim(this string passedString, string endString, int maximumLength)
        {
            if (string.IsNullOrEmpty(passedString)) { return string.Empty; }
            if (string.IsNullOrEmpty(endString)) { return LimitLength(passedString, maximumLength); }
            for (int i = 0; i < maximumLength; i++)
            {
                if (passedString.Length >= i + endString.Length)
                {
                    if (passedString.Substring(i, endString.Length) == endString)
                    {
                        return passedString.Substring(0, i);
                    }
                }
            }
            return LimitLength(passedString, maximumLength);
        }

        /// <summary>
        /// Trims passed number of characters off the right of the passed string.
        /// </summary>
        public static string RightTrim(this string passedString, int numberCharacters)
        {
            if (passedString == null) { return null; }

            if (passedString.Length > numberCharacters)
            {
                return passedString.Substring(
                    0,
                    passedString.Length - numberCharacters);
            }
            return string.Empty;
        }

        /// <summary>
        /// Trims passed number of characters off the left of the passed string.
        /// </summary>
        public static string LeftTrim(this string passedString, int numberCharacters)
        {
            if (passedString == null) { return null; }

            if (passedString.Length > numberCharacters)
            {
                return passedString.Substring(
                    numberCharacters,
                    passedString.Length - numberCharacters);
            }
            return string.Empty;
        }

        /// <summary>
        /// Returns a string containing a specified number of characters from
        /// the left side of a string. 
        /// </summary>
        public static string Left(this string passedString, int numberCharacters)
        {
            if (string.IsNullOrEmpty(passedString)) { return string.Empty; }
            if (passedString.Length < numberCharacters) { return passedString; }
            return passedString.Substring(0, numberCharacters);
        }

        /// <summary>
        /// Returns a string containing a specified number of characters from
        /// the left side of a string. 
        /// </summary>
        public static string Left(this string passedString, int? numberCharacters)
        {
            if (numberCharacters.HasValue)
                return passedString.Left(numberCharacters.Value);
            return passedString;
        }

        /// <summary>
        /// Overlays a string at the left side of the string
        /// </summary>
        public static string LeftOverlay(this string source, string overlayString)
        {
            if (string.IsNullOrEmpty(overlayString))
                return source;

            if (overlayString.Length > source.Length)
                return overlayString;

            return overlayString + source.Right(source.Length - overlayString.Length);
        }

        /// <summary>
        /// Returns a string containing a specified number of characters from
        /// the right side of a string. 
        /// </summary>
        public static string Right(this string passedString, int numberCharacters)
        {
            if (string.IsNullOrEmpty(passedString)) { return string.Empty; }
            if (passedString.Length < numberCharacters || numberCharacters < 1) { return passedString; }
            return passedString.Substring(passedString.Length - numberCharacters, numberCharacters);
        }

        /// <summary>
        /// Returns a string containing a specified number of characters from
        /// the right side of a string. 
        /// </summary>
        public static string Right(this string passedString, int? numberCharacters)
        {
            if (numberCharacters.HasValue)
                return passedString.Right(numberCharacters.Value);
            return passedString;
        }

        /// <summary>
        /// Overlays a string at the right side of the string
        /// </summary>
        public static string RightOverlay(this string source, string overlayString)
        {
            if (string.IsNullOrEmpty(overlayString))
                return source;

            if (overlayString.Length > source.Length)
                return overlayString;

            return source.Left(source.Length - overlayString.Length) + overlayString;
        }

        /// <summary>
        /// Returns a string containing a specified number of characters from
        /// the left side of a string. 
        /// </summary>
        public static string Mid(this string passedString, int position, int numberCharacters)
        {
            if (string.IsNullOrEmpty(passedString)) { return string.Empty; }
            string right = Right(passedString, passedString.Length - position);
            return Left(right, numberCharacters);
        }

        [Obsolete("Same as Left()")]
        /// <summary>
        /// Limits the length of string, discarding any characters over that length.
        /// </summary>
        /// <remarks>
        /// Same as left, should be removed.
        /// </remarks>
        public static string LimitLength(this string passedString, int maximumLength)
        {
            return LimitLength(passedString, maximumLength, string.Empty);
        }

        /// <summary>
        /// Limits the length of string, discarding any characters over that length.
        /// </summary>
        public static string LimitLength(this string passedString, int maximumLength, string overLimitText)
        {
            if (passedString == null) { return string.Empty; }
            if (passedString.Length > maximumLength)
                passedString = passedString.Substring(0, maximumLength) + overLimitText;
            return passedString;
        }

        /// <summary>
        /// Checks that the passed string is within the passed length parameters.
        /// </summary>
        public static bool CheckLength(this string passedString, int maxLength, int minLength)
        {
            return (maxLength == -1 ? true : passedString.Length <= maxLength)
                && (minLength == -1 ? true : passedString.Length >= minLength);
        }

        /// <summary>
        /// Checks that the passed string is within the passed length parameters.
        /// </summary>
        public static bool CheckLength(this string passedString, int? maxLength, int? minLength)
        {
            return passedString.CheckMaxLength(maxLength) && passedString.CheckMinLength(minLength);
        }

        /// <summary>
        /// Checks that the passed string is within the passed length parameters.
        /// </summary>
        public static bool CheckMaxLength(this string passedString, int? maxLength)
        {
            if (maxLength == null) { return true; }
            return (maxLength.Value <= 0 ? true : passedString.Length <= maxLength.Value);  
        }

        /// <summary>
        /// Checks that the passed string is within the passed length parameters.
        /// </summary>
        public static bool CheckMinLength(this string passedString, int? minLength)
        {
            if (minLength == null) { return true; }
            return (minLength.Value <= 0 ? true : passedString.Length >= minLength.Value);
        }

        /// <summary>
        /// Splits the string and removes the last part
        /// </summary>
        public static string RemoveLastPart(this string source, string seperator)
        {
            string[] parts = source.Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 1)
                return source;

            return parts.Take(parts.Length - 1).ToArray().ToString(seperator);
        }



        # endregion

        #region Words

        /// <summary>
        /// Returns the string as an array of words
        /// </summary>
        public static string[] ToWords(this string source)
        {
            return source.Split(new char[] { (char)9, (char)10, (char)13, (char)32 }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Returns the first word
        /// </summary>
        public static string FirstWord(this string source)
        {
            return source.ToWords().FirstOrDefault();
        }

        /// <summary>
        /// Returns the last word
        /// </summary>
        public static string LastWord(this string source)
        {
            return source.ToWords().LastOrDefault();
        }

        /// <summary>
        /// Returns the word at the passed position
        /// </summary>
        public static string WordAt(this string source, int number)
        {
            string[] words = source.ToWords();
            if (words.Length > number)
                return words[number];
            return string.Empty;
        }

        #endregion

        # region Casing

        /// <summary>
        /// Checks if the passed string is all in upper case.
        /// </summary>
        public static bool IsUpper(this string passedString)
        {
            return passedString == passedString.ToUpper();
        }

        /// <summary>
        /// Checks if the passed character is upper case.
        /// </summary>
        public static bool IsUpper(this Char character)
        {
            return IsUpper(character.ToString());
        }

        /// <summary>
        /// Checks if the passed string is all in lower case.
        /// </summary>
        public static bool IsLower(this string passedString)
        {
            return passedString == passedString.ToLower();
        }

        /// <summary>
        /// Checks if the passed character is lower case.
        /// </summary>
        public static bool IsLower(this Char character)
        {
            return IsLower(character.ToString());
        }

        /// <summary>
        /// Returns a copy of this string in title case.
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            string[] words = str.SplitOnSpaces();
            StringBuilder sb = new StringBuilder();
            foreach (string word in words)
            {
                if (word.Length >= 2)
                {
                    sb.Append(word.Left(1).ToUpper() +
                        word.Mid(1, word.Length).ToLower() + " ");
                }
                else
                {
                    sb.Append(word.ToUpper() + " ");
                }
            }
            return sb.ToString().Trim();
        }

        /// <summary>
        /// Returns a copy of this string in title case.
        /// </summary>
        public static string ToCamelCase(this string str)
        {
            StringBuilder sb = new StringBuilder();

            str = str.Replace(c => c.IsAnySymbol(), " ");

            string[] words = str.SplitOnSpaces();
            foreach (string word in words)
            {
                if (word.Left(1).IsLower())
                    sb.Append(word.ToTitleCase());
                else
                    sb.Append(word);
            }

            string s = sb.ToString();
            sb = new StringBuilder();

            bool b = false;
            foreach (char ch in s)
            {
                if (!b)
                {
                    if (ch.IsUpper())
                    {
                        sb.Append(ch.ToString().ToLower());
                    }
                    else if (ch.IsLower())
                    {
                        sb.Append(ch.ToString());
                        b = true;
                    }
                }
                else
                {
                    sb.Append(ch.ToString());
                }
            }

            return sb.ToString().Trim();
        }

        # endregion

        #region Extract

        /// <summary>
        /// Extracts any characters from the string which pass the filter.
        /// </summary>
        public static string Extract(this string str, Func<char, bool> filter)
        {
            return str
                .ToCharArray()
                .Where(filter)
                .AsString();
        }

        /// <summary>
        /// Extracts all symbols from a string.
        /// </summary>
        public static string ExtractSymbols(this string str)
        {
            return str.Extract(c => c.IsAnySymbol());
        }

        /// <summary>
        /// Extracts all letters from a string.
        /// </summary>
        public static string ExtractLetters(this string str)
        {
            return str.Extract(c => char.IsLetter(c));
        }

        /// <summary>
        /// Extracts all numbers from a string.
        /// </summary>
        public static string ExtractNumbers(this string str)
        {
            return str.Extract(c => char.IsNumber(c));
        }

        /// <summary>
        /// Extracts all lower case characters from a string.
        /// </summary>
        public static string ExtractLower(this string str)
        {
            return str.Extract(c => char.IsLower(c));
        }

        /// <summary>
        /// Extracts all upper case characters from a string.
        /// </summary>
        public static string ExtractUpper(this string str)
        {
            return str.Extract(c => char.IsUpper(c));
        }

        /// <summary>
        /// Removes all controls characters from a string.
        /// </summary>
        public static string ExtractControls(this string str)
        {
            return str.Extract(c => char.IsControl(c));
        }

        #endregion

        # region Remove

        /// <summary>
        /// Removes any characters from the string which pass the remove test.
        /// </summary>
        public static string Remove(this string str, Func<char, bool> removeTest)
        {
            if (removeTest == null)
                return str;

            return str
                .ToCharArray()
                .Where(c => !removeTest(c))
                .AsString();
        }

        /// <summary>
        /// Strips all symbols from a string.
        /// </summary>
        public static string RemoveSymbols(this string str)
        {
            return str.Remove(c => c.IsAnySymbol());
        }

        /// <summary>
        /// Strips all letters from a string.
        /// </summary>
        public static string RemoveLetters(this string str)
        {
            return str.Remove(c => char.IsLetter(c));
        }

        /// <summary>
        /// Strips all numbers from a string.
        /// </summary>
        public static string RemoveNumbers(this string str)
        {
            return str.Remove(c => char.IsNumber(c));
        }

        /// <summary>
        /// Strips all lower case characters from a string.
        /// </summary>
        public static string RemoveLower(this string str)
        {
            return str.Remove(c => char.IsLower(c));
        }

        /// <summary>
        /// Removes all upper case characters from a string.
        /// </summary>
        public static string RemoveUpper(this string str)
        {
            return str.Remove(c => char.IsUpper(c));
        }

        /// <summary>
        /// Removes all controls characters from a string.
        /// </summary>
        public static string RemoveControls(this string str)
        {
            return str.Remove(c => char.IsControl(c));
        }

        /// <summary>
        /// Strips all symbols from a string.
        /// </summary>
        public static string StripSpaces(this string passedString)
        {
            return passedString.Replace(" ", "");
        }

        # endregion

        # region Wrapping




        /// <summary>
        /// Wraps the text to fit the maximum line length passed.
        /// </summary>
        public static string[] Wrap(this string text, int maxLength)
        {
            return Wrap(text, maxLength, -1);
        }


        /// <summary>
        /// Wraps the text to fit the maximum line length passed.
        /// </summary>
        public static string[] Wrap(this string text, int maxLength, int maximumLines)
        {
            if (maxLength < 1) { return new string[] { text }; }
            text = text.Replace(" ", " ");
            string[] Words = text.Split(' ');
            List<string> tempLines = new List<string>((maxLength < 1 ? 1 : text.Length / maxLength));
            string currentLine = "";
            foreach (string currentWord in Words)
            {
                if (currentWord.Length > 0)
                {
                    if (currentLine.Length + currentWord.Length + 1 <= maxLength)
                    {
                        currentLine += " " + currentWord;
                        currentLine = currentLine.Trim();
                        //currentLineLength += (currentWord.Length + 1);
                    }
                    else
                    {
                        tempLines.Add(currentLine.Trim());
                        currentLine = currentWord;
                    }
                }
            }
            if (currentLine != "")
                tempLines.Add(currentLine.Trim());

            if (maximumLines != -1 && tempLines.Count > maximumLines)
            {
                tempLines.RemoveRange(maximumLines, tempLines.Count - maximumLines);
            }
            return tempLines.ToArray();
        }


        # endregion

        /// <summary>
        /// Compares strings as integers.
        /// </summary>
        public static int IntegerCompare(this string str1, string str)
        {
            int int1 = str1.ToInteger();
            int int2 = str.ToInteger();
            return int1.CompareTo(int2);
        }
    }
}

