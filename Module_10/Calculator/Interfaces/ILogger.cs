using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.Calculator.Interfaces
{
    interface ILogger
    {
        /// <summary>
        /// Записывает события
        /// </summary>
        /// <param name="eventMsg">Описание события</param>
        void Event(object sender, string eventMsg);

        /// <summary>
        /// Записывает ошибки
        /// </summary>
        /// <param name="errorMsg">Описание ошибки</param>
        void Error(object sender, string errorMsg);
    }
}
