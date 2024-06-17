using System.Text;

static void GenerateCSV(int recordsToGenerate, string filePath, string fileName)
{
    string[] firstNames = ["John", "James", "Michael", "Robert", "David", "Mary", "Patricia", "Jennifer", "Linda", "Susan"];
    string[] lastNames = ["Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Wilson", "Thomas"];
    string[] hairColours = ["black", "blonde", "ginger", "brown", "grey", "white", "blue"];
    string[] eyeColours = ["blue", "brown", "green", "hazel"];
    int age;

    var csv = new StringBuilder();
    string header = "firstName,lastName,age,eyeColour,hairColour";
    csv.AppendLine(header);

    for (int i = 0; i < recordsToGenerate; i++)
    {
        Random random = new Random();
        string firstName = firstNames[random.Next(0, firstNames.Count())];
        string lastName = lastNames[random.Next(0, lastNames.Count())];
        age = random.Next(1, 99);
        string eyeColour = eyeColours[random.Next(0, eyeColours.Count())];
        string hairColour = hairColours[random.Next(0, hairColours.Count())];

        string entry = $"{firstName},{lastName},{age},{eyeColour},{hairColour}";
        csv.AppendLine(entry);
    }
    File.WriteAllText($"{filePath}\\{fileName}.csv", csv.ToString());
}

static void PrintRow(params string[] columns)
{
    int width = (100 - columns.Length) / columns.Length;
    string row = "|";

    foreach (string column in columns)
    {
        row += AlignCentre(column, width) + "|";
    }

    Console.WriteLine(row);
}

static string AlignCentre(string text, int width)
{
    text = text.Length > width ? text.Substring(0, width - 3) + "..." : text;

    if (string.IsNullOrEmpty(text))
    {
        return new string(' ', width);
    }
    else
    {
        return text.PadRight(width - (width - text.Length) / 2).PadLeft(width);
    }
}

static void DisplayCSV(string filePath)
{
    string line;
    List<Person> People = new List<Person>();
    // Todo: Validate filePath and make sure a csv exists before continuing.
    StreamReader sr = new StreamReader(filePath);
    int iterator = 0;
    line = sr.ReadLine();

    Console.WriteLine($" {String.Concat(Enumerable.Repeat("_", 99))}");
    while (line != null)
    {
        if (iterator == 0)
        {
            string[] header = line.Split(",");
            PrintRow(header[0], header[1], header[2], header[3], header[4]);
            Console.WriteLine($" {String.Concat(Enumerable.Repeat("_", 99))}");
        }
        else
        {
            string[] details = line.Split(",");
            People.Add(new Person
            {
                FirstName = details[0],
                LastName = details[1],
                Age = Int32.Parse(details[2]),
                EyeColour = details[3],
                HairColour = details[4]
            });
        }

        iterator++;
        line = sr.ReadLine();
    }
    foreach (Person person in People)
    {
        PrintRow(person.FirstName, person.LastName, person.Age.ToString(), person.EyeColour, person.HairColour);
    }
    Console.WriteLine($" {String.Concat(Enumerable.Repeat("_", 99))}");
    sr.Close();
}

static bool ValidateOptions(string userInput, string[] possibleOptions)
{
    if (possibleOptions.Contains(userInput))
    {
        return true;
    }
    Console.WriteLine("Invalid option. Please only choose 1 or 2.");
    return false;
}

static void MenuLoop()
{
    Console.WriteLine("Would you like to return to the main menu?");
    string request = Console.ReadLine();
    if (request == "yes" || request == "y")
    {
        OpenMenu();
    }
    else if (request == "no" || request == "n")
    {
        Console.WriteLine("Closing console.");
        Environment.Exit(0);
    }
    else
    {
        Console.WriteLine("Invalid option. Please input yes, y, no, or n.");
        MenuLoop();
    }
}

static void OpenMenu()
{
    Console.WriteLine("Howdy, would you like to display a CSV or Generate one?");
    Console.WriteLine("1. Display a CSV");
    Console.WriteLine("2. Generate a CSV");
    string option = Console.ReadLine();
    while (!ValidateOptions(option, ["1", "2"]))
    {
        option = Console.ReadLine();
    }

    if (option == "1")
    {
        Console.WriteLine("Please provide the path for the file.");
        string path = Console.ReadLine();
        DisplayCSV(path);
        MenuLoop();
    }

    if (option == "2")
    {
        Console.WriteLine("Please provide a file name for your .csv.");
        string fileName = Console.ReadLine();
        Console.WriteLine("Please provide a location to store the csv. I.e 'C:\\Users\\John\\Desktop' ");
        string filePath = Console.ReadLine();
        Console.WriteLine("Please provide the amount of records you would like to generate.");
        string numberOfRecords = Console.ReadLine();
        bool isNumeric = Int32.TryParse(numberOfRecords, out _);
        while (!isNumeric)
        {
            Console.WriteLine($"{numberOfRecords} is not a number. Please try again");
            numberOfRecords = Console.ReadLine();
        }
        GenerateCSV(Int32.Parse(numberOfRecords), filePath, fileName);
        MenuLoop();
    }
}

try
{
    OpenMenu();
}
finally
{
}

public class Person
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string HairColour { get; set; }
    public string EyeColour { get; set; }
    public int Age { get; set; }

}

// See https://aka.ms/new-console-template for more information
