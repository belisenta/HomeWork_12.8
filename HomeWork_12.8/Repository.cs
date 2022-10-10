using static System.Console;
using static System.DateTime;
using static System.Int32;

namespace HomeWork_12._8;

internal class Repository 
{
    private string pathFile;
    private List<Worker> existingWorkers;
    

    public Repository(string pathFile)
    {
        this.pathFile = pathFile;
        CheckIfFileExistAndCreate();
        existingWorkers = GetAllWorkers();
    }

    // Метод хэширует данные из файла
    public void UpdateDatabase()
    {
        existingWorkers = GetAllWorkers();
    }
    
    // Метод считывает данные из файла и возвращает Worker c запрашиваемым ID
    public Worker GetWorkerById(int id)
    {
        Worker worker = existingWorkers.FirstOrDefault(worker => worker.Id == id);
        return worker;
    }

    //Метод выводит в консоль все существующие записи
    public void PrintAllEntries()
    {
        foreach (Worker worker in existingWorkers)
            Console.WriteLine(worker.Print());
    }

    // Метод считывает данные из файла, находит Worker c запрашиваемым ID, записывает в файл все данные, кроме удаляемого
    public void DeleteWorker(int id)
    {
        Worker workerToDelete = GetWorkerById(id);
        existingWorkers.Remove(workerToDelete);
        using StreamWriter sw = new(pathFile, false);
        existingWorkers.ForEach(worker => sw.WriteLine(worker.NewEntry()));
    }

    //Метод удаляет запись в файле по введенному пользователем ID
    public void DeleteEntryByUserInput()
    {
        WriteLine("Please, enter ID of entry you wish to delete: ");
        TryParse(ReadLine(), out int idToDelete);
        Clear();
        DeleteWorker(idToDelete);
    }

    //Метод записывает новый Worker в файл
    public void AddWorker(Worker worker)
    {
        using StreamWriter sw = new(pathFile, true);
        sw.WriteLine(worker.NewEntry());
    }

    //Метод создает новую запись в файле из введенных пользователем параметров
    public void CreateNewEntryFromUserInput()
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
        AddWorker(newWorker);
    }

    //Метод позволяет пользователю выбрать параметр для сортировки и вывода записей
    public void ChooseSortingOfEntriesByParameter()
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

        TryParse(ReadLine(), out int choice);
        IEnumerable<Worker> orderedEnumerable;

        switch (choice)
        {
            case 1:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.Id);
                PrintSortedEntries(orderedEnumerable);
                break;

            case 2:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.Fio);
                PrintSortedEntries(orderedEnumerable);
                break;

            case 3:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.Age);
                PrintSortedEntries(orderedEnumerable);
                break;

            case 4:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.Height);
                PrintSortedEntries(orderedEnumerable);
                break;

            case 5:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.DateOfBirth);
                PrintSortedEntries(orderedEnumerable);
                break;

            case 6:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.PlaceOfBirth);
                PrintSortedEntries(orderedEnumerable);
                break;

            case 7:
                Clear();
                orderedEnumerable = existingWorkers.OrderBy(worker => worker.DateOfEntryCreation);
                PrintSortedEntries(orderedEnumerable);
                break;

            default:
                WriteLine("Uncknown command");
                break;
        }
    }
    
    // Метод считывает данные из файла, фильтрует записи по запрошенному диапазону, возращает массив соответствующих значений
    public List<Worker> GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
    {
        List<Worker> filteredWorkers = new();

        foreach (Worker worker in existingWorkers)
        {
            DateTime dateCompared = worker.DateOfEntryCreation;
            if (DateTime.Compare(dateCompared, dateFrom) >= 0 && DateTime.Compare(dateCompared, dateTo) <= 0)
                filteredWorkers.Add(worker);
        }

        return filteredWorkers;
    }

    // Метод считывает диапазон дат введенных пользователем и выводит в консоль отфильтрованные по данному диапазону записи
    public void PrintEntriesFilteredByUserInsertedDateRange()
    {
        WriteLine("Please, enter first date (YYYY.MM.DD): ");
        TryParse(ReadLine(), out DateTime from);

        WriteLine("Please, enter second date (YYYY.MM.DD): ");
        TryParse(ReadLine(), out DateTime to);

        Clear();

        List<Worker> filteredWorkers = GetWorkersBetweenTwoDates(from, to);

        if (filteredWorkers.Any())
            foreach (Worker worker in filteredWorkers)
                WriteLine(worker.Print());
        else
            WriteLine("No entries available");
    }


    // Метод считывает данные из файла и возвращает массив из Worker
    private List<Worker> GetAllWorkers()
    {
        using StreamReader sr = new(pathFile);
        List<Worker> allWorkers = new();

        while (sr.ReadLine() is { } entry)
        {
            string[] fields = entry.Split("#");
            Worker worker = new()
            {
                Id = Convert.ToInt32(fields[0]),
                Age = Convert.ToInt32(fields[2]),
                Height = Convert.ToInt32(fields[3]),
                DateOfBirth = Convert.ToDateTime(fields[4]),
                DateOfEntryCreation = Convert.ToDateTime(fields[6]),
                Fio = fields[1],
                PlaceOfBirth = fields[5]
            };
            allWorkers.Add(worker);
        }
        return allWorkers;
    }

    // Метод проверяет существует ли файл с данными и создает его, если не находит
    private void CheckIfFileExistAndCreate()
    {
        if (File.Exists(pathFile)) return;
        using StreamWriter sw = new(pathFile, true);
        Worker worker = new();
        sw.WriteLine(worker.NewEntry());
    }

    //Метод выводит в консоль записи отсортированные по параметру 
    private static void PrintSortedEntries(IEnumerable<Worker> orderedEnumerable)
    {
        foreach (Worker worker in orderedEnumerable)
            WriteLine(worker.Print());
    }
}