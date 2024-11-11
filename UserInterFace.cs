public static class UserInterFace
{
    //TODO kolla status vid bordet, tex de vill ha något mer att dricka, dessert, de är nöjda/missnöjda kompensera? 
    //Sale: Type
    public static int UserChoice { get; set; }
    public static double PaidAmount { get; set; }
    public static double Tips { get; set; }
    public static double AmountToPay { get; set; }
    public static double Vat12 { get; set; }
    public static double Vat25 { get; set; }
    public static bool IsCash { get; set; }
    public static DateTime PaymentAccepted { get; set; }
    public static List<Product> orderList = new();

    public static void PrintOrderlist()
    {

        foreach (Product p in orderList) //TODO plussa på istället för att varje artikel hamnar på en egen rad, tex, 3 öl, 4 pizzor
        {
            // if (p.Quantity > 0)
            Console.WriteLine(p.ProductNumber + ". " + p.Name + " - " + p.Price + " kr. "); //  + p.Quantity + " st"

        }

    }

    // Ev. TODO ev. välja nollbong eller funktion för detta? 
    public static void Order(bool status, Product product, TableHandler tablehandler) //TODO kan den plussa istället för att lägga samma artikel på ny rad? 
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
            CountTotal();

            if (choice == "Q")
            {
                Console.WriteLine("Betala (d)irekt eller lägga order till (b)ord?");
                string paymentChoice = Console.ReadLine();
                paymentChoice = UppercaseFirst(paymentChoice);
                if (paymentChoice == "D")
                {
                    Payment();
                    orderList.Clear();
                    //TODO lägg i rapport-lista
                    break;
                }
                else if (paymentChoice == "B")
                {

                    //TableHandler tableHandler = new();
                    TableHandler.ShowTables();
                    TableHandler.OrderToTable(number, status, product, tableHandler, tablehandler);
                    //TODO lägg i rapport-lista
                    orderList.Clear();
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltig input");

                }

            }

        }
    }

    public static void CountTotal()
    {
        TableHandler tableHandler = new();
        AmountToPay = 0; //Nollställ efter varje knapptryckning när man lägger på en ny artikel
        foreach (Product p in tableHandler.tableProductList)
        {
            AmountToPay += p.Price; //p.Quantity *

        }
        Console.WriteLine("Summa att betala: " + AmountToPay);


    }
    public static void Payment()
    {

        if (AmountToPay > 0)
        {
            Console.WriteLine("*******BETALNING********");
            Console.Write("1. Kort eller 2. kontant?: ");
            int input = int.Parse(Console.ReadLine());
            CountTotal();
            switch (input)
            {

                case 1: // KORT
                    IsCash = false;
                    GetPayment();
                    break;


                case 2: // KONTANT
                    IsCash = true;
                    GetPayment();
                    break;



            }
        }
        else
        {
            Console.WriteLine("Finns inga produkter att ta betalt för!");
        }
    }
    public static void GetPayment()
    {
        TableHandler tableHandler = new();

        while (true)
        {
            Console.Write("Slå in totalbelopp ink. ev. dricks: ");
            PaidAmount = int.Parse(Console.ReadLine());
            Tips = PaidAmount - AmountToPay;

            if (PaidAmount < AmountToPay)
            {
                Console.WriteLine("Beloppet är för lågt! Summa att betala är " + AmountToPay);
                continue;
            }
            if (IsCash == true)
            {
                Console.Write("Slå in mottagna pengar: ");
                double givenMoney = int.Parse(Console.ReadLine());
                double change = givenMoney - PaidAmount;
                Console.WriteLine("Du dricksade " + Tips + " kr.");
                Console.WriteLine("Din växel är " + change + " kr.");
                Console.WriteLine("Tack!");
                Thread.Sleep(1000);
                PrintReceipt();
                break;
            }
            else if (PaidAmount >= AmountToPay)
            {
                Console.WriteLine("Du dricksade " + Tips + " kr.");
                Console.WriteLine("Betalning genomförs");
                // Thread.Sleep(1000);
                // Console.Write(".");
                // Thread.Sleep(1000);
                // Console.Write(".");
                // Thread.Sleep(1000);
                // Console.Write(".");
                // Thread.Sleep(1000);
                // Console.WriteLine(" Tack!");
                // Thread.Sleep(1000);
                PrintReceipt(); //TODO indata kvittonummer för att hålla reda på? 


                //Stäng bordet och töm bordslistan
                // foreach(Product p in tableHandler.tableProductList)
                // {
                //orderList.Clear();

                break;
            }
            else
            {
                Console.WriteLine("Ogilig inmatning!");
            }
        }
    }
    public static void SplitPayment() // vänta med denna
    {
        // Splitta broderligt
        // Splitta per person

    }
    public static void PrintReceipt() //TODO kvittokopia, kolla riktigt kvitto å se va som ska va mäd! Kuna spara kvitto. söka upp gamla kvitton.
    {
        PaymentAccepted = DateTime.Now;
        int ReceiptNumber = 1000; //Todo gör till egenskap

        Console.WriteLine("\t\tRestaurangNamn");
        Console.WriteLine("\t\tKvittonummer: " + ReceiptNumber);
        //if (blabla tablenr != null)
        Console.WriteLine("Bordsnummer: #");
        User currentUser = UserHandler.userList.Find(user => user.UserId == UserChoice);
        Console.WriteLine("Användare: " + currentUser.FirstName + " - " + currentUser.UserId); //Vilken Användare/servis
        Console.WriteLine("Datum: " + PaymentAccepted); //TODO DateTime från när betalningen gått igenom
        Console.WriteLine("Beställda artiklar: ");
        PrintOrderlist();
        Console.WriteLine("------------------------");
        Console.WriteLine("Betald summa: " + Math.Round(PaidAmount, 3));//Betald summa
        Console.WriteLine("Varav dricks: " + Math.Round(Tips, 3));//Varav dricks(Extra)
        CalculateVat();
        double Netto = AmountToPay - Vat12 - Vat25;
        Console.WriteLine("Netto: " + Math.Round(Netto, 2));
        Console.WriteLine("Varav moms 12%: " + Math.Round(Vat12, 3));//Varav moms
        Console.WriteLine("Varav moms 25%: " + Math.Round(Vat25, 3));
        //TODO lägg in allt detta i en rapportlista
        // placeholder kortuppgifter
    }

    public static void CalculateVat()
    {
        double CalcVat12 = 0;
        double CalcVat25 = 0;
        foreach (Product p in orderList)
        {
            if (p.MenuItem == Product.ProductType.Food || p.MenuItem == Product.ProductType.Beverage)
            {

                CalcVat12 += p.Price;
                Vat12 = CalcVat12 * 0.12;

            }

            else if (p.MenuItem == Product.ProductType.Alcohol)
            {
                CalcVat25 += p.Price;
                Vat25 = CalcVat25 * 0.25;
            }

        }


    }
    // public static void AddOrderToTable() { } Ska vara i tablehandler
    // public static void OpenTable() { } ska vara i tablehandler
    // public static void SendOrder() { } ska vara i tablehandler
    // public static void CancelOrder() { } ska vara i tablehandler
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
    public static void UserInterFaceStartMenu(Product product, TableHandler tableHandler, User user, int number, bool status, int size)
    {
        // Console.WriteLine("Välkommen!");
        // UserHandler.PrintUser(user);
        // Console.Write("Välj användare, ange ID-nummer: ");
        TableHandler.GenerateTables();
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
                    Order(status, product);
                    break;
                case "2":
                    TableHandler.ShowOpenTables();
                    tableHandler.HandleTableContents(number, tableHandler);
                    break;
                case "3":
                    DisplayMenu();
                    //if(admin)
                    Console.WriteLine("1. Skapa meny");
                    Console.WriteLine("2. Ändra meny");
                    Console.Write("Q för att avsluta: ");
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
                    TableHandler.TableMenu(number, status, size, product);
                    break;
                case "5":
                    ProductHandler.ProductStartMenu();
                    break;
                case "6":
                    UserHandler.UserStartMenu(user);
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
