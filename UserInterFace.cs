public static class UserInterFace
{
    //Sale: Type

    public static void Payment() { }
    public static void SplitPayment() { }
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
    public static void EditMenuDescription(){}
    public static void DisplayMenu()
    {
        Console.WriteLine("\t\tRestaurangmeny\n\n");

        foreach (Product p in ProductHandler.productList)
        {
            if (p.MenuItem == Product.ProductType.Food)
            {
                Console.WriteLine("\t{0, -20} {1, -15} {2}", p.Name, p.Price + "kr ", p.Description);
                continue;
            }
            Console.WriteLine("\n\n\t\tBarmeny\n\n");
            if (p.MenuItem == Product.ProductType.Alcohol)
            {
                Console.WriteLine("\t{0, -20} {1, -15} {2}", p.Name, p.Price +"kr ", p.Description);
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
