using AddressBook.CommonLibrary;
using System.Text;

namespace AdressBook.ViewerConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "ADDRESSBOOK";
            EmployeeList? zamestnanci;

            string?[] commandLineArgs = args;

            for (int i = 0; i < commandLineArgs.Length; i++)
            {
                string? argument = commandLineArgs[i];

                if (argument != null && argument.StartsWith("--input"))
                {
                    string? inputFilePath = GetValueFromArgumentIndex(commandLineArgs, "--input", i);
                    if (File.Exists(inputFilePath))
                    {
                        zamestnanci = EmployeeList.LoadFromJson(new FileInfo(inputFilePath));

                        var name = GetValueFromArgumentIndex(commandLineArgs, "--name", i);
                        var position = GetValueFromArgumentIndex(commandLineArgs, "--position", i);
                        var mainWorkPlace = GetValueFromArgumentIndex(commandLineArgs, "--main-workplace", i);
                        var outputFilePath = GetValueFromArgumentIndex(commandLineArgs, "--output", i);

                        if (outputFilePath != null)
                        {
                            zamestnanci?.Search(mainWorkPlace, position, name)
                                .SaveToCsv(new FileInfo(outputFilePath));
                        }

                        zamestnanci?.Search(mainWorkPlace, position, name).WriteToConsole();
                    }
                    else
                    {
                        Console.Error.WriteLine(
                            $"ZADANÝ VSTUPNÝ SÚBOR {inputFilePath} NEEXISTUJE. \nVYPÍNAM KONZOLU.");
                        Environment.Exit(0);
                    }
                }
            }

            Console.WriteLine("\nSTLAČTE ĽUBOVOĽNÚ KLÁVESNICU PRE VYPNUTIE...");
            Console.ReadKey();
        }
        
        static string? GetValueFromArgumentIndex(string?[]? commandLineArgs, string argument, int currentIndex)
        {
            if (commandLineArgs == null)
                return null;

            int argumentIndex = Array.IndexOf(commandLineArgs, argument, currentIndex);
            if (argumentIndex == -1 || argumentIndex + 1 >= commandLineArgs.Length)
                return null;

            StringBuilder valueBuilder = new();
            int endIndex = argumentIndex + 1;

            while (endIndex < commandLineArgs.Length && !commandLineArgs[endIndex]!.StartsWith("--"))
            {
                valueBuilder.Append(commandLineArgs[endIndex] + " ");
                endIndex++;
            }

            string value = valueBuilder.ToString().Trim();
            return string.IsNullOrEmpty(value) ? null : value;
        }

    }
}
