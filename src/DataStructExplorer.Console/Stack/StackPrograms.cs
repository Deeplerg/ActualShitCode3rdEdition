using System.Text;
using DataStructExplorer.Console.Stack.Operations;
using DataStructExplorer.Core;
using ExpressionCalculator;
using Spectre.Console;

namespace DataStructExplorer.Console.Stack;

public class StackPrograms
{
    private readonly ExpressionCalculator.ExpressionCalculator _calculator;
    private readonly string _defaultFileName;

    public StackPrograms(ExpressionCalculator.ExpressionCalculator calculator, string defaultFileName)
    {
        _calculator = calculator;
        _defaultFileName = defaultFileName;
    }

    public void HandleStackFile()
    {
        string input = Prompter.PromptFileNameAndReadWithoutNewLine(_defaultFileName);

        var operations = ParseOperations(input);

        var stack = new LinkedStack<object>();
        foreach (var operation in operations)
        {
            string? operationResult = operation.Execute(stack);
            string operationName = operation.Name;

            if (operationResult is null)
            {
                AnsiConsole.WriteLine(operationName);
            }
            else
            {
                AnsiConsole.WriteLine($"{operationName}: {operationResult}");
            }
        }
    }
    
    public void HandleStackPostfixCalculatorFile()
    {
        string postfixInput = Prompter.PromptFileNameAndReadWithoutNewLine(_defaultFileName);

        CalculatePostfixExpressionAndPrintResult(postfixInput);
    }

    public void HandleStackPostfixCalculatorInteractive()
    {
        string postfixInput = AnsiConsole.Ask<string>("Введите постфиксную запись:");

        CalculatePostfixExpressionAndPrintResult(postfixInput);
    }

    public void HandleStackInfixToPostfixFile()
    {
        string infixInput = Prompter.PromptFileNameAndReadWithoutNewLine(_defaultFileName);

        ConvertInfixToPostfixAndPrint(infixInput);
    }

    public void HandleStackInfixToPostfixInteractive()
    {
        string infixInput = AnsiConsole.Ask<string>("Введите инфиксную запись:");

        ConvertInfixToPostfixAndPrint(infixInput);
    }

    public void HandleStackInfixCalculatorInteractive()
    {
        string infixInput = AnsiConsole.Ask<string>("Введите инфиксную запись:");

        CalculateInfixExpressionAndPrintResult(infixInput);
    }
    
    private void CalculatePostfixExpressionAndPrintResult(string input)
    {
        double result = _calculator.Calculate(input, CalculationStrategy.FromReversePolishNotation);

        AnsiConsole.WriteLine("Результат: {0}", result);
    }

    private void CalculateInfixExpressionAndPrintResult(string input)
    {
        double result = _calculator.Calculate(input);

        AnsiConsole.WriteLine("Результат: {0}", result);
    }

    private void ConvertInfixToPostfixAndPrint(string input)
    {
        var tokens = _calculator.ConvertToReversePolishNotation(input);

        var builder = new StringBuilder();
        foreach (var token in tokens)
        {
            builder.Append(token.GetStringRepresentation());
            builder.Append(' ');
        }

        string result = builder.ToString();

        AnsiConsole.WriteLine("Результат: {0}", result);
    }
    
    private IEnumerable<IStackOperation> ParseOperations(string input)
    {
        var split = input.Split(' ');

        foreach (string encodedOperation in split)
        {
            if (encodedOperation.StartsWith("1,"))
            {
                string element = encodedOperation.Substring(2);
                yield return new PushStackOperation(element);
                continue;
            }

            yield return encodedOperation switch
            {
                "2" => new PopStackOperation(),
                "3" => new TopStackOperation(),
                "4" => new IsEmptyStackOperation(),
                "5" => new PrintStackOperation(),
                _ => throw new ArgumentException($"Неизвестная операция {encodedOperation}!")
            };
        }
    }
}