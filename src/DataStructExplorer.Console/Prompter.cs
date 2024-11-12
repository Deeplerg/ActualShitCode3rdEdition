using Spectre.Console;

namespace DataStructExplorer.Console;

public static class Prompter
{
    public static string PromptFileNameAndReadWithoutNewLine(string? defaultFileName = null)
    {
        string filename = PromptFileName(defaultFileName);
        return ReadFileContentsWithoutNewLine(filename);
    }

    public static string PromptFileName(string? defaultFileName = null)
    {
        var prompt = new TextPrompt<string>("Введите название файла");
    
        if (!string.IsNullOrEmpty(defaultFileName))
        {
            prompt
                .AllowEmpty()
                .DefaultValue(defaultFileName)
                .ShowDefaultValue();
        }

        prompt
            .Validate(File.Exists)
            .ValidationErrorMessage("[red]Такого файла не существует![/]");

        return AnsiConsole.Prompt(prompt);
    }

    public static string ReadFileContentsWithoutNewLine(string fileName)
    {
        var contents = File.ReadAllText(fileName);
    
        return contents
            .Replace("\n", string.Empty)
            .Replace("\r", string.Empty);
    }
}