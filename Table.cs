// Klass för ett bord med egenskaper Obs! cw på svenska för test. 

public class Table
{
    //int XPosition { get; set; }
    //int YPosition { get; set; }
    // Bordsnummer för bordskarta/lista
    public int Number { get; set; }

    // testar en bool för att visa öppna bord
    public bool Status { get; set; } // status/closed? ska man göra lång set med if som skriver ut open/closed?

    // Storlek för bordskarta gällande ex. bokningsläge (innebär: antalpers som ryms på bord)
    public int Size { get; set; }
    // bordsspecifik lista för produkter
    public Table(int number, bool status, int size)
    {
        Number = number;
        Status = status;
        Size = size;
        //tableProductList = new List<Product>();
    }
    public Table(bool status)
    {
        Status = status;
    }
}

// Bordslistan och metoder för att hantera bord
public class TableHandler
{
    // lista med bord
    public List<Product> tableProductList { get; set; } //= new();

    public static List<Table> tables { get; set; }
    public TableHandler()
    {
        tables = new List<Table>();
        tableProductList = new List<Product>();
    }

    public static void TableMenu(int number, bool status, int size, Product product)
    {
        //startvärden
        //int number = 0;
        //bool status = false;
        //TestTables();
        // Console.Clear();
        while (true)
        {
            // TODO lägga till TypeOfUser från user sen
            bool admin = true;

            Console.WriteLine("1. Visa bordslista.");
            Console.WriteLine("2. Öppna bord.");
            Console.WriteLine("3. Stänga bord.");
            Console.WriteLine("4. Stänga alla bord.");
            Console.WriteLine("8 tillbaks till ORDER"); // fixa ordning osv.

            // dessa kräver adminlevel typ
            if (admin)
            {
                Console.WriteLine("5. Skapa nytt bord.");
                Console.WriteLine("6. Ta bort bord.");
                Console.WriteLine("7. Redigera bord.");
            }
            Console.WriteLine("Q. Avsluta.");
            string? choice = Console.ReadLine().ToUpper(); // tryparse senare

            if (choice == "1")
            {
                ShowTables();
            }
            else if (choice == "2")
            {
                OpenTable(number);
            }
            else if (choice == "3")
            {
                CloseTable(number);
            }
            else if (choice == "4")
            {
                CloseAllTables();
            }
            else if (choice == "5" && admin)
            {
                AddTable(number, status, size);
            }
            else if (choice == "6" && admin)
            {
                RemoveTable(number);
            }
            else if (choice == "7" && admin)
            {
                EditTable(number, size);
            }
            else if (choice == "8")
            {
                //UserInterFace.Order(product);
            }
            else if (choice == "Q")
            {
                Console.WriteLine("Valfri tangent för att avsluta.");
                Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("Ogiltigt val");
            }
        }
    }

    // lite testbord ska finnas i ev. Json
    public static void TestTables()
    {
        // loop för att skapa lite testobjekt
        for (int i = 0; i < 5; i++)
        {
            bool status = false;
            int number = i + 1;
            int size = 4;

            Table newTable = new Table(number, status, size);
            tables.Add(newTable);

        }
    }
    // TODO lägga till produkter på ngt sätt.
    // metod för att öppna bord (söka upp i lista för vidare instruktioner), registrerar bord som öppet // onödig funktion??
    public static void OpenTable(int number)
    {
        Console.WriteLine();
        Console.WriteLine("Ange bordsnummer:");

        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out number))
        {
            Console.Write($"Vill du öppna {number}. J/N? ");
            string? input = Console.ReadLine().ToUpper();

            if (input == "J")
            {
                Table tableToOpen = tables.Find(tables => tables.Number == number);

                if (tableToOpen != null) // behövs null?
                {
                    //Checkar om status är true
                    tableToOpen.Status = true;

                    Console.WriteLine($"bord: {number}. är öppnat.");


                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Ogiltigt bordsnummer! Försök igen");
                }
            }
            else if (input == "N")
            {
                Console.WriteLine("Avbruten.");
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
            }
        }
    }

    // Metod för att stänga ALLA bord
    public static void CloseTable(int number) // close ALL tables
    {

        Console.WriteLine();
        ShowOpenTables(); //Lista använda bord
        Console.WriteLine();
        Console.WriteLine("Vilket bord vill du stänga? Ange bordsnummer:");
        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out number))
        {
            Console.Write($"Vill du stänga {number}. J/N? ");
            string? input = Console.ReadLine().ToUpper();

            if (input == "J")
            {
                // söker upp bordet efter bordsnummer
                Table tableToOpen = tables.Find(tables => tables.Number == number);

                if (tableToOpen != null) // checkar så bordet finns
                {
                    //Checkar om status är true
                    tableToOpen.Status = false;

                    Console.WriteLine($"bord: {number}. är stängt.");


                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Ogiltigt bordsnummer! Försök igen");
                }
            }
            else if (input == "N")
            {
                Console.WriteLine("Avbruten.");
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
            }
        }
    }
    // Metod för att stänga alla bord. Behov vid ex. dagsavslut.
    public static void CloseAllTables()
    {

        ShowTables();
        foreach (Table t in tables)
        {
            if (t.Status) // måste nog söka fram borden som e öppna
            {
                // TODO kolla om de finns bongade produkter på bordet
                int products = 0;
                if (products != 0)
                {
                    Console.WriteLine(t.Number + "Har produkter kvar! Vill du ändå stänga bordet? J/N?");
                    string? input = Console.ReadLine();
                    if (input == "J")
                    {
                        //TODO voida produkter typ

                    }
                    break;//? continue? return?
                }
                t.Status = false;
            }
        }
        Console.WriteLine("Alla bord är stängda.");
        Console.WriteLine();
    }
    // metod för att visa borden (listan)
    public static void ShowTables() // isVisible?
    {
        // //startvärden
        // int number = 0;
        // bool status = false;
        // int size = 0;
        //TableHandler table= new Table(0, false, 0);
        TestTables();
        //testar admin
        bool admin = false;
        //Fyrkantsemoji som får symbolisera ett bord
        string bord = "\u25A0"; // in i Table?
        Console.WriteLine();
        for (int i = 0; i < tables.Count; i++)
        {
            // checkar om bord är status och markerar rött
            if (!tables[i].Status)
            {
                if (admin)
                {
                    Console.WriteLine($"Bord: {tables[i].Number}, {tables[i].Size}.");
                }
                Console.WriteLine($"Bord: {tables[i].Number}.");
            }
            else
            {
                //TODO fixa redundans 
                if (admin)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{bord} Bord: {tables[i].Number}, {tables[i].Size}.");
                    Console.ResetColor();
                }
                Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine($"{bord} Bord: {tables[i].Number}.{tables[i].TableProductList}");
                Console.ResetColor();
            }
        }
        Console.WriteLine();
    }

    // metod för att visa alla öppna bord (även tomma) OBS! Kanske räcker med föregående metod
    public static void ShowOpenTables()
    {
        //TestTables();
        for (int i = 0; i < tables.Count; i++)
        {
            string bord = "\u25A0";
            // checkar om bord är status och markerar rött och "bordsemoji"
            if (tables[i].Status)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{bord} Bord: {tables[i].Number}.");
                Console.ResetColor();
            }
        }
    }

    // metod för att visa varor på bord, ska det läggas till en annan metod för hantering av bordsinnehåll?
    public void HandleTableContents(int number, TableHandler tableHandler)// endast send? eller addtotable?
    {
        Console.Write("välj bordsnummer: ");

        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out number))
        {
            Table tableToDisplay = tables.Find(tables => tables.Number == number);
            if (tableToDisplay != null) // checkar så bordet finns
            {
                //ska det checkas om status är true?

                foreach (Product p in tableHandler.tableProductList)
                {
                    Console.WriteLine(p.Name + " " + p.Price);
                    Console.WriteLine("TEST tableproductlist");
                }
            }
        }
        Console.Write($"Vill lägga skicka order till betalning? J/N?");
        //cw skicka vissa articklar till betalnng
        //cw markera vissa och skicka till betalning eller annat bord?
        string? input = Console.ReadLine().ToUpper();

        if (input == "J")
        {
            //här ska aktuella produkter skickas till betalning
            UserInterFace.Payment();
            //TODO stäng bordet


        }
        else if (input == "N")
        {
            Console.WriteLine("Avbruten.");
        }
        else
        {
            Console.WriteLine("Ogiltigt val.");
        }
    }

    // metod för att splitta bord (ex 3 öl. ska dom till annat/nytt bord eller betalas?)
    public void SplitTable()// Split, splitCheck?
    {

    }

    // metod för att radera produkter eller tömma bord. (krävs hov/admin? Ange behörighet alt redan inloggad)
    public void VoidOrder() // oklart namn ta bort varje produkt i listan
    {

    }


    // metod för att lägga en order till ett bord
    public static void OrderToTable(int number, bool status, Product product, TableHandler tableHandler)
    {

        // lägger till produkter i en tillfällig lista
        // find efter bordsid
        //TestTables();
        ShowTables();// ska detta va här eller en ny metod i TableHandler?
        Console.Write("välj bordsnummer: ");

        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out number))
        {
            Console.Write($"Vill lägga order på bord {number}. J/N? (Ps. onödigt steg.) ");
            string? input = Console.ReadLine().ToUpper();

            if (input == "J")
            {

                // söker upp bordet efter bordsnummer
                Table tableToAddOrder = tables.Find(tables => tables.Number == number);

                if (tableToAddOrder != null) // checkar så bordet finns
                {
                    tableToAddOrder.Status = true;
                    //tableHandler.tableProductList.Add(product); //TODO stämmer denna referens? 
                    foreach (Product p in UserInterFace.orderList)
                    {
                        tableHandler.tableProductList.Add(p);
                    }
                    // här ska ordern skickas ut och in i en lista på bordet... ska bordslistan skapas vid varje bord?


                    // bli mer bordsspecifik
                    if (status)
                    {
                        Console.WriteLine("Det finns redan produkter på bordet. Vill fortsätta? J/N?");//
                        string? choice = Console.ReadLine().ToUpper();
                        if (choice == "J")
                        {
                            foreach (Product p in UserInterFace.orderList)
                            {
                                tableHandler.tableProductList.Add(p);
                            }
                            Console.WriteLine("Du har lagt följande produkter på bord");

                            // foreach (Product j in tableHandler.tableProductList)
                            // {
                            //     Console.WriteLine($"hallå");
                            // }
                        }
                        else
                        {

                            Console.WriteLine("ajjabajja");

                        }
                    }

                }

                else
                {

                    Console.WriteLine("Ogiltigt bordsnummer! Försök igen");
                }
            }
            else if (input == "N")
            {
                Console.WriteLine("Avbruten.");
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
            }
        }
        foreach (Product p in tableHandler.tableProductList)
        {
            Console.WriteLine(p.Name + " " + p.Price); // varför bara en
        }
        Console.WriteLine("Order skickas till kök TODO");
        //UserInterFace.Order(product); // fixa bong
    }



    // meny för att köra tablehandler och testa
    // Funktion för ex. hov/admin där man skapar borden till sin restaurang/avdelning (relaterar mest till bokningsfunktioner)
    public static void AddTable(int number, bool status, int size)
    {
        Console.WriteLine();
        Console.WriteLine("Skapa nytt bord.");
        Console.WriteLine("Ange bordsnr:");
        number = int.Parse(Console.ReadLine()); //Fixa tryparse

        Console.WriteLine("Lägg till storlek:"); //sittplatser? stolar?
        size = int.Parse(Console.ReadLine());

        // Adda funktion för att kolla så inte nr finns
        Table newTable = new Table(number, status, size);
        tables.Add(newTable);

        //getPosition
    }

    // adminfunktion för att ta bort bord från lista
    public static void RemoveTable(int number)
    {

        Console.WriteLine();
        ShowTables(); //Lista använda bord
        Console.WriteLine();
        Console.WriteLine("Vilket bord vill du stänga? Ange bordsnummer:");
        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out number))
        {
            Console.Write($"Vill du ta bort bord {number}. J/N? ");
            string? input = Console.ReadLine().ToUpper();

            if (input == "J")
            {
                // söker upp bordet efter bordsnummer
                Table tableToRemove = tables.Find(tables => tables.Number == number);

                if (tableToRemove != null) // checkar så bordet finns
                {
                    //Checkar om status är true
                    tables.Remove(tableToRemove);

                    Console.WriteLine($"bord: {number}. är borttaget.");


                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Ogiltigt bordsnummer! Försök igen");
                }
            }
            else if (input == "N")
            {
                Console.WriteLine("Avbruten.");
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
            }
        }
    }

    // adminfunktion för att redigera bord i lista dvs. ändra storlek
    public static void EditTable(int number, int size)
    {
        Console.WriteLine();
        ShowTables(); //Lista använda bord
        Console.WriteLine();
        Console.WriteLine("Vilket bord vill du redigera? Ange bordsnummer:");
        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out number))
        {
            Console.Write($"Vill redigera bord {number}. J/N? ");
            string? input = Console.ReadLine().ToUpper();

            if (input == "J")
            {
                // söker upp bordet efter bordsnummer
                Table tableToEdit = tables.Find(tables => tables.Number == number);

                if (tableToEdit != null) // checkar så bordet finns
                {
                    Console.WriteLine("ange ny storlek.");

                    tableToEdit.Size = int.Parse(Console.ReadLine());

                    Console.WriteLine($"bord: {number}. storlek ändrat till {size}.");


                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Ogiltigt bordsnummer! Försök igen");
                }
            }
            else if (input == "N")
            {
                Console.WriteLine("Avbruten.");
            }
            else
            {
                Console.WriteLine("Ogiltigt val.");
            }
        }
    }
}


// försök till lite grafisk bordskart. Work in progress :)

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



