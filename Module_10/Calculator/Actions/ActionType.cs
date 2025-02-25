using Module_10.Calculator.Interfaces;
using System.Collections;
using System.Numerics;

namespace Module_10.Calculator.Actions
{

    /// <summary>
    /// Перечисление поддерживаемых операций
    /// </summary>
    internal enum ActionType
    {
        Add,
        Sub,
        Mul,
        Div
    }

    internal static class ActionTypeMethods
    {

        private static readonly Dictionary<ActionType, char> ActionTypeChar = new()
        {
            { ActionType.Add, '+' },
            { ActionType.Sub, '-' },
            { ActionType.Mul, '*' },
            { ActionType.Div, '/' }
        };

        private static readonly Dictionary<ActionType, string> ActionTypeRussianString = new()
        {
            { ActionType.Add, "cложение" },
            { ActionType.Sub, "вычитание" },
            { ActionType.Mul, "умножение"},
            { ActionType.Div, "деление" }
        };

        private static readonly Dictionary<ActionType, Type> ActionsClasses = new()
        {
            { ActionType.Add, typeof(Add<>) },
            { ActionType.Sub, typeof(Sub<>) },
            { ActionType.Mul, typeof(Mul<>) },
            { ActionType.Div, typeof(Div<>) }
        };

        /// <summary>
        /// Возвращает символ действия
        /// </summary>
        /// <param name="action">Выбранное действие</param>
        /// <returns>Символ действия</returns>
        /// <exception cref="NotImplementedException">Сообщение о неподдерживаемом действии</exception>
        internal static char GetActionChar(this ActionType action)
        { 
            if (!ActionTypeChar.TryGetValue(action, out char value)) throw new NotImplementedException($"Для действия {action} нет символа");
            return value;
        }

        /// <summary>
        /// Описание действия
        /// </summary>
        /// <param name="action">Тип действия</param>
        /// <returns>Строка описания действия</returns>
        internal static string GetActionString(this ActionType action)
        {
            if (!ActionTypeRussianString.TryGetValue(action, out string? value)) throw new NotImplementedException($"Для действия {action} нет описания");
            return value;
        }

        /// <summary>
        /// Создает новый экземпляр указанного действия
        /// </summary>
        /// <typeparam name="T">Тип над которым выполняется действие</typeparam>
        /// <param name="action">Тип действия</param>
        /// <returns>Новое действие</returns>
        /// <exception cref="NotImplementedException">Сообщение о неподдерживаемом действии</exception>
        internal static IAction<T> GetAction<T>(this ActionType action) where T : INumber<T>
        {
            // Создаем экземпляр класса через рефлексию
            if (ActionsClasses.TryGetValue(action, out var actionClass))
            {
                // Создаем обобщенный тип, например, Add<T>, Sub<T>, и т.д.
                var genericType = actionClass.MakeGenericType(typeof(T));
                // Создаем экземпляр с помощью рефлексии
                return (IAction<T>)Activator.CreateInstance(genericType)!;
            }
            throw new NotImplementedException($"Для действия {action} нет класса.");
        }
    }
}
