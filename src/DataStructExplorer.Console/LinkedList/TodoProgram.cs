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
            AnsiConsole.MarkupLine(Environment.NewLine + "[cyan]Выберите действие для списка задач:[/]");
            AnsiConsole.WriteLine("1. Добавить задачу");
            AnsiConsole.WriteLine("2. Удалить текущую задачу");
            AnsiConsole.WriteLine("3. Показать задачи");
            AnsiConsole.WriteLine("4. Перейти к следующей задаче");
            AnsiConsole.WriteLine("5. Перейти к предыдущей задаче");
            AnsiConsole.WriteLine("0. Выйти");

            int choice = AnsiConsole
                .Prompt(
                    new TextPrompt<int>("[cyan]Введите ваш выбор:[/]")
                        .Validate(c => c is >= 0 and <= 5));

            if (choice == 0) break;
            
            Console.Clear();
            ExecuteChoice(choice);
        }
    }

    private void ExecuteChoice(int choice)
    {
        switch (choice)
        {
            case 1:
                AddTask();
                break;
            case 2:
                RemoveCurrentTask();
                break;
            case 3:
                ShowTasks();
                break;
            case 4:
                MoveToNext();
                break;
            case 5:
                MoveToPrevious();
                break;
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
