
using Module_10.Calculator.Actions;
using Module_10.Calculator.Services;


var calc = new CalculatorService<int>(new LoggerService(), ActionType.Add, ActionType.Sub);

var res = calc.Calc(1, 2, ActionType.Add);

Console.WriteLine(res);
