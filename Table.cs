// Klass för ett bord med egenskaper

public class Table
{
    //int XPosition { get; set; }
    //int YPosition { get; set; }
    
    // Bordsnummer för bordskarta/lista
    public int Number {get; set;}

    // testar en bool för att visa öppna bord
    public bool Open {get; set;} // status kanske?
    
    // Storlek för bordskarta gällande ex. bokningsläge (innebär: antalpers som ryms på bord)
    int Size {get;set;}
    public Table(int number, bool open)
    {
        Number = number;
        Open = open;
    }
}

// Bordslistan och metoder för att hantera bord
public class TableHandler
{ 
    // lista med bord
    public List<Table> tables { get; set;} //= new();
    public TableHandler()
    {
        tables = new List<Table>();
    }

    // lite testbord
    public void TestTables(int number, bool open)
    {   
        // loop för att skapa lite testobjekt
        for (int i = 0; i < 10; i++)
        {   
            open = false;
            number = i + 1;
            Table newTable = new Table(number, open);
            tables.Add(newTable);
        }
    }
    
    // metod för att öppna bord (söka upp i lista för vidare instruktioner), registrerar bord som öppet
    public void OpenTable(int number, bool open)
    {
        open = true;
    }

    // Metod för att stänga bord
    public void CloseTable(int number, bool open)
    {
        Console.WriteLine("STÄNGA BORD");
        ShowOpenTables(open); //Lista använda bord 
        Console.Write("Vilket bord vill du stänga? Ange bordsnummer:");
        int nr = int.Parse(Console.ReadLine());

        // OBS! number funkar inte stänger alla
        foreach (Table t in tables)
        {
            if (open) // måste nog söka fram borden som e öppna
            {
                Console.Write("");
                number = nr;
                //if (products != 0)
                //{
                    //Console.WriteLine(t.number + "Har produkter kvar! Vill du ändå stänga bordet?");
                //}
                open = !open;
                Console.WriteLine("bord " + t.Number + " är stängt");
            }
        }
    }

    // metod för att visa borden (listan)
    public void ShowTables() // isVisible?
    {   
        string bord = "\u25A0"; // in i Table?
        bool open = false;
        for(int i = 0; i < tables.Count; i++)
        {   
            // checkar om bord är open och markerar rött
            if (!open)
            {   
                Console.WriteLine($"Bord: {tables[i].Number}.");
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{bord} Bord: {tables[i].Number}.");
            Console.ResetColor();
        }
    }

    // metod för att visa alla öppna bord (även tomma) OBS! Kanske räcker med föregående metod
    public void ShowOpenTables(bool open)
    {
        
        for(int i = 0; i < tables.Count; i++)
        {
            string bord = "\u25A0";   
            // checkar om bord är open och markerar rött
            if (open)
            {   
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{bord} Bord: {tables[i].Number}.");
                Console.ResetColor();        
            }
        }
        
    }

    // metod för att "bonga" produkter på bord (send)
    public void SendToTable()// endast send? eller addtotable?
    {

    }

    // metod för att splitta bord (ex 3 öl. ska dom till annat/nytt bord eller betalas?)
    public void SplitTable()// Split, splitCheck?
    {

    }

    // metod för att radera produkter eller tömma bord. (krävs hov/admin? Ange behörighet alt redan inloggad)
    public void VoidOrder() // oklart namn
    {

    }


    // metod för att skapa en order
    public void Order()
    {

    }





    // Funktion för ex. hov/admin där man skapar borden till sin restaurang/avdelning (relaterar mest till bokningsfunktioner)
    public static void AddTable()
    {
        Console.WriteLine("Lägg till bordsnr:");
        int number = int.Parse(Console.ReadLine()); //Fixa tryparse

        Console.WriteLine("Lägg till storlek (antal pers):"); //sittplatser? stolar?
        int size =int.Parse(Console.ReadLine());

        //getPosition

        //listOfTables.Add
    }

    // adminfunktion för att ta bort bord från lista
    public static void RemoveTable(){}

    // adminfunktion för att redigera bord i lista
    public static void ChangeTable(){}
}

// public class TableMap : Table//klass för en bordskarta, testar arv
// {
//     public string? Name { get; set; }
//     public int Width { get; set; }
//     public int Height { get; set; }
//     public List<TableMap> tableMaps { get; set; } //= new List<TableMap>();
//     public TableMap(string? name, int width, int height)// behov?
//     {
//         Name = name;
//         Width = width;
//         Height = height;
//         tableMaps = new List<TableMap>();
    
//     }
//     public void CreateMap()
//     {
//         Console.WriteLine("Vill du skapa ny bordskarta eller redigera en sparad?");
//         Console.WriteLine("1. för skapa ny.");
//         Console.WriteLine("2. ladda en sparad karta.");
//         string? choice = Console.ReadLine();
//         if (choice == "1")
//         {
//             Console.WriteLine("Ange namn för din bordskarta");
//             string name = Console.ReadLine();
//             while (true) // borde ha bättre loopar
//             {
//                 Console.Write("Hur bred ska kartan vara?\nange 0 för standard"); //fixa lite tryparse osv
//                 int width = int.Parse(Console.ReadLine());

//                 Console.Write("Hur hög ska kartan vara?\nange 0 för standard"); // tryparse senare
//                 int height = int.Parse(Console.ReadLine());

//                 string? xline;
//                 string? yline = null;
//                 string? space;

//                 if (width == 0 && height == 0)
//                 {
//                     width = 50;
//                     xline = new string('*', width);
//                     Console.WriteLine(xline);
//                     height = 25;
//                     for (int i = 0; i <= height; i++)
//                     {
//                         yline = "*";
//                         space = new string(' ', width - 2);
//                         Console.WriteLine($"{yline}{space}{yline}");
//                     }
//                     xline = new string('*', width - 2);
//                     Console.WriteLine($"{yline}{xline}{yline}");
//                 }
//                 else if (height <= 0 || height >= 50 || width <= 0 || width >= 100)
//                 {
//                     Console.WriteLine("Fel storlek! Försök igen.");
//                     continue;
//                 }

//                 xline = new string('*', width);
//                 Console.WriteLine(xline);

//                 for (int i = 0; i <= height; i++)
//                 {
//                     yline = "*";
//                     space = new string(' ', width - 2);
//                     Console.WriteLine($"{yline}{space}{yline}");
//                 }

//                 xline = new string('*', width - 2);
//                 Console.WriteLine($"{yline}{xline}{yline}");
//                 Console.WriteLine(name);

//                 Console.WriteLine("'E' för att fortsätta redigera.");
//                 Console.WriteLine("'S' för att spara.");

//                 Console.WriteLine("'Q' för avsluta.");
//                 choice = Console.ReadLine().ToUpper();
//                 if (choice == "S")
//                 {
//                     AddMap(width, height, name);// Ska väl sparas till ngn fil
//                     break;
//                 }
//                 else if (choice == "E")
//                 {
//                     continue;
//                 }
//                 else if (choice == "Q")
//                 {
//                     break;
//                 }
//             }
//         }
//         else if (choice == "2")
//         {
//             PrintMap();
//             Console.WriteLine("Ange kartnummer för att öppna");
//             // .find osv.
//             // AddTableToMap
//             Console.ReadKey();

//         }
//     }
//     public void AddTableToMap(int width, int height, string? name) // DockTable, GetTable? bättre namn asap:)
//     {
//         // kolla doom? spelet för inspiration
//         // kunna lägga in emoji-bord
//         // flytta bordet och fästa bordet på plats.
//         // Oklar logik kring loopar och y,x -positioner
        
//         Console.Clear();
//         Console.CursorVisible = false;
//         string tableSymbol = "\u25A0";
//         bool bordsloop = true;
//         // while (bordsloop)
//         // {
//             string? xline;
//             string? yline = null;
//             string? space;

//             // testar med standardkarta
//             width = 50;
//             xline = new string('*', width);
//             Console.WriteLine(xline);
//             height = 25;
//             for (int i = 0; i <= height; i++)
//                 {
//                         yline = "*";
//                         space = new string(' ', width - 2);
//                         Console.WriteLine($"{yline}{space}{yline}");
//                     }
//                     xline = new string('*', width - 2);
//                     Console.WriteLine($"{yline}{xline}{yline}");
//                     Console.WriteLine(tableSymbol);

//         //}

//     }
//     // metod för att flytta runt bord på karta
//     public void TableMovement(int xPosition, int yPosition, int width, int height)
//     {
//             int newX = xPosition;
//             int newY = yPosition;
//             bool isValidKey = true;

//             ConsoleKeyInfo keyInfo = Console.ReadKey();
//             switch (keyInfo.Key)
//             {
//                 case ConsoleKey.RightArrow: newX++; break;
//                 case ConsoleKey.LeftArrow: newX--; break;
//                 case ConsoleKey.UpArrow: newY--; break;
//                 case ConsoleKey.DownArrow: newY++; break;
//                 default: isValidKey = false; break;
//             }

//             // lägga till typ "space" för att låsa och esc för avbryta?
//             if (isValidKey &&
//                 newX >= 0 && newX < width &&
//                 newY >= 0 && newY < height)
//             {
//                 xPosition = newX;
//                 yPosition = newY;
//             }
//     }    
//     public void AddMap(int width, int height, string? name)
//     {
//         TableMap newMap = new(name, width, height);
//         tableMaps.Add(newMap);
//     }
//     public void PrintMap()
//     {
//         for (int i = 0; i < tableMaps.Count; i++)
//         {
//             Console.WriteLine($"{i+1}. {tableMaps[i].Name}");
//         }
//     }
// }


   
