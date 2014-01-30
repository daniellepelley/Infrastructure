using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pluto.Extensions
{
    public static class CharMethods
    {
        /// <summary>
        /// Returns whether the passed character isn't a letter, number or
        /// white space.
        /// </summary>
        public static bool IsAnySymbol(this char ch)
        {
            return ((!char.IsLetter(ch) &&
                !char.IsNumber(ch) &&
                !char.IsWhiteSpace(ch))
                || char.IsSymbol(ch));
        }


        /// <summary>
        /// Returns whether the passed character isn't a letter, number or
        /// white space.
        /// </summary>
        public static bool IsVowel(this char ch)
        {
            int letterNumber = ch.ToInteger();
            return letterNumber == 65 ||
                letterNumber == 69 ||
                letterNumber == 73 ||
                letterNumber == 79 ||
                letterNumber == 85 ||
                
                letterNumber == 97 ||
                letterNumber == 101 ||
                letterNumber == 105 ||
                letterNumber == 111 ||
                letterNumber == 117
                
                
                ;
        }

    }
}
