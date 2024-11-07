public static class UserInterFace
{
    //TODO kolla status vid bordet, tex de vill ha något mer att dricka, dessert, de är nöjda/missnöjda kompensera? 
    //Sale: Type
    
    public static double PaidAmount { get; set; }
    public static double Change {get;set;}
    public static double AmountToPay { get; set; }
    public static double Vat12 {get;set;}
    public static double Vat25 {get;set;}
    public static bool IsCash { get; set; }
    public static List<Product> orderList = new();

    public static void PrintOrderlist()
    {    

        foreach (Product p in orderList) //TODO plussa på istället för att varje artikel hamnar på en egen rad, tex, 3 öl, 4 pizzor
        {
            // if (p.Quantity > 0)
            // {
            Console.WriteLine(p.ProductNumber + ". " + p.Name + " - " + p.Price + " kr. "); //  + p.Quantity + " st"
            
            //}
        }

    }


    public static void Order(Product product) //TODO kan den plussa istället för att lägga samma artikel på ny rad? 
    {
        ProductHandler.PrintProduct();
        Console.WriteLine();

        while (true)
        {
            Console.Write("Skriv in beställning, ange artikelnr. 'Q' för klar: ");
            string? choice = Console.ReadLine().ToUpper();
            
            if (int.TryParse(choice, out int number))
            {
                Product prouktSomSkaVisas = ProductHandler.productList.Find(product => product.ProductNumber == number);
                    if (prouktSomSkaVisas != null)
                    {
                        orderList.Add(prouktSomSkaVisas);
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
                //TableHandler objekt = new TableHandler();
                Console.WriteLine("Betala (d)irekt eller lägga order till (b)ord?");
                string paymentChoice = Console.ReadLine();
                paymentChoice = UppercaseFirst(paymentChoice); 
                if (paymentChoice == "D") 
                {
                    Payment();
                    break;
                }
                else if (paymentChoice == "B")
                {
                    TableHandler.OrderToTable(number);
                    break;
                }
                else 
                {
                    Console.WriteLine("Ogiltig input");

                }
            
            //TableHandler.OpenTable(number); //eller skicka till betalning

            }

        }
    }

    public static void CountTotal() 
    {

        foreach (Product p in orderList)
        {
            AmountToPay += p.Price; //p.Quantity *

        }
        Console.WriteLine("Totalbelopp: " + AmountToPay);
        AmountToPay = 0;

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
                IsCash = false;
                GetPayment();
                break;

            //TODO stäng bordet

            case 2: // KONTANT
                IsCash = true;
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
            PaidAmount = int.Parse(Console.ReadLine());

            if (PaidAmount < AmountToPay)
            {
                Console.WriteLine("Beloppet är för lågt! Summa att betala är " + AmountToPay);
                continue;
            }
            if (IsCash == true)
            {
                Change = PaidAmount - AmountToPay;
                Console.WriteLine("Din växel är " + Change + " kr."); //TODO ska den inte dricksa? //If tips AddToSale eller Du dricksade + change
                Console.WriteLine("Tack!");
                Console.WriteLine("Kvitto: ");
                PrintReceipt();
                break;
            }
            else if (PaidAmount >= AmountToPay)
            {   
                Change = PaidAmount - AmountToPay;
                Console.WriteLine("Betalning genomförs..."); // TODO sleep thread så det är en väntetid? 
                Console.WriteLine("Tack!");
                Console.WriteLine("Kvitto: ");
                PrintReceipt(); // indata kvittonummer för att hålla reda på? 
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
    public static void PrintReceipt()
    {
        int ReceiptNumber = 1000; //Todo gör till egenskap
        Console.WriteLine("\t\tKvittonummer: " + ReceiptNumber + "\n\n");
        Console.WriteLine(); //Vilken Användare/servis
        Console.WriteLine(); //DateTime från när betalningen gått igenom
        
        PrintOrderlist();    
        // foreach (Product p in orderList)//Betalda artiklar ta från bordslistan
        // {       
        //     Console.WriteLine("\t{0, -20} {1, -15}", p.Name, p.Price + "kr ");
        // }
        
        Console.WriteLine("Betald summa: " + PaidAmount);//Betald summa
        Console.WriteLine("Varav dricks: " + Change);//Varav dricks(Extra)
        CalculateVat();
        Console.WriteLine("Varav moms 12%: " + Vat12);//Varav moms
        Console.WriteLine("Varav moms 25%: " + Vat25);
        ReceiptNumber++;
    }
    public static void CalculateVat()
    {
        foreach (Product p in orderList)
        {
            if (p.MenuItem == Product.ProductType.Food|| p.MenuItem == Product.ProductType.Beverage)
            {
                
                Vat12 += p.Price;
            }
            else if (p.MenuItem == Product.ProductType.Alcohol)
            {
                p.Price += Vat25;
            }
        }
        //95 * 0.25
        

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
    public static void UserInterFaceStartMenu()
    {
        Console.WriteLine("1.");
    }
    private static string UppercaseFirst(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        return char.ToUpper(str[0]) + str.Substring(1).ToLower();
    }
}
