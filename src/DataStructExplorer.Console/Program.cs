using System.Collections.ObjectModel;
using DataStructExplorer.Console;
using DataStructExplorer.Console.LinkedList;
using DataStructExplorer.Console.Queue;
using DataStructExplorer.Console.Stack;
using DataStructExplorer.Console.Tree;
using ExpressionCalculator;
using Spectre.Console;

const string defaultFileName = "input.txt";

while (true)
{
    var choice = PromptMainMenu();
    
    var handlers = CreateNewHandlers();
    
    if (!handlers.TryGetValue(choice, out var handler))
        throw CreateUnknownProgramException(choice);

    handler.Invoke();
}


ProgramChoice PromptMainMenu()
{
    return AnsiConsole.Prompt(
        new SelectionPrompt<ProgramChoice>()
            .Title("Выберите программу:")
            .AddChoiceGroup(
                ProgramChoice.StackGroup,
                ProgramChoice.StackFile,
                ProgramChoice.StackPostfixCalculatorFile,
                ProgramChoice.StackPostfixCalculatorInteractive,
                ProgramChoice.StackInfixToPostfixFile,
                ProgramChoice.StackInfixToPostfixInteractive,
                ProgramChoice.StackInfixCalculatorInteractive,
                ProgramChoice.StackPathSimplifierProgram)
            .AddChoiceGroup(
                ProgramChoice.QueueGroup,
                ProgramChoice.QueueFile,
                ProgramChoice.QueueTaskSchedulerProgram)
            .AddChoiceGroup(
                ProgramChoice.LinkedListGroup,
                ProgramChoice.LinkedListPrograms,
                ProgramChoice.LinkedListTodoProgram)
            .AddChoiceGroup(
                ProgramChoice.TreeGroup,
                ProgramChoice.TreeProgram,
                ProgramChoice.TreeAccessControlProgram)
            .AddChoices(ProgramChoice.Quit)
            .UseConverter(program =>
                program switch
                {
                    ProgramChoice.StackGroup => "Стек",
                    ProgramChoice.StackFile => "Выполнить программу из файла",
                    ProgramChoice.StackPostfixCalculatorFile =>
                        "Вычислить выражение в постфиксной записи (чтение из файла)",
                    ProgramChoice.StackPostfixCalculatorInteractive =>
                        "Вычислить выражение в постфиксной записи (интерактивно)",
                    ProgramChoice.StackInfixToPostfixFile =>
                        "Преобразовать инфиксную запись в постфиксную (чтение из файла)",
                    ProgramChoice.StackInfixToPostfixInteractive =>
                        "Преобразовать инфиксную запись в постфиксную (интерактивно)",
                    ProgramChoice.StackInfixCalculatorInteractive =>
                        "Вычислить выражение в инфиксной записи (интерактивно)",
                    ProgramChoice.StackPathSimplifierProgram =>
                        "Программа упрощения пути",

                    ProgramChoice.QueueGroup => "Очередь",
                    ProgramChoice.QueueFile => "Выполнить программу из файла",
                    ProgramChoice.QueueTaskSchedulerProgram => "Программа - планировщик задач (task scheduler)",

                    ProgramChoice.LinkedListGroup => "Связный список",
                    ProgramChoice.LinkedListPrograms => "Программы связного списка",
                    ProgramChoice.LinkedListTodoProgram => "Программа TODO",

                    ProgramChoice.TreeGroup => "Дерево",
                    ProgramChoice.TreeProgram => "Программа дерева",
                    ProgramChoice.TreeAccessControlProgram => "Программа Access Control List",

                    ProgramChoice.Quit => "Выйти из программы",
                    _ => throw CreateUnknownProgramException(program)
                })
            .MoreChoicesText("(Прокрутите вверх и вниз, чтобы увидеть больше вариантов)"));
}

Exception CreateUnknownProgramException(ProgramChoice program)
    => new ArgumentException($"Неизвестная программа {program}!");

ReadOnlyDictionary<ProgramChoice, Action> CreateNewHandlers()
{
    string defaultInputFileName = defaultFileName;
    
    var calculator = ExpressionCalculatorBuilder.Default.Build();

    var stackPrograms = new StackPrograms(calculator, defaultInputFileName);
    var queuePrograms = new QueuePrograms(defaultInputFileName);
    var linkedListPrograms = new LinkedListProgram();
    var treeProgram = new TreeProgram();
    var pathProgram = new PathSimplifierProgram();
    var taskSchedulerProgram = new TaskSchedulerProgram();
    var todoProgram = new TodoProgram();
    var accessControlProgram = new AccessControlProgram();

    return new Dictionary<ProgramChoice, Action>()
    {
        { ProgramChoice.StackFile, stackPrograms.HandleStackFile },
        { ProgramChoice.StackPostfixCalculatorFile, stackPrograms.HandleStackPostfixCalculatorFile },
        { ProgramChoice.StackPostfixCalculatorInteractive, stackPrograms.HandleStackPostfixCalculatorInteractive },
        { ProgramChoice.StackInfixToPostfixFile, stackPrograms.HandleStackInfixToPostfixFile },
        { ProgramChoice.StackInfixToPostfixInteractive, stackPrograms.HandleStackInfixToPostfixInteractive },
        { ProgramChoice.StackInfixCalculatorInteractive, stackPrograms.HandleStackInfixCalculatorInteractive },
        { ProgramChoice.StackPathSimplifierProgram, pathProgram.Run },
    
        { ProgramChoice.QueueFile, queuePrograms.HandleQueueFile },
        { ProgramChoice.QueueTaskSchedulerProgram, taskSchedulerProgram.Run },

        { ProgramChoice.LinkedListPrograms, linkedListPrograms.Run },
        { ProgramChoice.LinkedListTodoProgram, todoProgram.Run },

        { ProgramChoice.TreeProgram, treeProgram.Run },
        { ProgramChoice.TreeAccessControlProgram, accessControlProgram.Run },
    
        { ProgramChoice.Quit, () => Environment.Exit(0) }
    }.AsReadOnly();
}