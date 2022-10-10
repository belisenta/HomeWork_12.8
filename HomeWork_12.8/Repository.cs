using static System.Console;
using static System.DateTime;
using static System.Int32;

namespace HomeWork_12._8;

internal class Repository 
{
    private string pathFile;
    internal List<Worker> existingWorkers;
    

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

    //Метод записывает новый Worker в файл
    public void AddWorker(Worker worker)
    {
        using StreamWriter sw = new(pathFile, true);
        sw.WriteLine(worker.NewEntry());
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
}