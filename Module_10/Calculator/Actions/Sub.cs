using Module_10.Calculator.Interfaces;
using System.Numerics;
using Module_10.Calculator.Actions;

namespace Module_10.Calculator.Actions
{
    internal class Sub<T> : IAction<T> where T : INumber<T>
    {

        /// <summary>
        /// Метод выполняет вычитание двух чисел
        /// </summary>
        /// <param name="a">Первый аргумент вычитания</param>
        /// <param name="b">Второй аргумент вычитания</param>
        /// <returns>Результат вычитания</returns>
        /// <exception cref="ArgumentException"></exception>
        public T Calc(T a, T b)
        {
            try
            {
                checked
                {
                    return a - b;
                }
            }
            catch (OverflowException ex)
            {
                throw new ArgumentException($"Переполнение типа {typeof(T).Name} при выполении оперции умножения", ex);
            }
            
        }

        /// <summary>
        /// Метод возвращает какое дествие выполняет вычисление
        /// </summary>
        /// <returns>Тип действия вычисления</returns>
        public ActionType GetActionType()
        {
            return ActionType.Sub;
        }
    }
}
