using Module_10.Calculator.Actions;
using Module_10.Calculator.Helpers;
using Module_10.Calculator.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.Calculator.Services
{
    internal class CalculatorUIService<T> where T : INumber<T>
    {
        /// <summary>
        /// Обрабатываемый калькулятор
        /// </summary>
        private readonly CalculatorService<T> Calculator;

        /// <summary>
        /// Логгер
        /// </summary>
        private readonly ILogger? Logger = null;

        /// <summary>
        /// Событие ввода каких-то неизвествных значений (чтобы было :))
        /// </summary>
        public event Action<string?>? OnSecretInputCommand;

        internal CalculatorUIService(ILogger? logger, CalculatorService<T> calculator)
        {
            Logger = logger;
            Calculator = calculator;

            Logger?.Event(this, $"Создан консольный интерфейс для калькулятора: {this.Calculator.GetInfo()}");
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="calculator">Ссылка на обрабатываемый калькулятор</param>
        internal CalculatorUIService (CalculatorService<T> calculator) : this(null, calculator)
        {
        }

        /// <summary>
        /// Вызывает секретное действие
        /// </summary>
        /// <param name="command"></param>
        private void SecretInputCommand(string? command)
        {
            OnSecretInputCommand?.Invoke(command);
        }

        /// <summary>
        /// Приветсвует пользователя
        /// </summary>
        public void Greating()
        {
            Console.WriteLine($"Добро пожаловать. {Calculator.GetInfo()}");
        }

        /// <summary>
        /// Выводит меню калькулятора
        /// </summary>
        /// <returns></returns>
        public void PrintMenu()
        {
            Logger?.Event(this, $"Вывод меню всех доступных действий.");
            Console.WriteLine("Какое действе хотите выполнить?");
            var actions = Calculator.GetSupportActionTypes();
            try
            {
                foreach (var action in actions)
                {
                    Console.WriteLine($"{(int)action + 1}. {action.GetActionString().Capitalize()}\t\'{action.GetActionChar()}\'");
                }
            }
            catch (NotImplementedException ex)
            {
                Logger?.Error(this, ex.Message);
            }
        }

        /// <summary>
        /// Числовой ввод пользователя
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="preInputMessage"></param>
        /// <returns></returns>
        public Result<K> ReadUserNumberInput<K>(string? preInputMessage) where K : INumber<K>
        {
            Logger?.Event(this, "Запрос ввода пользователя.");

            Console.Write($"{preInputMessage}> ");
            var input = Console.ReadLine();
            var errMsg = $"\"{input}\" - не правильный формат входных данных";
            if ( K.TryParse(input, CultureInfo.CurrentCulture, out K? value) && (value is not null) )
            {
                return Result<K>.Success(value);
            }
            else
            {
                SecretInputCommand(input);
                Logger?.Error(this, errMsg);
                return Result<K>.Error(errMsg);
            }
        }

        /// <summary>
        /// Выводит результат вычисления
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="action"></param>
        public void PrintResult(T a, T b, ActionType action)
        {
            Logger?.Event(this, "Вывод результата.");
            var res = Calculator.Calc(a, b, action);
            var msg = res.IsSuccess ? $"{res.Value}" : res.ErrorMessage ?? "Нет сообщения об ошибке";
            Console.WriteLine($"Результат действия \"{action.GetActionString()}\": {msg}");
        }

        /// <summary>
        /// Запрос на повторение цикла
        /// </summary>
        /// <returns></returns>
        public bool AskToContinue()
        {
            Logger?.Event(this, "Вопрос о повторном выполении.");
            while (true)
            {
                Console.Write("Хотите продолжить? (да/нет)> ");
                var req = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(req))
                {
                    if (req.ToLower().Equals("да"))
                    {
                        return true;
                    }
                    if (req.ToLower().Equals("нет"))
                    {
                        return false;
                    }
                }
                SecretInputCommand(req);
                Console.WriteLine("Ваш ответ не соответствует ни одному из ожидаемый вариантов.");
                Logger?.Error(this, "Ошибка ответа на вопрос о повторном выполнении.");
            }
        }
    }
}
