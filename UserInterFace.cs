public static class UserInterFace
{
    //TODO kolla status vid bordet, tex de vill ha något mer att dricka, dessert, de är nöjda/missnöjda kompensera? 
    //Sale: Type
    public static double Pay { get; set; }
    public static bool Cash { get; set; }
    public static double AmountToPay = 650;  //TODO belopp här tillfälligt tills bordslogik finns

    public static void Payment()
    {

        Console.WriteLine("*******BETALNINGSTERMINAL********");
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
                Console.WriteLine("Din växel är " + change + " kr."); //TODO ska den inte dricksa? 
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
    public static void SplitPayment()
    {
        // Splitta broderligt
        // Splitta per person

    }
    public static void Order() { }
    public static void AddOrderToTable() { }
    public static void OpenTable() { }
    public static void SendOrder() { }
    public static void CancelOrder() { }
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
                Console.Write("Vill du fortsätta? Du har " + descriptionsToFill + " kvar att fylla i. j/n: ");
                string input = Console.ReadLine();
                input = UppercaseFirst(input);
                if (input == "J")
                {
                    continue;
                }
                else if (input == "N")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Ogiltig input!");
                    //TODO gör så att den också fortsätter tillbaka till Vill du fortsätta? 
                }
            }
        }
    }
    public static void EditMenuDescription() { }
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
        }//TODO lägg till dryck någonstans också




    }
    private static string UppercaseFirst(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        return char.ToUpper(str[0]) + str.Substring(1).ToLower();
    }
}
