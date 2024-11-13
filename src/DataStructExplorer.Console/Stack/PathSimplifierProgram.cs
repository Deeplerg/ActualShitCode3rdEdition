using Spectre.Console;

namespace DataStructExplorer.Console.Stack;

using System;

public class PathSimplifierProgram
{
    public void Run()
    {
        while (true)
        {
            AnsiConsole.Markup("[cyan]Введите путь для упрощения (или \"выход\" для завершения):[/] ");
            string? input = Console.ReadLine();
            if (string.IsNullOrEmpty(input) || input == "выход") break;

            string simplifiedPath = SimplifyPath(input);
            AnsiConsole.MarkupLine($"[green]Упрощенный путь:[/] {simplifiedPath}\n");
        }
    }

    private string SimplifyPath(string path)
    {
        Stack<string> stack = new Stack<string>();
        string[] components = path.Split('/', StringSplitOptions.RemoveEmptyEntries);

        foreach (string component in components)
        {
            if (component == ".") continue;
            if (component == "..")
            {
                if (stack.Count > 0) stack.Pop();
            }
            else
            {
                stack.Push(component);
            }
        }

        var result = new List<string>(stack);
        result.Reverse();
        return "/" + string.Join("/", result);
    }
}