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
        /// Список всех методов Calc поддерживаемых действий
        /// </summary>
        private readonly Dictionary<ActionType, Func<T, T, T>> Actions = [];

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
            try
            {
                Logger?.Event($"Добавление действия: {action.GetActionType().GetActionString()}");
                Actions.Add(action.GetActionType(), action.Calc);
            }
            catch(ArgumentException ex)
            {
                Logger?.Error(ex.Message);
            }
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
        internal char[]? GetSupportActionChars()
        {
            try
            {
                return [.. Actions.Keys.ToArray().Select(p => p.GetActionChar())];
            }
            catch (NotImplementedException ex)
            {
                Logger?.Error(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Доступные типы действий
        /// </summary>
        /// <returns>Массив доступных действий</returns>
        internal ActionType[] GetSupportActionTypes()
        {
            return [.. Actions.Keys];
        }

        /// <summary>
        /// Выполняется выбранное действие
        /// </summary>
        /// <param name="a">Первый аргумент действия</param>
        /// <param name="b">Второй аргумент действия</param>
        /// <param name="action">Тип тействия</param>
        /// <returns>Результат действия</returns>
        /// <exception cref="ArgumentException"></exception>
        internal T? Calc(T a, T b, ActionType action)
        {
            try
            {
                
                if (!Actions.TryGetValue(action, out Func<T, T, T>? value)) throw new ArgumentException($"Калькулятор не поддерживает операцию \"{action.GetActionString()}\"");
                var res = value.Invoke(a, b);

                Logger?.Event($"Выполняем вычисление: {action.GetActionString()} с аргументами типа {this.GetArgumentType().Name} a={a} и b={b}, где результат={res}");
                return res;
            }
            catch (ArgumentException ex)
            {
                Logger?.Error(ex.Message);
                throw;
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
        /// <exception cref="NotImplementedException"></exception>
        internal string GetInfo()
        {
            return $"Калькулятор выполняет действия \"{String.Join(" ", this.GetSupportActionChars() ?? [])}\" с аргументами типа {this.GetArgumentType().Name}";
        }
    }
}
