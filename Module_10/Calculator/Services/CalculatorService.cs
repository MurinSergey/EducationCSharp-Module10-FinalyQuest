using Module_10.Calculator.Actions;
using Module_10.Calculator.Interfaces;
using System;
using System.Numerics;

namespace Module_10.Calculator.Services
{
    internal class CalculatorService<T> where T : INumber<T>
    {
        /// <summary>
        /// Логирование данных
        /// </summary>
        internal ILogger? Logger { private get; set; } = null;

        /// <summary>
        /// Список все действий поддерживаемый калькулятором
        /// </summary>
        private readonly List<IAction<T>> Actions = [];

        /// <summary>
        /// Список всех методов Calc поддерживаемых действий
        /// </summary>
        private readonly Dictionary<ActionType, Func<T, T, T>> ActionsCalc = [];

        /// <summary>
        /// Конструктор создающий объект со списком поддерживаемых действий и логгером
        /// </summary>
        /// <param name="logger">Объект записи действий и ошибок</param>
        /// <param name="action">Обязательное действие</param>
        /// <param name="actions">Список дополнительных действий</param>
        internal CalculatorService(ILogger? logger, IAction<T> action, params IAction<T>[] actions)
        {
            Logger = logger;

            AddAction(action);

            foreach (IAction<T> _action in actions)
            {
                AddAction(_action);
            }

            Logger?.Event(this.GetInfo());
        }

        /// <summary>
        /// Конструктор создающий объект со списком поддерживаемых действий и логгером
        /// </summary>
        /// <param name="logger">Объект записи действий и ошибок</param>
        /// <param name="action">Тип обязательного действия</param>
        /// <param name="actions">Список типов дополнительных действий</param>
        internal CalculatorService(ILogger? logger, ActionType action, params ActionType[] actions) : this(logger, action.GetAction<T>(), [.. actions.Select(p => p.GetAction<T>())])
        {
        }

        /// <summary>
        /// Конструктор создающий объект со списком поддерживаемых действий
        /// </summary>
        /// <param name="action">Обязательное действие</param>
        /// <param name="actions">Список дополнительных действий</param>
        internal CalculatorService (IAction<T> action, params IAction<T>[] actions) : this (null, action, actions)
        {
        }

        /// <summary>
        /// Конструктор создающий объект со списком поддерживаемых действий
        /// </summary>
        /// <param name="action">Тип обязательного действия</param>
        /// <param name="actions">Список типов дополнительных действий</param>
        internal CalculatorService (ActionType action, params ActionType[] actions) : this (null, action, actions)
        {
        }

        /// <summary>
        /// Метод добавляет новое действие
        /// </summary>
        /// <param name="action">Новое действие</param>
        internal void AddAction(IAction<T> action)
        {
            Actions.Add(action);
            ActionsCalc.Add(action.GetActionType(), action.Calc);

            Logger?.Event($"Добавлено новое действие: {action.GetActionType().GetActionString()}");
        }

        /// <summary>
        /// Метод доавляет новое действие
        /// </summary>
        /// <param name="action">Тип нового действия</param>
        internal void AddAction(ActionType action)
        {
            AddAction(action.GetAction<T>());
        }

        /// <summary>
        /// Доступные действия
        /// </summary>
        /// <returns>Массив доступных действий</returns>
        internal char[] GetSupportActionChars()
        {
            char[] actionsChar = new char[Actions.Count];

            for (int i = 0; i < actionsChar.Length; i++)
            {
                try
                {
                    actionsChar[i] = Actions[i].GetActionType().GetActionChar();
                }
                catch (NotImplementedException ex)
                {
                    if (Logger is not null)
                    {
                        Logger.Error(ex.Message);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return actionsChar;
        }

        /// <summary>
        /// Доступные типы действий
        /// </summary>
        /// <returns>Массив доступных действий</returns>
        internal ActionType[] GetSupportActionTypes()
        {
            return [.. ActionsCalc.Keys];
        }

        /// <summary>
        /// Выполняется выбранное действие
        /// </summary>
        /// <param name="a">Первый аргумент действия</param>
        /// <param name="b">Второй аргумент действия</param>
        /// <param name="action">Тип тействия</param>
        /// <returns>Результат действия</returns>
        internal T? Calc(T a, T b, ActionType action)
        {
            try
            {
                if (!ActionsCalc.TryGetValue(action, out Func<T, T, T>? value)) throw new ArgumentException($"Калькулятор не поддерживает операцию \"{action.GetActionChar()}\"");
                Logger?.Event($"Выполняем вычисление: {action.GetActionString()} с аргументами типа {this.GetArgumentType().Name} a={a} и b={b}");
                return value.Invoke(a, b);
            }
            catch (ArgumentException ex)
            {
                if (Logger is not null)
                {
                    Logger.Error(ex.Message);
                    return default;
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Метод возвращает тип аргументов
        /// </summary>
        /// <returns>Тип аргументов</returns>
        internal Type GetArgumentType()
        {
            return typeof(T);
        }

        /// <summary>
        /// Возвращает описание калькулятора
        /// </summary>
        /// <returns>Строка описания</returns>
        internal string GetInfo()
        {
            return $"Калькулятор выполняет действия \"{String.Join(" ", this.GetSupportActionChars())}\" с аргументами типа {this.GetArgumentType().Name}";
        }
    }
}
