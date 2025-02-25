using Module_10.Calculator.Interfaces;
using System.Numerics;
using Module_10.Calculator.Actions;

namespace Module_10.Calculator.Actions
{
    internal class Div<T> : IAction<T> where T : INumber<T>
    {

        /// <summary>
        /// Метод выполняет деление двух чисел
        /// </summary>
        /// <param name="a">Первый аргумент деления</param>
        /// <param name="b">Второй аргумент деления</param>
        /// <returns>Результат деления</returns>
        /// <exception cref="ArgumentException"></exception>
        public T Calc(T a, T b)
        {
            try
            {
                checked
                {
                    return a / b;
                }
                
            }
            catch (DivideByZeroException ex)
            {
                throw new ArgumentException("Деление на ноль не поддерживается", ex);
            }
            catch (OverflowException ex)
            {
                throw new ArgumentException($"Переполнение типа {typeof(T).Name} при выполении оперции деления", ex);
            }
        }

        /// <summary>
        /// Метод возвращает какое дествие выполняет вычисление
        /// </summary>
        /// <returns>Тип действия вычисления</returns>
        public ActionType GetActionType()
        {
            return ActionType.Div;
        }
    }
}
