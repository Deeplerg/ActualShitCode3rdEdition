namespace DataStructExplorer.Console.LinkedList;

using System;
using System.Collections.Generic;
using Spectre.Console;

public class TodoProgram
{
    private readonly LinkedList<TodoItem> _todoList = new();
    private LinkedListNode<TodoItem>? _currentNode;

    public void Run()
    {
        while (true)
        {
            int choice = AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("[cyan]Выберите действие для списка задач:[/]")
                    .AddChoices(1, 2, 3, 4, 5, 0)
                    .UseConverter(x =>
                    {
                        return x switch
                        {
                            1 => "1. Добавить задачу",
                            2 => "2. Удалить текущую задачу",
                            3 => "3. Показать задачи",
                            4 => "4. Перейти к следующей задаче",
                            5 => "5. Перейти к предыдущей задаче",
                            0 => "Выйти",
                            _ => x.ToString()
                        };
                    })
                    .PageSize(int.MaxValue));

            if (choice == 0) break;
            
            Console.Clear();
            ExecuteChoice(choice);
        }
    }

    private void ExecuteChoice(int choice)
    {
        switch (choice)
        {
            case 1: AddTask(); break;
            case 2: RemoveCurrentTask(); break;
            case 3: ShowTasks(); break;
            case 4: MoveToNext(); break;
            case 5: MoveToPrevious(); break;
        }
    }

    private void AddTask()
    {
        var time = AnsiConsole.Prompt(new TextPrompt<string>("[cyan]Введите время (чч:мм):[/]"));
        var description = AnsiConsole.Prompt(new TextPrompt<string>("[cyan]Введите задачу:[/]"));

        if (TimeSpan.TryParse(time, out TimeSpan taskTime))
        {
            var newTask = new TodoItem(taskTime, description);

            if (!_todoList.Any() || _todoList.First!.Value.Time > taskTime)
            {
                _todoList.AddFirst(newTask);
            }
            else
            {
                var currentNode = _todoList.First;

                while (currentNode.Next != null && currentNode.Next.Value.Time <= taskTime)
                {
                    currentNode = currentNode.Next;
                }

                _todoList.AddAfter(currentNode, newTask);
            }

            AnsiConsole.MarkupLine("[green]Задача добавлена.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Неправильный формат времени.[/]");
        }
    }


    private void RemoveCurrentTask()
    {
        if (_currentNode is null)
        {
            AnsiConsole.MarkupLine("[red]Текущая задача не выбрана.[/]");
            return;
        }

        var nextNode = _currentNode.Next ?? _currentNode.Previous;
        _todoList.Remove(_currentNode);
        _currentNode = nextNode;

        AnsiConsole.MarkupLine("[green]Задача удалена.[/]");
    }

    private void ShowTasks()
    {
        AnsiConsole.Markup("[cyan]Список задач:[/]");
        foreach (var task in _todoList)
        {
            AnsiConsole.Write(Environment.NewLine + task);
        }
    }

    private void MoveToNext()
    {
        if (_currentNode is null)
        {
            AnsiConsole.MarkupLine("[yellow]Начнем с первой задачи.[/]");
            _currentNode = _todoList.First;
        }
        else
        {
            _currentNode = _currentNode.Next ?? _todoList.First;
        }

        if (_currentNode is not null)
            AnsiConsole.WriteLine($"Текущая задача: {_currentNode.Value}");
    }

    private void MoveToPrevious()
    {
        if (_currentNode is null) _currentNode = _todoList.Last;
        else _currentNode = _currentNode.Previous ?? _todoList.Last;

        if (_currentNode is not null)
            AnsiConsole.WriteLine($"Текущая задача: {_currentNode.Value}");
    }
}
