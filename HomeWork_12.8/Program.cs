using static System.Console;
using static System.Int32;

namespace HomeWork_12._8;

internal class Program
{
    private static void Main()
    {
        Repository repository = new("database1.txt");
        CreateMenu();
        ChooseOptionInMenu(repository);
    }

    private static void CreateMenu()
    {
        WriteLine("Choose option:\n\n" +
                  "(1) Show all existing entries\n" +
                  "(2) Add new entry\n" +
                  "(3) Delete existng entry\n" +
                  "(4) Sort entries by parameter\n" +
                  "(5) Filter entries by date range\n" +
                  "\nEnter (1), (2), (3), (4), (5)");
    }

    private static void ReturnToMenu(Repository repository)
    {
        ReadKey();
        Clear();
        CreateMenu();
        ChooseOptionInMenu(repository);
    }

    private static void ChooseOptionInMenu(Repository repository)
    {
        TryParse(ReadLine(), out int choice);

        switch (choice)
        {
            case 1:
                Clear();
                repository.PrintAllEntries();
                ReturnToMenu(repository);
                break;

            case 2:
                Clear();
                repository.CreateNewEntryFromUserInput();
                repository.UpdateDatabase();
                WriteLine("Entry added!");
                ReturnToMenu(repository);
                break;

            case 3:
                Clear();
                repository.DeleteEntryByUserInput();
                repository.UpdateDatabase();
                WriteLine("Entry deleted!");
                ReturnToMenu(repository);
                break;

            case 4:
                Clear();
                repository.ChooseSortingOfEntriesByParameter();
                ReturnToMenu(repository);
                break;

            case 5:
                Clear();
                repository.PrintEntriesFilteredByUserInsertedDateRange();
                ReturnToMenu(repository);
                break;

            default:
                WriteLine("\nUncknown command");
                ReturnToMenu(repository);
                break;
        }
    }
}