// Klass för ett bord med egenskaper Obs! cw på svenska för test. 

#region Table

using Microsoft.VisualBasic;

public class Table
{
    // Bordsspecifik lista för produkter
    public List<Product> TableList { get; set; } //= new();
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
        TableList = new List<Product>(); //TODO ska den ligga här? JA!
    }
    //public Table() { }
}

#endregion
#region TableHandler
// Bordslistan och metoder för att hantera bord och ordrar med produkter
public class TableHandler
{
    public static int CurrentTable { get; set; }
    // lista med bord
    public static List<Table> tables { get; set; } = new();

    public static List<Product> voidOrders { get; set; } = new(); // Borde nog ligga i report alt order

    public TableHandler()
    {
        tables = new List<Table>();

        voidOrders = new List<Product>();
    }

    #region TableMenu
    public static void TableMenu()
    {

        while (true)
        {

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
            string? choice = Console.ReadLine()?.ToUpper();

            if (choice == "1")
            {
                ShowTables();
            }
            else if (choice == "2")
            {
                ShowOpenTables(); // Oklart behov :)
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
    // public static void GenerateTables()
    // {
    //     for (int i = 0; i < 5; i++)
    //     {
    //         bool status = false;
    //         int number = i + 1;
    //         int size = 4;

    //         Table newTable = new Table(number, status, size);
    //         tables.Add(newTable);
    //     }
    // }
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
            string? input = Console.ReadLine()?.ToUpper();

            if (input == "J")
            {
                // Söker upp bord efter dess number
                Table tableToOpen = tables.Find(tables => tables.Number == number);

                // Checkar Så bordet finns
                if (tableToOpen != null || tableToOpen.Status) // kolla så den inte redan är öppen
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
        bool aTableIsOpen = ShowOpenTables();
        if (!aTableIsOpen)
        {
            return;
        }

        Console.WriteLine();
        Console.WriteLine("Vilket bord vill du stänga? Ange bordsnummer:");
        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out int number))
        {
            Console.Write($"Vill du stänga {number}. J/N? "); //måste kolla om de finns varor.
            string? input = Console.ReadLine()?.ToUpper();

            if (input == "J")
            {
                // söker upp bordet efter bordsnummer
                Table tableToOpen = tables.Find(tables => tables.Number == number);

                if (tableToOpen != null) // checkar så bordet finns
                {

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
        Console.WriteLine();
        //ShowOpenTables();
        foreach (Table t in tables) //Ska man skapa en 
        {

            if (t.Status) // checkar om bord är öppna
            {
                Console.WriteLine(t.Number);
                Table tableToClear = tables.Find(tables => tables.Status == t.Status);
                Console.Write($"Bord nummer [{t.Number}] Har ohanterade produkter. Vill du ändå stänga bordet? J/N?");
                string? input = Console.ReadLine()?.ToUpper();
                foreach (Product p in tableToClear.TableList.ToList())
                {
                    // kollar om de finns produkter kvar på bordet
                    if (p != null)
                    {
                        if (input == "J")
                        {

                            voidOrders.Add(p);

                            Console.WriteLine($"{p.Name} Produkter borttagna.");
                        }
                        else if (input == "N")
                        {
                            break;//? continue? return?
                        }
                        else
                        {
                            Console.WriteLine("Felaktigt val!");
                        }
                        tableToClear.TableList.Remove(p);
                    }
                }
                t.Status = false;
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
                Console.WriteLine($"{bord} Bord: {tables[i].Number}.");
                Console.ResetColor();
            }
        }
        Console.WriteLine();
    }
    #endregion
    #region ShowOpenTables
    // metod för att visa alla öppna bord (även tomma) OBS! Kanske räcker med föregående metod
    public static bool ShowOpenTables()
    {
        bool aTableIsOpen = false;
        Console.WriteLine();
        for (int i = 0; i < tables.Count; i++)
        {
            string bord = "\u25A0";
            // checkar om bord är status och markerar rött och "bordsemoji"
            if (tables[i].Status)
            {
                aTableIsOpen = true;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{bord} Bord: {tables[i].Number}.");
                Console.ResetColor();
            }

        }
        Console.WriteLine();
        // checkar om de finns ngt bord öppet och returnerar en bool
        if (!aTableIsOpen)
            Console.WriteLine("Finns inga öppna bord!");
        Console.WriteLine();
        return aTableIsOpen;
    }
    #endregion
    #region HandleTableContents        
    // metod för att visa varor på bord, ska det läggas till en annan metod för hantering av bordsinnehåll?
    public static void HandleTableContents(Receipt receipt)// endast send? eller addtotable?
    {
        Console.Write("Välj bord att hantera: ");
        string? nr = Console.ReadLine();

        while (true)
        {
            if (int.TryParse(nr, out int number))
            {
                if (number > tables.Count)
                {
                    Console.WriteLine("Felaktigt bordsnummer.");
                    return;
                }
                Table tableToHandle = tables.Find(tables => tables.Number == number);
                if (tableToHandle.TableList.Count <= 0) //// checkar så bordet finns och att det är öppet
                {
                    Console.WriteLine("Tomt bord.");
                    break;
                }
                foreach (Product p in tableToHandle.TableList)
                {
                    Console.WriteLine(p.Name + " " + p.Price);
                }
                Console.WriteLine();
                Console.WriteLine("Ange val:");
                Console.WriteLine("1. Betalning.");
                Console.WriteLine("2. Dela");
                //Console.WriteLine("3. Rensa bord?");
                Console.Write("'Q' för att avbryta: "); // oklart läge :)
                string? choice = Console.ReadLine().ToUpper();
                Console.WriteLine();

                if (choice == "Q")
                {
                    Console.WriteLine("avslutar...");
                    return;
                }
                else if (choice == "1")
                {
                    receipt.CurrentTable = number;

                    Console.WriteLine("------------------------");
                    // Får bygga en meny för ex. splitta nota osv1
                    foreach (Product p in tableToHandle.TableList)
                    {
                        Console.WriteLine(p.Name + " " + p.Price);
                    }
                    Console.WriteLine("------------------------");
                    Console.Write($"Vill du skicka order till betalning? J/N?");
                    string? input = Console.ReadLine().ToUpper();

                    if (input == "J")
                    {
                        foreach (Product p in tableToHandle.TableList)
                        {
                            UserInterFace.orderList.Add(p);
                        }
                        Payment.StartPayment(receipt);
                        tableToHandle.Status = false;
                        tableToHandle.TableList.Clear();//TODO stäng bordet
                        break;
                    }
                    else if (input == "N")
                    {
                        Console.WriteLine("Avbruten.");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Ogiltigt val.");
                        continue;
                    }
                }


                else if (choice == "2")
                {
                    SplitTable(receipt, number);
                    return;
                }

                else
                {
                    Console.WriteLine("Finns inga produkter på det bordet!");
                    break;
                }
                //}
            }
        }
    }
    #endregion
    #region SplitTable

    // metod för att splitta bordsnota (ex 3 öl. ska dom till annat/nytt bord eller betalas?)
    public static void SplitTable(Receipt receipt, int number)// Split, splitCheck? 
    {

        Console.WriteLine("Produkter att hantera:");
        Console.WriteLine();
        // while (start) // ngn do while number inte = =?
        // {
        List<Product> splitList = new(); //använda orderlist från Uinterface?
        List<Product> tempSplitList = new(); //tillfällig "varukorg"

        Table tableToHandle = tables.Find(tables => tables.Number == number); //vilket bord e detta?
        foreach (Product p in tableToHandle.TableList)
        {
            Console.WriteLine($"{p.ProductNumber}. {p.Name} {p.Price}"); //dictionary?                 
            tempSplitList.Add(p);
        }
        Console.WriteLine();

        while (true)
        {
            Console.WriteLine("Ange produktnummer för artikel som du vill hantera."); // måste ta remove oxå
            Console.Write("'Q' för klar och 'A' för avbryt. ");
            string? input = Console.ReadLine().ToUpper();
            Console.WriteLine();

            if (int.TryParse(input, out number))
            {
                receipt.CurrentTable = number;
                Product productToAdd = tempSplitList.Find(product => product.ProductNumber == number);
                if (productToAdd != null)
                {
                    splitList.Add(productToAdd);
                    tempSplitList.Remove(productToAdd); // ska detta ske senare så man kan aborta eller ändra sig om man gör fel?  

                    Console.WriteLine("Valda produkter:");
                    foreach (Product p in splitList)
                    {
                        Console.WriteLine($"{p.ProductNumber}. {p.Name} {p.Price}"); //dictionary? 
                    }
                    if (tempSplitList.Count > 0)
                    {
                        Console.WriteLine("Produkter kvar att hantera:");
                        foreach (Product p in tempSplitList)
                        {

                            Console.WriteLine($"{p.ProductNumber}. {p.Name} {p.Price}"); //dictionary? 
                        }
                        Console.WriteLine();
                    }

                    //kunna ändra sig å skicka tillbaka saker
                    else if (tempSplitList.Count <= 0)
                    {
                        Console.WriteLine("Inga produkter kvar att hantera");
                        Console.WriteLine();
                        break;
                    }
                }
            else if (input == "Q")
                {
                    break;
                }
            else if (input == "A")
                {
                    Console.WriteLine("Hantering avbruten.");
                    return;
                }
            else
                {
                    Console.WriteLine("Ogiltigt val.");
                    //continue;
                }
            }

        }
        Console.WriteLine();
        Console.WriteLine("Ange:");
        Console.WriteLine("1. Till betalning.");
        Console.WriteLine("2. Flytta produkter till annat bord.");
        Console.WriteLine("Q. För att avsluta hantering.");
        string? val = Console.ReadLine().ToUpper();

        switch (val)
        {
            case "1":
                Console.WriteLine("Produkter skickas till betalning.");
                foreach (Product p in splitList)
                {
                    UserInterFace.orderList.Add(p);
                    tableToHandle.TableList.Remove(p); //status
                }
                receipt.AmountToPay = UserInterFace.CountTotal(receipt);
                Payment.StartPayment(receipt); // varför måste jag skicka med table to handle?
                UserInterFace.UserInterFaceStartMenu(receipt);
                splitList.Clear();
                return;

            case "2":
                foreach (Product p in splitList)
                {
                    UserInterFace.orderList.Add(p);
                    tableToHandle.TableList.Remove(p); //status
                }
                splitList.Clear();
                //receipt.AmountToPay = UserInterFace.CountTotal(receipt); dubblerar?
                OrderToTable();
                if (tableToHandle.TableList.Count <= 0)
                {
                    tableToHandle.Status = false;
                }
                return;

            case "Q":
                Console.WriteLine("");
                break;
        }
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
    public static void OrderToTable()
    {

        ShowTables();// ska detta va här eller en ny metod i TableHandler?
        Console.Write("välj bordsnummer: "); //Börja om här tills rätt bordsnummer eller q
        string? nr = Console.ReadLine();
        while (true)
        {
            if (int.TryParse(nr, out int number))
            {
                if (number > tables.Count)
                {
                    Console.WriteLine("finns inte inom range");
                    break;
                }

                // söker upp bordet efter bordsnummer
                Table tableToAddOrder = tables.Find(tables => tables.Number == number);

                Console.Write($"Vill lägga order på bord {number}. J/N? ");
                string? input = Console.ReadLine().ToUpper();

                if (input == "J")
                {
                    // checkar om de finns grejjer på bordet
                    if (tableToAddOrder.TableList.Count > 0 || tableToAddOrder.Status == true)
                    {
                        Console.WriteLine("Det finns redan produkter på bordet. Vill du addera din order till dessa? J/N?");
                        string? choice = Console.ReadLine()?.ToUpper();
                        while (true)
                        {
                            if (choice == "J")
                            {
                                foreach (Product p in UserInterFace.orderList)
                                {
                                    tableToAddOrder.TableList.Add(p);
                                }
                                UserInterFace.orderList.Clear();
                                Console.WriteLine("Dina produkter har lagts till på bordet");
                                return;
                            }
                            else if (choice == "N")
                            {
                                Console.WriteLine("Åtgärd avbruten.");
                                return; // här vill man nog tillbaks till
                            }
                            else
                            {
                                Console.WriteLine("Ogiltigt val.");

                            }
                        }
                    }
                    else if (tableToAddOrder.Status == false)
                    {
                        tableToAddOrder.Status = true;

                        foreach (Product p in UserInterFace.orderList)// här läggs ordern till bordet
                        {
                            tableToAddOrder.TableList.Add(p);
                        }
                        UserInterFace.orderList.Clear();

                        Dictionary<string, int> productAntal = new Dictionary<string, int>();

                        // Söker upp alla matchande produkter och räknar
                        foreach (Product p in tableToAddOrder.TableList)
                        {
                            if (productAntal.ContainsKey(p.Name)) // kollar matchande p.Name
                            {
                                productAntal[p.Name]++; // Räknar antal träffar av samma name
                            }
                            else
                            {
                                productAntal[p.Name] = 1; // om bara en träff så = 1
                            }
                        }
                        Console.WriteLine();
                        foreach (var p in productAntal)
                        {
                            Console.WriteLine($"{p.Value} st {p.Key}");
                        }
                        break;
                    }
                    // Console.ReadKey();
                    // Console.Clear();
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
            }
        }
    }
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
        tables.Add(newTable);

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
                Table tableToEdit = tables.Find(tables => tables.Number == number);

                if (tableToEdit != null) // checkar så bordet finns
                {
                    Console.WriteLine("ange ny storlek.");

                    tableToEdit.Size = int.Parse(Console.ReadLine());

                    Console.WriteLine($"bord: {number}. storlek ändrat till {tableToEdit.Size}."); //TODO: kommer detta fungera? 

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