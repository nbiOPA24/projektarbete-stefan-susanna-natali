public class Order
{
    public double TotalSum { get; set; }
    public double TotalVat { get; set; }
    public static int OrderNumber = 1;
    public List<Product> ProductList { get; set; }
    public User User { get; set; }

    public Order(List<Product> NewOrderList, User user)
    {
        User = user;
        ProductList = NewOrderList;
        OrderNumber++;
    }
}

public static class UserInterFace
{

    public static int UserChoice { get; set; }
    public static List<Order> orderList = new();

    // public static void testmetod()
    // {
    //     //loop för att skriva ut alla ordrar
    //     foreach (Order p in orderList)
    //     {
    //         Console.WriteLine(p.ProductList);
    //     }
    //     //loop för att skriva ut alla produkter i en order
    //     foreach (Product p in orderList[2].ProductList)
    //     {
    //         Console.WriteLine(p.Price + p.Name);
    //     }
    //     orderList[2].ProductList

    // }
    #region PrintOrderLis
    public static void PrintOrderlist(Order order)
    {

        Dictionary<string, int> productAmount = new Dictionary<string, int>();

        // Söker upp alla matchande produkter och räknar
        foreach (Product p in order.ProductList)
        {
            if (productAmount.ContainsKey(p.Name)) // kollar matchande p.Name
            {
                productAmount[p.Name]++; // Räknar antal träffar av samma name
            }
            else
            {
                productAmount[p.Name] = 1; // om bara en träff så = 1
            }
        }
        foreach (var p in productAmount)
        {
            Console.WriteLine($"{p.Value} st {p.Key}");
        }
    }
    #endregion
    #region Order
    // Ev. TODO ev. välja nollbong eller funktion för detta? 
    public static void Order(User user)
    {
        Console.WriteLine();
        ProductHandler.PrintProduct();
        Console.WriteLine();
        List<Product> temporaryProductList = new();

        while (true)
        {
            Console.Write("Skriv in beställning, ange artikelnr. 'Q' för klar: ");
            string? choice = Console.ReadLine().ToUpper();

            if (int.TryParse(choice, out int number))
            {
                Product productsToAdd = ProductHandler.productList.Find(product => product.ProductNumber == number);
                if (productsToAdd != null)
                {
                    temporaryProductList.Add(productsToAdd);// orderlista för att skickas till betalning eller bord
                }

                else
                {
                    Console.WriteLine("Ogiltig input");
                }
            }
            // PrintOrderlist();
            Order newOrder = new(temporaryProductList, user);
            double totalSum = CountTotal(newOrder);   //skicka in och räkna ut summan för bordet, skicka tillbaka till totalSum
            Payment.CalculateVat(newOrder, out double vat12, out double vat25); //out retunerar en variabel
            Console.WriteLine("Summa att betala: " + totalSum);
            newOrder.TotalSum = totalSum;
            newOrder.TotalVat = vat12 + vat25;
            PrintOrderlist(newOrder);
            if (choice == "Q")
            {
                Console.WriteLine("Betala (D)irekt eller lägga order till (B)ord?");
                string? paymentChoice = Console.ReadLine().ToUpper();

                if (paymentChoice == "D")
                {
                    Payment.StartPayment(newOrder, null);//table, receipt);
                    break;
                }
                else if (paymentChoice == "B")
                {
                    TableHandler.ShowTables();// ska detta va här eller en ny metod i TableHandler?
                    OrderToTable(newOrder);
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltig input");

                }
            }
        }
    }
    #endregion
    #region OrderToTable
    public static void OrderToTable(Order order)
    {
        // fixa felhantering med en loop och lite breaks så att den startar om på rätt plats.
        Console.Write("välj bordsnummer: "); //Börja om här tills rätt bordsnummer eller q
        string? nr = Console.ReadLine();

        if (int.TryParse(nr, out int number))
        {
            if (number > TableHandler.Tables.Count)
            {
                Console.WriteLine("finns inte inom range");
            }
        }

        Console.Write($"Vill lägga order på bord {number}. J/N?: ");
        Console.WriteLine();
        string? input = Console.ReadLine().ToUpper();
        Table tableToAddOrder = TableHandler.Tables.Find(tables => tables.Number == number);
        Table table = new(5,true,4);
        tableToAddOrder = table;
        if (input == "J")
        {

            if (tableToAddOrder.Status == false || tableToAddOrder.Status == true)
            {
                tableToAddOrder.Status = true;

                foreach (Product p in order.ProductList)// här läggs ordern till bordet
                {
                    Product product = new("Pizza", 140, Product.ProductType.Food);

                    tableToAddOrder.TableOrder.ProductList.Add(p);
                    tableToAddOrder.TableOrder.ProductList.Add(product);

                }
            }
        }
        else
        {
            Console.WriteLine("Ogiltigt bordsnummer! Försök igen");
            OrderToTable(order);
        }
        // skapar en Dict med stringKey och intKey
        Dictionary<string, int> productAntal = new Dictionary<string, int>();

        // Söker upp alla matchande produkter och räknar
        foreach (Product p in tableToAddOrder.TableOrder.ProductList)
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
        foreach (var p in productAntal)
        {
            Console.WriteLine($"{p.Value} st {p.Key}");
        }
        Console.WriteLine();
        Console.WriteLine("Order skickas till köksprinter.");//TODO (bara mat till köket.)
        Console.WriteLine();

        if (input == "N")
        {
            Console.WriteLine("Avbruten.");
        }
        else
        {
            Console.WriteLine("Ogiltigt val.");
        }
    }

    #endregion
    #region CountTotal
    public static double CountTotal(Order order)// denna räknar ju inte med bordsprodukterna
    {

        double temptotal = 0; //Nollställ efter varje knapptryckning när man lägger på en ny artikel
        if (orderList.Count != 0)
        {
            foreach (Product p in order.ProductList)
            {
                temptotal += p.Price;

            }
        }
        else //YEEEEEEEEEEEEEEEEEEEESSSSSSSSSSSSSSSSSSSSSS
        {
            foreach (Product p in order.ProductList)
            {
                temptotal += p.Price;
            }
        }
        return temptotal;


    }
    #endregion
    #region CreateDescr
    public static void CreateMenuDescription() // OM en produkt inte innehåller en beskriving, fyll i beskrivning
    {
        // TODO: Console.WriteLine("Vill du fylla i alla tomma beskrivningar eller välja från en lista? ");
        int descriptionsToFill = 0;
        foreach (Product p in ProductHandler.productList)
        {
            if (p.Description == null)
                descriptionsToFill++;
        }

        for (int i = 0; i < ProductHandler.productList.Count; i++)
        {
            if (ProductHandler.productList[i].Description == null)
            {
                Console.Write("Q för tillbaka. Beskriv produkten " + ProductHandler.productList[i].Name + ": ");
                string answer = Console.ReadLine();
                answer = UppercaseFirst(answer);
                if (Back(answer))
                {
                    break;
                }
                ProductHandler.productList[i].Description += answer;
                descriptionsToFill--;
                if (descriptionsToFill == 0)
                {
                    Console.WriteLine("Meny komplett!");
                    break;
                }
                while (true)
                {
                    Console.Write("Vill du fortsätta? Du har " + descriptionsToFill + " kvar att fylla i. j/n: ");
                    string input = Console.ReadLine().ToUpper();
                    if (input == "J")
                    {
                        break;
                    }
                    else if (input == "N")
                    {

                        return;
                    }
                    else
                    {
                        Console.WriteLine("Ogiltig input!");

                    }
                }
            }
        }
    }
    #endregion
    #region EditDescript
    public static void EditMenuDescription()
    {
        ProductHandler.PrintProduct();
        Console.Write("Q För tillbaka. Vilken artikel ska ändras? Ange siffra: ");
        string? answer = Console.ReadLine();
        if (Back(answer))
        {
            return;
        }
        if (int.TryParse(answer, out int number))
            for (int i = 0; i < ProductHandler.productList.Count; i++)
            {
                if (number - 1 == i)
                {
                    Console.Write("Skriv in ny beskrivning: ");
                    string newDescription = Console.ReadLine();
                    ProductHandler.productList[i].Description = newDescription;
                }
            }

    }
    #endregion
    #region DisplayMenu
    public static void DisplayMenu()
    {

        Console.WriteLine("\t\tRestaurangmeny\n\n");
        foreach (Product food in ProductHandler.productList)
        {
            if (food.MenuItem == Product.ProductType.Food)
            {
                Console.WriteLine("\t{0, -20} {1, -15} {2}", food.Name, food.Price + "kr ", food.Description);
                continue;
            }
        }


        Console.WriteLine("\n\n\t\tBarmeny\n\n");
        foreach (Product drink in ProductHandler.productList)
        {
            if (drink.MenuItem == Product.ProductType.Alcohol || drink.MenuItem == Product.ProductType.Beverage)
            {
                Console.WriteLine("\t{0, -20} {1, -15} {2}", drink.Name, drink.Price + "kr ", drink.Description);
            }
        }

    }
    #endregion
    #region StartMenu
    public static void UserInterFaceStartMenu(User user01)
    {
        while (true)
        {
#if !DEBUG
            Console.Clear();
#endif
            Data.LoadUserList("user.json");
            //TableHandler.GenerateTables();
            Console.WriteLine("Välkommen!");
            UserHandler.PrintUser();
            Console.Write("Välj användare, ange Användar-ID: "); //Todo, flytta denna till main så man kan använda den instansen ist!
            UserChoice = 2402;//int.Parse(Console.ReadLine()); //Pausad så man slipper logga in
            UserHandler.IsAdmin();
            bool innerMenu = true;
            while (innerMenu)

            {
                Data.LoadUserList("user.json");
                Data.LoadProductList("product.json");
                Data.LoadNextId("nextid.id");
                Data.LoadNextProductNumber("nextproductnumber.id");
                Data.LoadReceiptList("receipt.json");
                Data.LoadNextReceiptNumber("nextreceiptnumber.id");
                Console.WriteLine("*****SUNAST-KASSASYSTEM*****");
                Console.WriteLine("1. Ny Order");//Ny beställning");
                Console.WriteLine("2. Hantera Order"); // hämta order på bord
                Console.WriteLine("3. Restaurangmeny");
                Console.WriteLine("4. Bordshantering");
                Console.WriteLine("5. Skriv ut alla kvitton");

                if (User.Admin)
                {
                    Console.WriteLine("6. Produktmeny");
                    Console.WriteLine("7. Personalmeny");
                    Console.WriteLine("8. Rapporter");

                }
                Console.WriteLine("(L)ogga ut");

                string? choice = Console.ReadLine().ToUpper();
                switch (choice)
                {
                    case "1":
                        Order(user01);
                        Data.SaveNextReceiptNumber("nextreceiptnumber.json");
                        break;
                    case "2":
                        TableHandler.ShowOpenTables();
                        TableHandler.HandleTableContents();
                        Data.SaveNextReceiptNumber("nextreceiptnumber.json");

                        break;
                    case "3":
                        DisplayMenu();
                        if (User.Admin)
                        {
                            Console.WriteLine("1. Skapa meny");
                            Console.WriteLine("2. Ändra meny");
                            Console.Write("Q. för att avsluta: ");
                            string? menuchoice = Console.ReadLine().ToUpper();
                            switch (menuchoice)
                            {
                                case "1":
                                    CreateMenuDescription();
                                    break;
                                case "2":
                                    EditMenuDescription();
                                    break;
                                case "Q":
                                    break;
                                default:
                                    Console.WriteLine("Ogiltig input!");
                                    break;
                            }
                        }
                        break;
                    case "4":
                        TableHandler.TableMenu();
                        break;
                    case "5":
                        Receipt receipt = new();
                        Payment.PrintReceiptList(receipt);
                        break;
                    case "6":
                        ProductHandler.ProductStartMenu();

                        break;
                    case "7":
                        UserHandler.UserStartMenu(user01);
                        break;
                    case "8":
                        // ReportHandler.ReportMenu();
                        break;

                    case "L":
                        Back(choice);
                        innerMenu = false;
                        break;
                    default:
                        Console.WriteLine("Ogiltig input!");
                        break;
                }
            }
        }
    }
    #endregion
    #region GetName
    public static string GetName()
    {
        Console.Write("Ange namn: ");
        string? input = Console.ReadLine();
        return input;
    }
    #endregion
    #region Back
    public static bool Back(string input)
    {
        if (input == "Q")
        {
            Console.WriteLine("Tas tillbaka");
            return true;
        }

        return false;

    }
    #endregion
    #region UpperCase
    public static string UppercaseFirst(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        return char.ToUpper(str[0]) + str.Substring(1).ToLower();
    }

}
#endregion