using DataStructExplorer.Console;
using DataStructExplorer.Console.Queue;
using DataStructExplorer.Console.Stack;
using ExpressionCalculator;
using Spectre.Console;

const string defaultFileName = "input.txt";

var choice = AnsiConsole.Prompt(
    new SelectionPrompt<ProgramChoice>()
        .Title("Выберите программу:")
        .AddChoiceGroup(
            ProgramChoice.StackGroup,
            ProgramChoice.StackFile,
            ProgramChoice.StackPostfixCalculatorFile,
            ProgramChoice.StackPostfixCalculatorInteractive,
            ProgramChoice.StackInfixToPostfixFile,
            ProgramChoice.StackInfixToPostfixInteractive,
            ProgramChoice.StackInfixCalculatorInteractive)
        .AddChoiceGroup(
            ProgramChoice.QueueGroup,
            ProgramChoice.QueueFile)
        .UseConverter(program =>
            program switch
            {
                ProgramChoice.StackGroup => "Стек",
                ProgramChoice.StackFile => "Выполнить программу из файла",
                ProgramChoice.StackPostfixCalculatorFile => "Вычислить выражение в постфиксной записи (чтение из файла)",
                ProgramChoice.StackPostfixCalculatorInteractive => "Вычислить выражение в постфиксной записи (интерактивно)",
                ProgramChoice.StackInfixToPostfixFile => "Преобразовать инфиксную запись в постфиксную (чтение из файла)",
                ProgramChoice.StackInfixToPostfixInteractive => "Преобразовать инфиксную запись в постфиксную (интерактивно)",
                ProgramChoice.StackInfixCalculatorInteractive => "Вычислить выражение в инфиксной записи (интерактивно)",

                ProgramChoice.QueueGroup => "Очередь",
                ProgramChoice.QueueFile => "Выполнить программу из файла",

                _ => throw CreateUnknownProgramException(program)
            }));


var calculator = ExpressionCalculatorBuilder.Default.Build();

var stackPrograms = new StackPrograms(calculator, defaultFileName);
var queuePrograms = new QueuePrograms(defaultFileName);

var handlers = new Dictionary<ProgramChoice, Action>()
{
    { ProgramChoice.StackFile, stackPrograms.HandleStackFile },
    { ProgramChoice.StackPostfixCalculatorFile, stackPrograms.HandleStackPostfixCalculatorFile },
    { ProgramChoice.StackPostfixCalculatorInteractive, stackPrograms.HandleStackPostfixCalculatorInteractive },
    { ProgramChoice.StackInfixToPostfixFile, stackPrograms.HandleStackInfixToPostfixFile },
    { ProgramChoice.StackInfixToPostfixInteractive, stackPrograms.HandleStackInfixToPostfixInteractive },
    { ProgramChoice.StackInfixCalculatorInteractive, stackPrograms.HandleStackInfixCalculatorInteractive },
    
    { ProgramChoice.QueueFile, queuePrograms.HandleQueueFile },
}.AsReadOnly();

if (!handlers.TryGetValue(choice, out var handler))
    throw CreateUnknownProgramException(choice);

handler.Invoke();

Exception CreateUnknownProgramException(ProgramChoice program)
    => new ArgumentException($"Неизвестная программа {program}!");