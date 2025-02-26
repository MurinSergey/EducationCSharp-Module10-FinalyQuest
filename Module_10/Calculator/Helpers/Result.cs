using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Module_10.Calculator.Helpers
{
    internal readonly struct Result<T>
    {
        /// <summary>
        /// Значение результата
        /// </summary>
        internal T? Value { get; } = default;

        /// <summary>
        /// Признак успешности результата
        /// </summary>
        internal bool IsSuccess { get; } = default;

        /// <summary>
        /// Текст ошибки результата
        /// </summary>
        internal string? ErrorMessage { get; } = default;

        internal Result (T? value, bool isSuccess, string? errorMessage = null)
        {
            Value = value;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        /// <summary>
        /// Создает результат с успешным признаком
        /// </summary>
        /// <param name="value">Значение результата</param>
        /// <returns>Результат</returns>
        internal static Result<T> Success(T value) => new(value, true);

        /// <summary>
        /// Создает результат с упровальным признаком
        /// </summary>
        /// <param name="errorMessage">Текст ошибки результата</param>
        /// <returns>Результат</returns>
        internal static Result<T> Error(string? errorMessage) => new(default, false, errorMessage);
    }
}
