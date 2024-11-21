// Klass för ett bord med egenskaper Obs! cw på svenska för test. 

#region Table

public class Table
{
    // Bordsspecifik lista för produkter
    //public List<Product> TableList { get; set; } //= new();
    //public Order Order { get; set; }
    public Order? TableOrder { get; set; }

    // Bordsnummer för bordskarta/lista
    public int Number { get; set; }
    // Bool för att visa bordets status öppet/stängt
    public bool Status { get; set; } // 
    // Storlek på bord för bordskarta/bokningsprogram
    public int Size { get; set; }
    public Table(int number, bool status, int size)
    {
        Number = number;
        Status = status;
        Size = size;
        //TableList = new List<Product>(); //TODO ska den ligga här? JA!
    }
    public Table() { }
}

#endregion
#region TableHandler
// Bordslistan och metoder för att hantera bord och ordrar med produkter
public class TableHandler
{
    public static int CurrentTable { get; set; }
    // lista med bord
    public static List<Table> Tables = new(); //{ get; set; }
    public TableHandler()
    {
    }
    #region TableMenu
    public static void TableMenu()
    {
        //startvärden
        //int number = 0;
        //bool status = false;
        //TestTables();
        // Console.Clear();

        //UserHandler.IsAdmin();// kallar på metod IsAdmin och checkar Admin bool för inloggad user

        //UserHandler.IsAdmin();
        while (true)
        {
            // TODO lägga till TypeOfUser från user sen
            //bool admin = true;

            Console.WriteLine("1. Visa bordslista.");
            Console.WriteLine("2. Visa öppna bord.");
            Console.WriteLine("3. Stänga bord.");
            Console.WriteLine("4. Stänga alla bord.");

            // dessa kräver adminlevel typ
            if (User.Admin)
            {
                Console.WriteLine("5. Skapa nytt bord.");
                Console.WriteLine("6. Ta bort bord.");
                Console.WriteLine("7. Redigera bord.");
            }
            Console.WriteLine("Q. Tillbaks till startmenyn.");
            string? choice = Console.ReadLine().ToUpper();

            if (choice == "1")
            {
                ShowTables();
            }
            else if (choice == "2")
            {
                OpenTable();
                //ShowOpenTables(); // Oklart behov :)
            }
            else if (choice == "3")
            {
                CloseTable();
            }
            else if (choice == "4")
            {
                CloseAllTables(); // TODO kolla så den checkar om bordet har produkter kvar

            }
            else if (choice == "5" && User.Admin)
            {
                AddTable();
            }
            else if (choice == "6" && User.Admin)
            {
                RemoveTable();
            }
            else if (choice == "7" && User.Admin)
            {
                EditTable();
            }
            else if (choice == "Q")
            {
                Console.WriteLine("Valfri tangent för att avsluta.");
                //Console.ReadKey();
                break;
            }
            else
            {
                Console.WriteLine("Ogiltigt val");
            }
        }
    }
    #endregion
    #region GenerateTables
    // En metod som loopar fram lite testbord till bordslistan 
    public static void GenerateTables()
    {

        for (int i = 0; i < 5; i++)
        {
            bool status = false;
            int number = i + 1;
            int size = 4;

            Table newTable = new Table(number, status, size);
            Tables.Add(newTable);

        }
    }
    #endregion
    #region OpenTable
    // Metod för att öppna bord (söka upp i lista för vidare instruktioner), registrerar bord som öppet // överflödig metod. 
    public static void OpenTable()
    {
        Console.WriteLine();
        Console.WriteLine("Ange bordsnummer:");
        // tar in bordsnumret
        string? nr = Console.ReadLine();
        // konverterar till int
        if (int.TryParse(nr, out int number))
        {
            Console.Write($"Vill du öppna {number}. J/N? "); // Extra steg i felhanteringssyfte
            string? input = Console.ReadLine().ToUpper();

            if (input == "J")
            {
                // Söker upp bord efter dess number
                Table tableToOpen = Tables.Find(tables => tables.Number == number);

                // Checkar Så bordet finns
                if (tableToOpen != null)
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
    #endregion
    #region CloseTable
    // Metod för att stänga ett bord tex. vid dagsavslut (oklart behov)
    public static void CloseTable()
    {
        Console.WriteLine();
        ShowOpenTables(); //Lista använda bord
        Console.WriteLine();
        Console.WriteLine("Vilket bord vill du stänga? Ange bordsnummer:");
        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out int number))
        {
            Console.Write($"Vill du stänga {number}. J/N? ");
            string? input = Console.ReadLine().ToUpper();

            if (input == "J")
            {
                // söker upp bordet efter bordsnummer
                Table tableToOpen = Tables.Find(tables => tables.Number == number);

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
    #endregion
    #region CloseAllTables
    // Metod för att stänga alla bord. Behov vid ex. dagsavslut.
    public static void CloseAllTables()
    {
        ShowTables();
        foreach (Table t in Tables) //Ska man skapa en 
        {
            int number = 0;
            Table tableToClear = Tables.Find(tables => tables.Number == number);
            //Table tableToHandle = tables.Find(tables => tables.Number == number);

            if (t.Status) // checkar om bord är öppna
            {
                // kollar om de finns produkter kvar på bordet
                foreach (Product p in tableToClear.TableOrder.ProductList)
                {
                    if (p != null)
                    {
                        Console.WriteLine(t.Number + "Har produkter kvar! Vill du ändå stänga bordet? J/N?");
                        string? input = Console.ReadLine();
                        if (input == "J")
                        {
                            //TODO voida produkter typ clear()

                        }
                        break;//? continue? return?
                    }
                    t.Status = false;

                }
            }
        }
        Console.WriteLine("Alla bord är stängda.");
        Console.WriteLine();
    }
    #endregion
    #region ShowTables
    // metod för att visa borden (listan)
    public static void ShowTables() // isVisible?
    {

        //testar admin
        bool admin = false;
        //Fyrkantsemoji som får symbolisera ett bord
        string bord = "\u25A0"; // in i Table?
        Console.WriteLine();
        for (int i = 0; i < Tables.Count; i++)
        {
            // checkar om bord är status och markerar rött
            if (!Tables[i].Status)
            {
                if (admin)
                {
                    Console.WriteLine($"Bord: {Tables[i].Number}, {Tables[i].Size}.");
                }
                Console.WriteLine($"Bord: {Tables[i].Number}.");
            }
            else
            {
                //TODO fixa redundans 
                if (admin)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{bord} Bord: {Tables[i].Number}, {Tables[i].Size}.");
                    Console.ResetColor();
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{bord} Bord: {Tables[i].Number}.");
                Console.ResetColor();
            }
        }
        Console.WriteLine();
    }
    #endregion
    #region ShowOpenTables
    // metod för att visa alla öppna bord (även tomma) OBS! Kanske räcker med föregående metod
    public static void ShowOpenTables()
    {

        for (int i = 0; i < Tables.Count; i++)
        {
            string bord = "\u25A0";
            // checkar om bord är status och markerar rött och "bordsemoji"
            if (Tables[i].Status)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{bord} Bord: {Tables[i].Number}.");
                Console.ResetColor();
            }

        }
    }
    #endregion
    #region HandleTableContents
    // metod för att visa varor på bord, ska det läggas till en annan metod för hantering av bordsinnehåll?
    // public void HandleTableContents()// endast send? eller addtotable?
    // {
    //     // if (inga bord har status... tebax)
    //     {
    //         Console.Write("Välj bordsnummer: ");
    //         string? nr = Console.ReadLine();
    //         Console.WriteLine("1. Betalning.");
    //         Console.WriteLine("2. Dela");
    //     }
    // }
    // metod för att visa varor på bord, ska det läggas till en annan metod för hantering av bordsinnehåll?
    public static void HandleTableContents()// endast send? eller addtotable?
    {
        // if (inga bord har status... tebax)
        {
            Console.Write("Välj bordsnummer: ");
            string? nr = Console.ReadLine();

            Console.WriteLine("1. Betalning.");
            Console.WriteLine("2. Dela");
            Console.WriteLine("'Q' för att avbryta."); // oklart läge :)
            string? choice = Console.ReadLine().ToUpper();
            while (true)
            {

                if (choice == "1")
                    if (int.TryParse(nr, out int number))
                    {

                        Table tableToHandle = Tables.Find(tables => tables.Number == number);
                        CurrentTable = number;

                        if (tableToHandle != null && tableToHandle.Status) //// checkar så bordet finns och att det är öppet
                        {

                            // Får bygga en meny för ex. splitta nota osv1
                            foreach (Product p in tableToHandle.TableOrder.ProductList)
                            {
                                Console.WriteLine(p.Name + " " + p.Price);
                            }
                            Console.Write($"Vill du skicka order till betalning? J/N?");
                            //cw skicka vissa articklar till betalnng
                            //cw markera vissa och skicka till betalning eller annat bord?
                            string? input = Console.ReadLine().ToUpper();

                            if (input == "J")
                            {
                                // foreach (Product p in tableToHandle.TableOrder.ProductList)
                                // {
                                //     UserInterFace.orderList.Add(p);
                                // }
                                Payment.StartPayment(tableToHandle.TableOrder, CurrentTable);
                                tableToHandle.Status = false;
                                tableToHandle.TableOrder = null; //.ProductList.Clear();//TODO stäng bordet

                            }
                            else if (input == "N")
                            {
                                Console.WriteLine("Avbruten.");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt val.");
                            }

                            break;
                        }
                        // else if (receipt.AmountToPay <= 0)
                        // {
                        //     Console.WriteLine("Det finns inget att betala. Summan är noll!");
                        //     break;
                        // }
                        else
                        {
                            Console.WriteLine("Finns inga produkter på det bordet!");
                            break;
                        }
                    }
                    else if (choice == "2")
                    {
                        Console.WriteLine("Flytta produkter.");
                        int i = 1;
                        while (true)
                        {
                            List<Product> splitList = new();
                            Table tableToHandle = Tables.Find(tables => tables.Number == number); //vilket bord e detta?
                            foreach (Product p in tableToHandle.TableOrder.ProductList)
                            {
                                Console.WriteLine($"{i} {p.Name}{p.Price}{p.ProductNumber}");
                                i++;
                            }
                            while (true)
                            {
                                Console.WriteLine("Välj produkt som du vill hantera");
                                number = int.Parse(Console.ReadLine());
                                Table temp = Tables.Find(tables => tables.Number == number);

                                if (number == 0)
                                {
                                    break;
                                }
                            }
                        }
                        // splitta jämt per antal

                    }
                    else if (choice == "Q")
                    {
                        return;
                    }
                    else if (choice == "3")
                    {
                        Console.WriteLine("Rensa?");
                    }
                    else
                    {
                        Console.WriteLine("FEL");
                        break;
                    }
            }
        }
    }
    #endregion
    #region SplitTable

    // metod för att splitta bordsnota (ex 3 öl. ska dom till annat/nytt bord eller betalas?)
    public void SplitTable()// Split, splitCheck? 
    {

    }
    #endregion
    #region VoidOrder
    // metod för att radera produkter eller tömma bord. (krävs hov/admin? Ange behörighet alt redan inloggad)
    public void VoidOrder() // oklart namn ta bort varje produkt i listan
    {

    }
    #endregion
    #region OrderToTable
    // metod för att lägga en order till ett bord

    #endregion
    #region AddTable
    // Funktion för ex. hov/admin där man skapar borden till sin restaurang/avdelning (relaterar mest till bokningsfunktioner)
    public static void AddTable()
    {
        bool status = false;
        Console.WriteLine();
        Console.WriteLine("Skapa nytt bord.");
        Console.WriteLine("Ange bordsnr:");
        int number = int.Parse(Console.ReadLine()); //Fixa tryparse

        Console.WriteLine("Lägg till storlek:"); //sittplatser? stolar?
        int size = int.Parse(Console.ReadLine());

        // Adda funktion för att kolla så inte nr finns
        Table newTable = new Table(number, status, size);
        Tables.Add(newTable);

        //getPosition
    }

    // adminfunktion för att ta bort bord från lista
    public static void RemoveTable()
    {

        Console.WriteLine();
        ShowTables(); //Lista använda bord
        Console.WriteLine();
        Console.WriteLine("Vilket bord vill du stänga? Ange bordsnummer:");
        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out int number))
        {
            Console.Write($"Vill du ta bort bord {number}. J/N? ");
            string? input = Console.ReadLine().ToUpper();

            if (input == "J")
            {
                // söker upp bordet efter bordsnummer
                Table tableToRemove = Tables.Find(tables => tables.Number == number);

                if (tableToRemove != null) // checkar så bordet finns
                {
                    //Checkar om status är true
                    Tables.Remove(tableToRemove);

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
    #endregion
    #region EditTable
    // adminfunktion för att redigera bord i lista dvs. ändra storlek
    public static void EditTable()
    {
        Console.WriteLine();
        ShowTables(); //Lista använda bord
        Console.WriteLine();
        Console.WriteLine("Vilket bord vill du redigera? Ange bordsnummer:");
        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out int number))
        {
            Console.Write($"Vill redigera bord {number}. J/N? ");
            string? input = Console.ReadLine().ToUpper();

            if (input == "J")
            {
                // söker upp bordet efter bordsnummer
                Table tableToEdit = Tables.Find(tables => tables.Number == number);

                if (tableToEdit != null) // checkar så bordet finns
                {
                    Console.WriteLine("ange ny storlek.");

                    tableToEdit.Size = int.Parse(Console.ReadLine());
                    Console.WriteLine($"bord: {number}. storlek ändrat till {tableToEdit.Size}.");


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
    #endregion
    #endregion
    #region TrashBin
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
//     public void AddTableToMap(int width, int height, string? name) // DockTable, GetTable? bättre namn asap:) No funk :(
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
#endregion