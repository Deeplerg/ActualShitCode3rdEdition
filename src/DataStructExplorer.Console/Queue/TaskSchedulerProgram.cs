using Spectre.Console;

namespace DataStructExplorer.Console.Queue;

using System;

public class TaskSchedulerProgram
{
    private Queue<(int Delay, string Message)> _taskQueue = new();

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Markup(
                "[cyan]Введите задачу (формат: миллисекунды сообщение) " +
                "или 'выполнить' для запуска задач, 'выход' для завершения:[/] ");
            
            string? input = Console.ReadLine();
            
            if (string.IsNullOrEmpty(input) || input == "выход") break;
            if (input == "выполнить") ExecuteTasks();
            else ScheduleTask(input);
        }
    }

    private void ScheduleTask(string input)
    {
        var parts = input.Split(' ', 2);
        if (parts.Length < 2 || !int.TryParse(parts[0], out int delay))
        {
            AnsiConsole.MarkupLine("[red]Неверный формат ввода. Попробуйте снова.[/]");
            return;
        }

        _taskQueue.Enqueue((delay, parts[1]));
        AnsiConsole.MarkupLine("[green]Задача добавлена в очередь.[/]");
    }

    private void ExecuteTasks()
    {
        AnsiConsole.MarkupLine("[cyan]Выполнение задач...[/]");

        while (_taskQueue.Count > 0)
        {
            var (delay, message) = _taskQueue.Dequeue();
            Thread.Sleep(delay);
            AnsiConsole.MarkupLine($"[green]{message}[/]");
        }
    }
}
