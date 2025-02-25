
using Module_10.Calculator.Actions;
using Module_10.Calculator.Services;


var calc = new CalculatorService<int>(new LoggerService(), ActionType.Add, ActionType.Sub, ActionType.Mul, ActionType.Div);

var res = calc.Calc(int.MaxValue, 1, ActionType.Div);

Console.WriteLine(res);
