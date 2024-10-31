
public class Table
{
    int XPosition { get; set; }
    int YPosition { get; set; }
    int Number {get; set;}
    int Size {get;set;}
    // public Table(int xPosition, int yPosition, int number)
    // {
    //     XPosition = xPosition;
    //     YPosition = yPosition;
    //     Number = number;
    // }
}

public class TableHandler
{ 

    public static List<Table> listOfTables = new();
    public static void AddTable()
    {
        Console.WriteLine("Lägg till bordsnr:");
        int number = int.Parse(Console.ReadLine());

        Console.WriteLine("Lägg till antal personer:");
        int size =int.Parse(Console.ReadLine());

        //getPosition

        //listOfTables.Add
    }
    public static void RemoveTable(){}
    public static void ChangeTable(){}
}

public class TableMap : Table//klass för en bordskarta, testar arv
{
    public string? Name { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<TableMap> tableMaps { get; set; } //= new List<TableMap>();
    public TableMap(string? name, int width, int height)// behov?
    {
        Name = name;
        Width = width;
        Height = height;
        tableMaps = new List<TableMap>();
    
    }
    public void CreateMap()
    {
        Console.WriteLine("Vill du skapa ny bordskarta eller redigera en sparad?");
        Console.WriteLine("1. för skapa ny.");
        Console.WriteLine("2. ladda en sparad karta.");
        string? choice = Console.ReadLine();
        if (choice == "1")
        {
            Console.WriteLine("Ange namn för din bordskarta");
            string name = Console.ReadLine();
            while (true) // borde ha bättre loopar
            {
                Console.Write("Hur bred ska kartan vara?\nange 0 för standard"); //fixa lite tryparse osv
                int width = int.Parse(Console.ReadLine());

                Console.Write("Hur hög ska kartan vara?\nange 0 för standard"); // tryparse senare
                int height = int.Parse(Console.ReadLine());

                string? xline;
                string? yline = null;
                string? space;

                if (width == 0 && height == 0)
                {
                    width = 50;
                    xline = new string('*', width);
                    Console.WriteLine(xline);
                    height = 25;
                    for (int i = 0; i <= height; i++)
                    {
                        yline = "*";
                        space = new string(' ', width - 2);
                        Console.WriteLine($"{yline}{space}{yline}");
                    }
                    xline = new string('*', width - 2);
                    Console.WriteLine($"{yline}{xline}{yline}");
                }
                else if (height <= 0 || height >= 50 || width <= 0 || width >= 100)
                {
                    Console.WriteLine("Fel storlek! Försök igen.");
                    continue;
                }

                xline = new string('*', width);
                Console.WriteLine(xline);

                for (int i = 0; i <= height; i++)
                {
                    yline = "*";
                    space = new string(' ', width - 2);
                    Console.WriteLine($"{yline}{space}{yline}");
                }

                xline = new string('*', width - 2);
                Console.WriteLine($"{yline}{xline}{yline}");
                Console.WriteLine(name);

                Console.WriteLine("'E' för att fortsätta redigera.");
                Console.WriteLine("'S' för att spara.");

                Console.WriteLine("'Q' för avsluta.");
                choice = Console.ReadLine().ToUpper();
                if (choice == "S")
                {
                    AddMap(width, height, name);// Ska väl sparas till ngn fil
                    break;
                }
                else if (choice == "E")
                {
                    continue;
                }
                else if (choice == "Q")
                {
                    break;
                }
            }
        }
        else if (choice == "2")
        {
            PrintMap();
            Console.WriteLine("Ange kartnummer för att öppna");
            // .find osv.
            // AddTableToMap
            Console.ReadKey();

        }
    }
    public void AddTableToMap(int width, int height, string? name) // DockTable, GetTable? bättre namn asap:)
    {
        // kolla doom? spelet för inspiration
        // kunna lägga in emoji-bord
        // flytta bordet och fästa bordet på plats.
        // Oklar logik kring loopar och y,x -positioner
        
        Console.Clear();
        Console.CursorVisible = false;
        string tableSymbol = "\u25A0";
        bool bordsloop = true;
        while (bordsloop)
        {
            string? xline;
            string? yline = null;
            string? space;

            // testar med standardkarta
            width = 50;
            xline = new string('*', width);
            Console.WriteLine(xline);
            height = 25;
            for (int i = 0; i <= height; i++)
                {
                        yline = "*";
                        space = new string(' ', width - 2);
                        Console.WriteLine($"{yline}{space}{yline}");
                        Console.WriteLine(tableSymbol);
                    }
                    xline = new string('*', width - 2);
                    Console.WriteLine($"{yline}{xline}{yline}");

        }

    }
    // metod för att flytta runt bord på karta
    public void TableMovement(int xPosition, int yPosition, int width, int height)
    {
            int newX = xPosition;
            int newY = yPosition;
            bool isValidKey = true;

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            switch (keyInfo.Key)
            {
                case ConsoleKey.RightArrow: newX++; break;
                case ConsoleKey.LeftArrow: newX--; break;
                case ConsoleKey.UpArrow: newY--; break;
                case ConsoleKey.DownArrow: newY++; break;
                default: isValidKey = false; break;
            }

            // lägga till typ "space" för att låsa?
            if (isValidKey &&
                newX >= 0 && newX < width &&
                newY >= 0 && newY < height)
            {
                xPosition = newX;
                yPosition = newY;
            }
    }    
    public void AddMap(int width, int height, string? name)
    {
        TableMap newMap = new(name, width, height);
        tableMaps.Add(newMap);
    }
    public void PrintMap()
    {
        for (int i = 0; i < tableMaps.Count; i++)
        {
            Console.WriteLine($"{i+1}. {tableMaps[i].Name}");
        }
    }
}


   
