using Spectre.Console;

namespace DataStructExplorer.Console.Tree;

using DataStructExplorer.Core;
using System;

public class TreeProgram
{
    private readonly BinaryTree<char> _tree = new();
    private readonly Stack<TreeNode<char>> _navigationStack = new();
    
    private TreeNode<char>? _currentNode;

    public void Run()
    {
        while (true)
        {
            AnsiConsole.Markup("[cyan]Выберите действие для работы с деревом:[/]\n");
            AnsiConsole.WriteLine("1. Создать дерево из картинки");
            AnsiConsole.WriteLine("2. Перейти к корневому узлу");
            AnsiConsole.WriteLine("3. Создать новый корневой узел");
            AnsiConsole.WriteLine("4. Перейти к левому дочернему узлу");
            AnsiConsole.WriteLine("5. Перейти к правому дочернему узлу");
            AnsiConsole.WriteLine("6. Добавить левый дочерний узел");
            AnsiConsole.WriteLine("7. Добавить правый дочерний узел");
            AnsiConsole.WriteLine("8. Удалить текущий узел");
            AnsiConsole.WriteLine("9. Показать обход дерева в глубину");
            AnsiConsole.WriteLine("10. Показать обход дерева в ширину");
            AnsiConsole.WriteLine("0. Выйти");

            int choice = AnsiConsole
                .Prompt(
                    new TextPrompt<int>("[cyan]Введите ваш выбор:[/]")
                        .Validate(c => c is >= 0 and <= 10));

            if (choice == 0)
                break;

            Console.Clear();
            ExecuteChoice(choice);
        }
    }

    private void ExecuteChoice(int choice)
    {
        switch (choice)
        {
            case 1: CreateSampleTree(); break;
            case 2: GoToRoot(); break;
            case 3: CreateRootNode(); break;
            case 4: GoLeft(); break;
            case 5: GoRight(); break;
            case 6: AddLeftNode(); break;
            case 7: AddRightNode(); break;
            case 8: RemoveCurrentNode(); break;
            case 9: PrintDepthFirstTraversal(); break;
            case 10: PrintBreadthFirstTraversal(); break;
        }
    }

    private void CreateSampleTree()
    {
        _tree.Root = new TreeNode<char>('A');
        var root = _tree.Root;
        
        root.Left = new TreeNode<char>('B');
        root.Left.Left = new TreeNode<char>('D');
        root.Left.Left.Right = new TreeNode<char>('G');
        root.Right = new TreeNode<char>('C');
        root.Right.Left = new TreeNode<char>('E');
        root.Right.Right = new TreeNode<char>('F');
        root.Right.Right.Left = new TreeNode<char>('H');
        root.Right.Right.Right = new TreeNode<char>('J');
        
        _currentNode = _tree.Root;
        _navigationStack.Clear();
        AnsiConsole.MarkupLine("[green]Дерево из картинки создано.[/]");
    }

    private void GoToRoot()
    {
        _currentNode = _tree.Root;
        _navigationStack.Clear();
        AnsiConsole.MarkupLine("[green]Перешли к корневому узлу.[/]");
    }

    private void CreateRootNode()
    {
        char name = AnsiConsole.Prompt(new TextPrompt<char>("[cyan]Введите название нового корневого узла:[/]"));
        
        _navigationStack.Clear();
        _tree.Root = new TreeNode<char>(name);
        _currentNode = _tree.Root;
        
        AnsiConsole.MarkupLine("[green]Создан новый корневой узел.[/]");
    }

    private void GoLeft()
    {
        if (_currentNode is null || _currentNode.Left is null)
        {
            AnsiConsole.MarkupLine("[red]Левого дочернего узла не существует![/]");
            return;
        }
        _navigationStack.Push(_currentNode);
        _currentNode = _currentNode.Left;
        AnsiConsole.MarkupLine($"[green]Перешли к левому дочернему узлу: {_currentNode.Data}[/]");
    }

    private void GoRight()
    {
        if (_currentNode?.Right is null)
        {
            AnsiConsole.MarkupLine("[red]Правого дочернего узла не существует![/]");
            return;
        }
        _navigationStack.Push(_currentNode);
        _currentNode = _currentNode.Right;
        AnsiConsole.MarkupLine($"[green]Перешли к правому дочернему узлу: {_currentNode.Data}[/]");
    }

    private void AddLeftNode()
    {
        if (_currentNode is null)
        {
            AnsiConsole.MarkupLine("[red]Текущий узел не существует![/]");
            return;
        }
        char data = AnsiConsole.Prompt(new TextPrompt<char>("[cyan]Введите значение нового левого дочернего узла:[/]"));
        _currentNode.Left = new TreeNode<char>(data);
        AnsiConsole.MarkupLine($"[green]Левый дочерний узел '{data}' добавлен.[/]");
    }

    private void AddRightNode()
    {
        if (_currentNode is null)
        {
            AnsiConsole.MarkupLine("[red]Текущий узел не существует![/]");
            return;
        }
        char data = AnsiConsole.Prompt(new TextPrompt<char>("[cyan]Введите значение нового правого дочернего узла:[/]"));
        _currentNode.Right = new TreeNode<char>(data);
        AnsiConsole.MarkupLine($"[green]Правый дочерний узел '{data}' добавлен.[/]");
    }

    private void RemoveCurrentNode()
    {
        if (_currentNode is null || !_navigationStack.Any())
        {
            AnsiConsole.MarkupLine("[red]Невозможно удалить корневой узел или узел не выбран![/]");
            return;
        }

        var parent = _navigationStack.Peek();
        if (parent.Left == _currentNode)
            parent.Left = null;
        else if (parent.Right == _currentNode)
            parent.Right = null;

        _currentNode = _navigationStack.Pop();
        AnsiConsole.MarkupLine("[green]Текущий узел удален.[/]");
    }

    private void PrintDepthFirstTraversal()
    {
        string result = _tree.TraverseDepthFirst();
        AnsiConsole.MarkupLine($"[green]Обход дерева в глубину:[/] {result}");
    }

    private void PrintBreadthFirstTraversal()
    {
        string result = _tree.TraverseBreadthFirst();
        AnsiConsole.MarkupLine($"[green]Обход дерева в ширину:[/] {result}");
    }
}