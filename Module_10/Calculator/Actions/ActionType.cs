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
        /// Возвращает тип действия по символу
        /// </summary>
        /// <param name="action">Символ действия</param>
        /// <returns>Тип тействия</returns>
        /// <exception cref="NotImplementedException">Сообщение о неподдерживаемом символе</exception>
        internal static ActionType GetActionType(this char action)
        {
            foreach (KeyValuePair<ActionType, char> item in ActionTypeChar)
            {
                if (item.Value.Equals(action)) return item.Key;
            }
            throw new NotImplementedException($"Для действия \'{action}\' нет типа.");
        }

        /// <summary>
        /// Возвращает тип действия по строке
        /// </summary>
        /// <param name="action">Строка действия</param>
        /// <returns>Тип действия</returns>
        /// <exception cref="NotImplementedException">Сообщение о неподдерживаемом символе</exception>
        internal static ActionType GetActionType(this string action)
        {
            foreach (KeyValuePair<ActionType, string> item in ActionTypeRussianString)
            {
                if (item.Value.Equals(action.ToLower())) return item.Key;
            }
            throw new NotImplementedException($"Для действия \'{action}\' нет типа.");
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
            return action switch
            {
                ActionType.Add => new Add<T>(),
                ActionType.Sub => new Sub<T>(),
                ActionType.Mul => new Mul<T>(),
                ActionType.Div => new Div<T>(),
                _ => throw new NotImplementedException($"Для действия {action} нет класса.")
            };
        }
    }
}
