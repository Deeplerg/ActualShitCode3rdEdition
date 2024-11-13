using DataStructExplorer.Core;
using Spectre.Console;

namespace DataStructExplorer.Console.Tree;

using System;

public class AccessControlProgram
{
    private readonly AccessControlTree _aclTree = new();
    private AccessLevelNode? _currentNode;

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Markup("[bold green]Программа Access Control List (ACL)[/]\n");

            if (_currentNode is null)
                AnsiConsole.Markup("[bold red]Роль не выбрана.[/]\n");
            else
                AnsiConsole.Markup($"[bold yellow]Текущая роль:[/] {_currentNode.Role}\n");
            
            AnsiConsole.MarkupLine("\n[cyan]Выберите действие:[/]");
            AnsiConsole.WriteLine("1. Создать корневую роль");
            AnsiConsole.WriteLine("2. Добавить подчиненную роль");
            AnsiConsole.WriteLine("3. Показать разрешения роли");
            AnsiConsole.WriteLine("4. Назначить разрешение");
            AnsiConsole.WriteLine("5. Отозвать разрешение");
            AnsiConsole.WriteLine("6. Переместиться в подчиненную роль");
            AnsiConsole.WriteLine("7. Переместиться в корневую роль");
            AnsiConsole.WriteLine("0. Выход");

            int choice = AnsiConsole
                .Prompt(
                    new TextPrompt<int>("[cyan]Введите ваш выбор:[/]")
                        .Validate(c => c is >= 0 and <= 7));

            if (choice == 0) break;

            Console.Clear();
            ExecuteChoice(choice);
        }
    }

    private void ExecuteChoice(int choice)
    {
        switch (choice)
        {
            case 1: CreateRootRole(); break;
            case 2: AddSubordinateRole(); break;
            case 3: ShowEffectivePermissions(); break;
            case 4: GrantPermission(); break;
            case 5: RevokePermission(); break;
            case 6: MoveToSubordinateRole(); break;
            case 7: MoveToRootRole(); break;
        }
    }

    private void CreateRootRole()
    {
        string role = AnsiConsole.Ask<string>("[cyan]Введите название корневой роли:[/]");
        _aclTree.Root = new AccessLevelNode(role);
        _currentNode = _aclTree.Root;
        AnsiConsole.MarkupLine("[green]Корневая роль успешно создана![/]");
    }

    private void AddSubordinateRole()
    {
        if (_currentNode is null)
        {
            AnsiConsole.MarkupLine("[red]Корневая роль не существует![/]");
            return;
        }

        string role = AnsiConsole.Ask<string>("[cyan]Введите название подчиненной роли:[/]");
        var node = new AccessLevelNode(role);
        
        if (_currentNode.Left is null)
        {
            _currentNode.Left = node;
        }
        else if (_currentNode.Right is null)
        {
            _currentNode.Right = node;
        }
        else
        {
            AnsiConsole.MarkupLine("[red]Эта роль уже имеет две подчиненных![/]");
            return;
        }

        AnsiConsole.MarkupLine("[green]Подчиненная роль добавлена успешно![/]");
    }

    private void ShowEffectivePermissions()
    {
        if (_currentNode is null)
        {
            AnsiConsole.MarkupLine("[red]Роль не выбрана![/]");
            return;
        }

        HashSet<string> permissions = _aclTree.GetEffectivePermissions(_currentNode);
        AnsiConsole.MarkupLine($"[yellow]Эффективные разрешения для роли {_currentNode.Role}:[/]");
        
        foreach (var perm in permissions)
        {
            AnsiConsole.MarkupLine($"- {perm}");
        }
    }

    private void GrantPermission()
    {
        if (_currentNode is null)
        {
            AnsiConsole.MarkupLine("[red]Роль не выбрана![/]");
            return;
        }

        string permission = AnsiConsole.Ask<string>("[cyan]Введите разрешение для добавления:[/]");
        _aclTree.GrantPermission(_currentNode, permission);
        AnsiConsole.MarkupLine("[green]Разрешение успешно добавлено![/]");
    }

    private void RevokePermission()
    {
        if (_currentNode is null)
        {
            AnsiConsole.MarkupLine("[red]Роль не выбрана![/]");
            return;
        }

        string permission = AnsiConsole.Ask<string>("[cyan]Введите разрешение для отзыва:[/]");
        _aclTree.RevokePermission(_currentNode, permission);
        AnsiConsole.MarkupLine("[green]Разрешение успешно отозвано![/]");
    }

    private void MoveToSubordinateRole()
    {
        if (_currentNode is null)
        {
            AnsiConsole.MarkupLine("[red]Роль не выбрана![/]");
            return;
        }

        int choice = AnsiConsole.Prompt(
            new SelectionPrompt<int>()
                .Title("[cyan]Выберите подчиненную роль:[/]")
                .AddChoices( 1, 2 )
                .UseConverter(x =>
                {
                    return x switch
                    {
                        1 => "Левая подчиненная роль",
                        2 => "Правая подчиненная роль",
                        _ => x.ToString()
                    };
                }));

        _currentNode = choice switch
        {
            1 => _currentNode.Left,
            2 => _currentNode.Right,
            _ => _currentNode
        };

        AnsiConsole.MarkupLine(_currentNode == null
            ? "[red]Подчиненная роль не найдена![/]"
            : $"[green]Перемещено в роль: {_currentNode.Role}[/]");
    }

    private void MoveToRootRole()
    {
        if (_aclTree.Root is null)
        {
            AnsiConsole.MarkupLine("[red]Корневой роли не существует![/]");
            return;
        }
        
        _currentNode = _aclTree.Root;
        AnsiConsole.MarkupLine($"[green]Перемещено в роль: {_currentNode.Role}[/]");
    }
}