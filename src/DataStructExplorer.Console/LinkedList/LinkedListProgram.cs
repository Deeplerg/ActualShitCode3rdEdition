namespace DataStructExplorer.Console.LinkedList;

using System;
using Spectre.Console;
using Core;

public class LinkedListProgram
{
    private LinkedList _list = new();

    public void Run()
    {
        while (true)
        {
            int choice = AnsiConsole.Prompt(
                new SelectionPrompt<int>()
                    .Title("[cyan]Выберите операцию для связанного списка:[/]")
                    .AddChoices(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0)
                    .UseConverter(x =>
                    {
                        return x switch
                        {
                            1 => "1. Перевернуть список",
                            2 => "2. Переместить последний элемент в начало",
                            3 => "3. Посчитать уникальные элементы",
                            4 => "4. Удалить неуникальные элементы",
                            5 => "5. Вставить список после первого X",
                            6 => "6. Вставить элемент в отсортированный список",
                            7 => "7. Удалить все элементы со значением E",
                            8 => "8. Вставить F перед первым E",
                            9 => "9. Добавить другой список",
                            10 => "10. Разделить список по первому вхождению X",
                            11 => "11. Удвоить список",
                            12 => "12. Поменять местами два элемента по значению",
                            0 => "Выход",
                            _ => x.ToString()
                        };
                    })
                    .PageSize(int.MaxValue));

            if (choice == 0) break;

            Console.Clear();
            ExecuteChoice(choice);
            AnsiConsole.WriteLine("\nТекущий список:");
            DisplayList(_list);
        }
    }

    private void ExecuteChoice(int choice)
    {
        switch (choice)
        {
            case 1: ReverseList(); break;
            case 2: MoveLastElementToStart(); break;
            case 3: CountUniqueElements(); break;
            case 4: RemoveNonUniqueElements(); break;
            case 5: InsertListAfterX(); break;
            case 6: InsertElementInSortedOrder(); break;
            case 7: RemoveAllElementsWithE(); break;
            case 8: InsertFBeforeE(); break;
            case 9: AppendAnotherList(); break;
            case 10: SplitListByX(); break;
            case 11: DoubleList(); break;
            case 12: SwapElementsByValue(); break;
        }
    }

    private void ReverseList()
    {
        LinkedListOperations.Reverse(_list);
        AnsiConsole.MarkupLine("[green]Список перевернут.[/]");
    }

    private void MoveLastElementToStart()
    {
        LinkedListOperations.MoveLastToStart(_list);
        AnsiConsole.MarkupLine("[green]Последний элемент перемещен в начало.[/]");
    }

    private void CountUniqueElements()
    {
        int count = LinkedListOperations.CountUniqueElements(_list);
        AnsiConsole.MarkupLine($"[green]Количество уникальных элементов: {count}[/]");
    }

    private void RemoveNonUniqueElements()
    {
        LinkedListOperations.RemoveNonUniqueElements(_list);
        AnsiConsole.MarkupLine("[green]Неуникальные элементы удалены.[/]");
    }

    private void InsertListAfterX()
    {
        int x = AnsiConsole.Prompt(new TextPrompt<int>("[cyan]Введите значение X, после которого нужно вставить список:[/]"));
        var newList = CreateLinkedList("элементы нового списка");
        LinkedListOperations.InsertListAfterFirstX(_list, newList, x);
        AnsiConsole.MarkupLine($"[green]Список вставлен после первого вхождения {x}.[/]");
    }

    private void InsertElementInSortedOrder()
    {
        int e = AnsiConsole.Prompt(new TextPrompt<int>("[cyan]Введите элемент для вставки в отсортированный список:[/]"));
        LinkedListOperations.InsertInSortedOrder(_list, e);
        AnsiConsole.MarkupLine("[green]Элемент вставлен в отсортированный список.[/]");
    }

    private void RemoveAllElementsWithE()
    {
        int e = AnsiConsole.Prompt(new TextPrompt<int>("[cyan]Введите значение E для удаления из списка:[/]"));
        LinkedListOperations.RemoveAllElements(_list, e);
        AnsiConsole.MarkupLine($"[green]Все вхождения {e} удалены из списка.[/]");
    }

    private void InsertFBeforeE()
    {
        int e = AnsiConsole.Prompt(new TextPrompt<int>("[cyan]Введите элемент E, перед которым нужно вставить F:[/]"));
        int f = AnsiConsole.Prompt(new TextPrompt<int>("[cyan]Введите элемент F для вставки перед E:[/]"));
        LinkedListOperations.InsertBeforeFirstE(_list, e, f);
        AnsiConsole.MarkupLine($"[green]Элемент {f} вставлен перед первым вхождением {e}.[/]");
    }

    private void AppendAnotherList()
    {
        var toAppend = CreateLinkedList("элементы списка для добавления");
        LinkedListOperations.AppendList(_list, toAppend);
        AnsiConsole.MarkupLine("[green]Список добавлен к текущему списку.[/]");
    }

    private void SplitListByX()
    {
        int x = AnsiConsole.Prompt(new TextPrompt<int>("[cyan]Введите значение X, по которому нужно разделить список по первому вхождению:[/]"));
        var (beforeX, afterX) = LinkedListOperations.SplitByFirstX(_list, x);
        AnsiConsole.MarkupLine("[green]Список разделен по первому вхождению X.[/]");
        AnsiConsole.MarkupLine("[cyan]До X:[/]");
        DisplayList(beforeX);
        AnsiConsole.MarkupLine("[cyan]После X:[/]");
        DisplayList(afterX);
    }
    
    private void DoubleList()
    {
        LinkedListOperations.DoubleList(_list);
        AnsiConsole.MarkupLine("[green]Список удвоен.[/]");
    }

    private void SwapElementsByValue()
    {
        int value1 = AnsiConsole.Prompt(new TextPrompt<int>("[cyan]Введите первое значение для обмена:[/]"));
        int value2 = AnsiConsole.Prompt(new TextPrompt<int>("[cyan]Введите второе значение для обмена с первым:[/]"));
        LinkedListOperations.SwapElements(_list, value1, value2);
        AnsiConsole.MarkupLine($"[green]Элементы {value1} и {value2} поменялись местами, если они существуют.[/]");
    }
    
    private LinkedList CreateLinkedList(string prompt)
    {
        var list = new LinkedList();
        string input = AnsiConsole.Prompt(new TextPrompt<string>($"[cyan]Введите {prompt}, разделенные пробелами:[/]"));
        foreach (var element in input.Split(' '))
        {
            if (int.TryParse(element, out int data))
                list.AddLast(data);
        }
        return list;
    }

    private void DisplayList(LinkedList list)
    {
        var current = list.Head;
        while (current is not null)
        {
            AnsiConsole.Markup($"[yellow]{current.Data}[/] -> ");
            current = current.Next;
        }
        AnsiConsole.MarkupLine("[grey]null[/]");
    }
}