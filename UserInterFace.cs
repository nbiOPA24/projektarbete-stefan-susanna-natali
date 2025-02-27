
public static class UserInterFace
{

    public static int UserChoice { get; set; }
    public static List<Product> orderList = new List<Product>();


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
    #region PrintOrderList
    public static void PrintOrderlist()
    {
        Dictionary<string, int> productAntal = new Dictionary<string, int>();

        Dictionary<string, int> productAmount = new Dictionary<string, int>();

        // Söker upp alla matchande produkter och räknar
        foreach (Product p in orderList)
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
    public static void Order(Receipt receipt)
    {
        Console.WriteLine();
        Console.WriteLine("Produkter:");
        Console.WriteLine();
        ProductHandler.PrintProduct();
        Console.WriteLine();

        while (true)
        {
            Console.Write("Skriv in beställning, ange artikelnr. 'Q' för klar eller 'A' för avbryt: ");
            string? choice = Console.ReadLine().ToUpper();
            if (choice == "A")
            {
                break;
            }
            if (int.TryParse(choice, out int number))
            {
                Product productsToAdd = ProductHandler.productList.Find(product => product.ProductNumber == number);
                if (productsToAdd != null)
                {
                    orderList.Add(productsToAdd);// orderlista för att skickas till betalning eller bord

                }

                else
                {
                    Console.WriteLine("Ogiltig input");
                }
            }
            PrintOrderlist();

            double totalSum = CountTotal(receipt);   //skicka in och räkna ut summan för bordet, skicka tillbaka till totalSum
            double totalVat = Payment.CalculateVat(receipt, out double vat25, out double vat12); //out retunerar en till variabel
            Console.WriteLine("Summa att betala: " + totalSum);
            receipt.Vat25 = vat25;
            receipt.Vat12 = vat12;
            receipt.AmountToPay = totalSum;

            if (choice == "Q")
            {
                Console.WriteLine("Betala (D)irekt eller lägga order till (B)ord?");
                string? paymentChoice = Console.ReadLine().ToUpper();

                if (paymentChoice == "D")
                {
                    Payment.StartPayment(receipt);
                    orderList.Clear();// för tidigt?
                    break;
                }
                else if (paymentChoice == "B")
                {
                    TableHandler.OrderToTable();
                    orderList.Clear();// oklart
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
    #region CountTotal
    public static double CountTotal(Receipt receipt)// denna räknar ju inte med bordsprodukterna
    {

        double temptotal = 0; //Nollställ efter varje knapptryckning när man lägger på en ny artikel
        if (orderList.Count != 0)
        {
            foreach (Product p in orderList)
            {
                temptotal += p.Price;

            }
        }
        // else //YEEEEEEEEEEEEEEEEEEEESSSSSSSSSSSSSSSSSSSSSS
        // {
        //     foreach (Product p in table.TableList)
        //     {
        //         temptotal += p.Price;
        //     }
        // }
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
    public static void UserInterFaceStartMenu(Receipt receipt) //, User user01
    {
        while (true)
        {
//#if !DEBUG
            //Console.Clear();
//
            Data.LoadUserList("user.json");
            // TableHandler.GenerateTables();
            Console.WriteLine("Välkommen!");
            Console.WriteLine();
            UserHandler.PrintUser();
            Console.WriteLine();
            Console.Write("Välj användare, ange ID-nummer: "); // "AnvändarId:" 
            UserChoice = 2402;//int.Parse(Console.ReadLine()); //Pausad så man slipper logga in
            UserHandler.IsAdmin();
            bool innerMenu = true;
            while (innerMenu)
            {
                Data.LoadUserList("user.json");
                Data.LoadProductList("product.json");
                Data.LoadNextId("nextid.json");
                Data.LoadNextProductNumber("nextproductnumber.json");
                Data.LoadReceiptList("receipt.json");
                Data.LoadNextReceiptNumber("nextreceiptnumber.json");
                Console.WriteLine();
                Console.WriteLine("*****SUNAST-KASSASYSTEM*****");
                Console.WriteLine();
                Console.WriteLine("1. Ny Order");//Ny beställning");
                Console.WriteLine("2. Hantera bordsnotor"); // hämta order på bord
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
                        Order(receipt);
                        Data.SaveNextReceiptNumber("nextreceiptnumber.json");
                        break;
                    case "2":
                        bool aTableIsOpen = TableHandler.ShowOpenTables();
                        if (!aTableIsOpen)
                        {
                            continue;
                        }
                        TableHandler.HandleTableContents(receipt);
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
                        Payment.PrintReceiptList(receipt);
                        break;
                    case "6":
                        ProductHandler.ProductStartMenu();

                        break;
                    case "7":
                        UserHandler.UserStartMenu();
                        break;
                        case "8":
                        ReportHandler.ReportMenu();
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