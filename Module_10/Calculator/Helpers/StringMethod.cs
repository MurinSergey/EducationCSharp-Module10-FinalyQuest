using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.Calculator.Helpers
{
    public static class StringMethod
    {
        
        /// <summary>
        /// Возвращает строку с заглавной буквой
        /// </summary>
        /// <param name="text">строка</param>
        /// <returns>Строка</returns>
        public static string Capitalize(this string text)
        {
            if (String.IsNullOrWhiteSpace(text))
                return text;

            return Char.ToUpper(text[0]) + text.Substring(1).ToLower();
        }
    }
}
