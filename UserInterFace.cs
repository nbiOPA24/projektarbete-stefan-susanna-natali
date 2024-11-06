public static class UserInterFace
{
    //TODO kolla status vid bordet, tex de vill ha något mer att dricka, dessert, de är nöjda/missnöjda kompensera? 
    //Sale: Type
    public static double Pay { get; set; }
    public static bool Cash { get; set; }

    public static double AmountToPay { get; set; }  //TODO belopp här tillfälligt tills bordslogik finns
    public static List<Product> orderList = new();

    public static void PrintOrderlist()
    {
        int i = 1;

        foreach (Product p in orderList) //TODO plussa på istället för att varje artikel hamnar på en egen rad, tex, 3 öl, 4 pizzor
        {
            // if (p.Quantity > 0)
            // {
            Console.WriteLine(i + ". " + p.Name + " - " + p.Price + " kr. "); //  + p.Quantity + " st"
            i++;
            //}
        }

    }
    public static void Payment()
    {

        Console.WriteLine("*******BETALNING********");
        //TODO skriv in bordsnummer
        Console.Write("1. Kort eller 2. kontant?: ");
        int input = int.Parse(Console.ReadLine());

        Console.WriteLine("Summa att betala: " + AmountToPay);

        switch (input)
        {

            case 1: // KORT
                Cash = false;
                GetPayment();
                break;

            //TODO stäng bordet

            case 2: // KONTANT
                Cash = true;
                GetPayment();
                break;
                //TODO stäng bordet


        }
    }
    public static void GetPayment()
    {

        while (true)
        {
            Console.Write("Slå in totalbelopp: ");
            int payment = int.Parse(Console.ReadLine());

            if (payment < AmountToPay)
            {
                Console.WriteLine("Beloppet är för lågt! Summa att betala är " + AmountToPay);
                continue;
            }
            if (Cash == true)
            {
                double change = payment - AmountToPay;
                Console.WriteLine("Din växel är " + change + " kr."); //TODO ska den inte dricksa? //If tips AddToSale eller Du dricksade + change
                Console.WriteLine("Tack!");
                Console.WriteLine("Skriva ut kvitto?"); //TODO fixa
                break;
            }
            else if (payment >= AmountToPay)
            {
                Console.WriteLine("Betalning genomförs..."); // TODO sleep thread så det är en väntetid? 
                Console.WriteLine("Tack!");
                Console.WriteLine("Skriva ut kvitto?"); //TODO fixa
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

    public static void Order() //TODO kan den plussa istället för att lägga samma artikel på ny rad? 
    {
        ProductHandler.PrintProduct();
        // välj bordsnummer OpenTable();

        while (true)
        {
            Console.Write("Skriv in beställning, ange siffra. q för klar: ");
            string choice = Console.ReadLine();
            if (choice == "q")
            {
                break;
            }
            if (int.TryParse(choice, out int number))
            {
                foreach (Product p in ProductHandler.productList)
                {
                    if (number - 1 == ProductHandler.productList.IndexOf(p))
                    {

                        orderList.Add(p);
                        break;
                    }

                }
                PrintOrderlist();
                CountOrder();
            }
        }
    }

    public static void CountOrder() 
    {
        foreach (Product p in orderList)
        {
            AmountToPay += p.Price; //p.Quantity *

        }
        Console.WriteLine("Totalbelopp: " + AmountToPay);

    }
    public static void PrintReceipt()
    {
        int ReceiptNumber = 1000; //Todo gör till egenskap
        Console.WriteLine("\t\tKvittonummer: " + ReceiptNumber + "\n\n");
        Console.WriteLine(); //Vilken Användare/servis
        Console.WriteLine(); //DateTime från när betalningen gått igenom
                            //Betalda artiklar
                            //Betald summa
                            //Varav dricks(Extra)
                            //Varav moms
                             
        foreach (Product food in ProductHandler.productList)
        {
            if (food.MenuItem == Product.ProductType.Food)
            {
                Console.WriteLine("\t{0, -20} {1, -15} {2}", food.Name, food.Price + "kr ", food.Description);
                continue;
            }
        }
        ReceiptNumber++;
    }
    // public static void AddOrderToTable() { } Ska vara i tablehandler
    // public static void OpenTable() { } ska vara i tablehandler
    // public static void SendOrder() { } ska vara i tablehandler
    // public static void CancelOrder() { } ska vara i tablehandler
    public static void CreateMenuDescription()
    {// OM en produkt inte innehåller en beskriving, fyll i beskrivning
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
                Console.Write("Beskriv produkten " + ProductHandler.productList[i].Name + ": ");
                string answer = Console.ReadLine();
                answer = UppercaseFirst(answer);
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
                    string input = Console.ReadLine();
                    input = UppercaseFirst(input);
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
        Console.Write("Vilken artikel ska ändras? Ange siffra: ");
        int number = int.Parse(Console.ReadLine());

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
    private static string UppercaseFirst(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        return char.ToUpper(str[0]) + str.Substring(1).ToLower();
    }
}
