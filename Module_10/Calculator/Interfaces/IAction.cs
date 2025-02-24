using Module_10.Calculator.Actions;
using System.Numerics;

namespace Module_10.Calculator.Interfaces
{
    interface IAction<T>
    {

        /// <summary>
        /// Выполняет вычисление
        /// </summary>
        /// <param name="a">Первый аргумент вычисления</param>
        /// <param name="b">Второй аргумент вычисления</param>
        /// <returns>Результат вычисления</returns>
        public T Calc(T a, T b);

        /// <summary>
        /// Возвращает действие которое выполняние вычисление
        /// </summary>
        /// <returns>Вычисляемое действие</returns>
        public ActionType GetActionType();
    }
}
