
using Module_10.Calculator.Actions;
using Module_10.Calculator.Helpers;
using Module_10.Calculator.Services;


var logger = new LoggerService();
var calc = new CalculatorService<double>(logger, ActionType.Add, ActionType.Sub, ActionType.Mul, ActionType.Div);
var ui = new CalculatorUIService<double>(logger, calc);
ui.OnSecretInputCommand += ExitCommand;

while (true)
{
    ui.Greating();
    ui.PrintMenu();
    var reqNum = ui.ReadUserNumberInput<int>("Номер меню");
    if (reqNum.IsSuccess)
    {
        var selectAction = (ActionType)reqNum.Value - 1;
        var a = ui.ReadUserNumberInput<double>("Первый аргумент");
        if (a.IsSuccess)
        {
            var b = ui.ReadUserNumberInput<double>("Первый аргумент");
            if (b.IsSuccess)
            {
                ui.PrintResult(a.Value, b.Value, selectAction);
            }
            else
            {
                Console.WriteLine(b.ErrorMessage);
            }
        }
        else
        {
            Console.WriteLine(a.ErrorMessage);
        }
    }
    else
    {
        Console.WriteLine(reqNum.ErrorMessage);
    }

    if (!ui.AskToContinue())
        break;
}

static void ExitCommand(string? exitCommand)
{
    if (!String.IsNullOrWhiteSpace(exitCommand) && exitCommand.ToLower().Equals("exit"))
    {
        Environment.Exit(0);
    }
}

