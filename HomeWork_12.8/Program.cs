﻿using static System.Console;
using static System.DateTime;
using static System.Int32;

namespace HomeWork_12._8;

internal class Program
{
    private static void Main()
    {
        Repository repository = new();
        repository.CheckIfFileExistAndCreate();
        List<Worker> existingWorkers = repository.GetAllWorkers();

        CreateMenu();
        ChooseOptionInMenu(existingWorkers, repository);
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

    private static void ReturnToMenu(List<Worker> existingWorkers, Repository repository)
    {
        ReadKey();
        Clear();
        CreateMenu();
        ChooseOptionInMenu(existingWorkers, repository);
    }

    private static void ChooseOptionInMenu(List<Worker> existingWorkers, Repository repository)
    {
        TryParse(ReadLine(), out int choise);

        switch (choise)
        {
            case 1:
                Clear();
                PrintAllEntries(existingWorkers);
                ReturnToMenu(existingWorkers, repository);
                break;

            case 2:
                Clear();
                CreateNewEntry(existingWorkers, repository);
                existingWorkers = repository.GetAllWorkers();
                WriteLine("Entry added!");
                ReturnToMenu(existingWorkers, repository);
                break;

            case 3:
                Clear();
                DeleteEntry(existingWorkers, repository);
                existingWorkers = repository.GetAllWorkers();
                WriteLine("Entry deleted!");
                ReturnToMenu(existingWorkers, repository);
                break;

            case 4:
                Clear();
                ChooseSortingOfEntriesByParameter(existingWorkers, repository);
                ReturnToMenu(existingWorkers, repository);
                break;

            case 5:
                Clear();
                PrintFilteredEntriesByDateRange(existingWorkers, repository);
                ReturnToMenu(existingWorkers, repository);
                break;

            default:
                WriteLine("Uncknown command");
                ReturnToMenu(existingWorkers, repository);
                break;
        }
    }

    private static void PrintAllEntries(List<Worker> existingWorkers)
    {
        foreach (Worker worker in existingWorkers)
            WriteLine(worker.Print());
    }

    private static void CreateNewEntry(List<Worker> existingWorkers, Repository repository)
    {
        int id = existingWorkers.Max(worker => worker.Id) + 1;

        WriteLine("\nPlease, enter full name: ");
        string fio = ReadLine() ?? string.Empty;

        WriteLine("\nPlease, enter age: ");
        TryParse(ReadLine(), out int age);

        WriteLine("\nPlease, enter height: ");
        TryParse(ReadLine(), out int height);

        WriteLine("\nPlease, enter date of birth (YYYY.MM.DD): ");
        TryParse(ReadLine(), out DateTime dateOfBirth);

        WriteLine("\nPlease, enter place of birth: ");
        string placeOfBirth = ReadLine() ?? string.Empty;

        WriteLine("\nPlease, enter date of entry creation (YYYY.MM.DD): ");
        TryParse(ReadLine(), out DateTime dateOfEntryCreation);

        Clear();

        Worker newWorker = new(id, fio, age, height, dateOfBirth, placeOfBirth, dateOfEntryCreation);
        repository.AddWorker(newWorker);
    }

    private static void DeleteEntry(List<Worker> existingWorkers, Repository repository)
    {
        WriteLine("Please, enter ID of entry you wish to delete: ");
        TryParse(ReadLine(), out int idToDelete);
        Clear();

        repository.DeleteWorker(idToDelete, existingWorkers);
    }

    private static void ChooseSortingOfEntriesByParameter(List<Worker> existingWorkers, Repository repository)
    {
        WriteLine("Choose sorting parameter:\n\n" +
                  "(1) ID\n" +
                  "(2) FullName\n" +
                  "(3) Age\n" +
                  "(4) Height\n" +
                  "(5) Date of birth\n" +
                  "(6) Place of birth\n" +
                  "(7) Date of entry creation\n" +
                  "\nEnter (1), (2), (3), (4), (5), (6), (7)");

        TryParse(ReadLine(), out int choise);
        IEnumerable<Worker> orderedEnumerable;

        switch (choise)
        {
            case 1:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.Id);
                PrintSortedEntries(orderedEnumerable);
                ReturnToMenu(existingWorkers, repository);
                break;

            case 2:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.Fio);
                PrintSortedEntries(orderedEnumerable);
                ReturnToMenu(existingWorkers, repository);
                break;

            case 3:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.Age);
                PrintSortedEntries(orderedEnumerable);
                ReturnToMenu(existingWorkers, repository);
                break;

            case 4:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.Height);
                PrintSortedEntries(orderedEnumerable);
                ReturnToMenu(existingWorkers, repository);
                break;

            case 5:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.DateOfBirth);
                PrintSortedEntries(orderedEnumerable);
                ReturnToMenu(existingWorkers, repository);
                break;

            case 6:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.PlaceOfBirth);
                PrintSortedEntries(orderedEnumerable);
                ReturnToMenu(existingWorkers, repository);
                break;

            case 7:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.DateOfEntryCreation);
                PrintSortedEntries(orderedEnumerable);
                ReturnToMenu(existingWorkers, repository);
                break;

            default:
                WriteLine("Uncknown command");
                ReturnToMenu(existingWorkers, repository);
                break;
        }
    }

    private static void PrintSortedEntries(IEnumerable<Worker> orderedEnumerable)
    {
        foreach (Worker worker in orderedEnumerable)
            WriteLine(worker.Print());
    }

    private static void PrintFilteredEntriesByDateRange(List<Worker> existingWorkers, Repository repository)
    {
        WriteLine("Please, enter first date (YYYY.MM.DD): ");
        TryParse(ReadLine(), out DateTime from);

        WriteLine("Please, enter second date (YYYY.MM.DD): ");
        TryParse(ReadLine(), out DateTime to);

        Clear();

        List<Worker> filteredWorkers = repository.GetWorkersBetweenTwoDates(from, to, existingWorkers);

        if (filteredWorkers.Any())
            foreach (Worker worker in filteredWorkers)
                WriteLine(worker.Print());
        else
            WriteLine("No entries available");
    }
}