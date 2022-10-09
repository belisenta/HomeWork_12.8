namespace HomeWork_12._8;

internal struct Worker
{
    public int Id { get; set; }

    public string Fio { get; set; }

    public int Age { get; set; }

    public int Height { get; set; }

    public DateTime DateOfBirth { get; set; }

    public string PlaceOfBirth { get; set; }

    public DateTime DateOfEntryCreation { get; set; }
    
    public string Print()
    {
        return
            $"ID: {Id} " +
            $"\nName: {Fio} " +
            $"\nAge: {Age} " +
            $"\nHeight: {Height} " +
            $"\nDate of birth: {DateOfBirth:d} " +
            $"\nPlace of birth: {PlaceOfBirth} " +
            $"\nEntry created: {DateOfEntryCreation:d}\n";
    }

    public string NewEntry()
    {
        return $"{Id}#{Fio}#{Age}#{Height}#{DateOfBirth:d}#{PlaceOfBirth}#{DateOfEntryCreation:d}";
    }
    
    public Worker(int id, string fio, int age, int height, DateTime dateOfBirth, string placeOfBirth, DateTime dateOfEntryCreation)
    {
        Id = id;
        Fio = fio;
        Age = age;
        Height = height;
        DateOfBirth = dateOfBirth;
        PlaceOfBirth = placeOfBirth;
        DateOfEntryCreation = dateOfEntryCreation;
    }
}