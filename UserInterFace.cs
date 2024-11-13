public static class UserInterFace
{

    public static int UserChoice { get; set; }
    public static List<Product> orderList = new();

    public static void PrintOrderlist()
    {

        foreach (Product p in orderList)
        {

            Console.WriteLine(p.ProductNumber + ". " + p.Name + " - " + p.Price + " kr. ");

        }

    }

    // Ev. TODO ev. välja nollbong eller funktion för detta? 
    public static void Order(Table table)
    {
        Console.WriteLine();
        ProductHandler.PrintProduct();
        Console.WriteLine();

        while (true)
        {
            Console.Write("Skriv in beställning, ange artikelnr. 'Q' för klar: ");
            string? choice = Console.ReadLine().ToUpper();

            if (int.TryParse(choice, out int number))
            {
                Product productsToAdd = ProductHandler.productList.Find(product => product.ProductNumber == number);
                if (productsToAdd != null)
                {
                    orderList.Add(productsToAdd);
                }

                else
                {
                    Console.WriteLine("Ogiltig input");
                }
            }
            PrintOrderlist();
            CountTotal(table);

            if (choice == "Q")
            {
                Console.WriteLine("Betala (D)irekt eller lägga order till (B)ord?");
                string? paymentChoice = Console.ReadLine().ToUpper();

                if (paymentChoice == "D")
                {
                    Payment.StartPayment(table);
                    orderList.Clear();// för tidigt?
                    //TODO lägg i rapport-lista
                    break;
                }
                else if (paymentChoice == "B")
                {

                    //TableHandler.ShowTables();
                    //TableHandler tableHandler = new();
                    //TableHandler.ShowTables();

                    TableHandler.OrderToTable(number);
                    //TODO lägg i rapport-lista
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

    public static void CountTotal(Table table)// denna räknar ju inte med bordsprodukterna
    {

        Payment.AmountToPay = 0; //Nollställ efter varje knapptryckning när man lägger på en ny artikel
        if (orderList.Count != 0)
        {
            foreach (Product p in orderList)
            {
                Payment.AmountToPay += p.Price; //p.Quantity *

            }
        }
        else //YEEEEEEEEEEEEEEEEEEEESSSSSSSSSSSSSSSSSSSSSS
        {
            foreach (Product p in table.TableList)
            {
                Payment.AmountToPay += p.Price;
            }
        }

        Console.WriteLine("Summa att betala: " + Payment.AmountToPay); 


    }

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
    public static void UserInterFaceStartMenu(TableHandler tableHandler, int number, bool status, int size, Table table)
    {
        // Console.WriteLine("Välkommen!");
        // UserHandler.PrintUser(user);
        // Console.Write("Välj användare, ange ID-nummer: ");

        UserChoice = 2401;//int.Parse(Console.ReadLine());
        while (true)
        {
            Console.WriteLine("*****SUNAST-KASSASYSTEM*****");
            Console.WriteLine("1. Ny Order");//Ny beställning");
            Console.WriteLine("2. Hantera Order"); // hämta order på bord
            Console.WriteLine("3. Se restaurangmeny");
            Console.WriteLine("4. Bordshantering");
            Console.WriteLine("5. Produktmeny");
            Console.WriteLine("6. Personalmeny");

            string? choice = Console.ReadLine().ToUpper();
            switch (choice)
            {
                case "1":
                    Order(table);
                    break;
                case "2":
                    TableHandler.ShowOpenTables();
                    tableHandler.HandleTableContents();
                    break;
                case "3":
                    DisplayMenu();
                    //if(admin)
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
                            return;
                    }
                    break;
                case "4":
                    TableHandler.TableMenu(number, status, size);
                    break;
                case "5":
                    ProductHandler.ProductStartMenu();
                    break;
                case "6":
                    UserHandler.UserStartMenu();
                    break;
                case "7":
                    break;
                case "Q":
                    Back(choice);
                    break;
            }
        }
    }
   
    public static bool Back(string input)
    {
        if (input == "Q")
        {
            Console.WriteLine("Tas tillbaka");
            return true;
        }

        return false;

    }

    public static string UppercaseFirst(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        return char.ToUpper(str[0]) + str.Substring(1).ToLower();
    }

}
