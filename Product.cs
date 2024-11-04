// Klass för en produkt med div. egenskaper.

public class Product
{
    public enum ProductType // förrätt, varmrätt, dessert? 
    {
        Food = 1,
        Alcohol = 2,
        Beverage = 3

    }

    //  int[] array = new int[2];
    //  string[] ProductType = ["food", "Drinks"];
    public enum VatRate //momssatser för produkter 
    {
        _12 = 1,
        _25 = 2,

    }


    public string Name { get; set; }
    public double Price { get; set; }
    public ProductType MenuItem { get; set; } // typ av produkt som relaterar till vilken moms som gäller för produkten
    public VatRate VatItem { get; set; } // momssats
    public string Description { get; set; }


    public Product(string name, double price, ProductType menuItem, VatRate vatItem)
    {
        Name = name;
        Price = price;
        MenuItem = menuItem;
        VatItem = vatItem;
    }
}
public static class ProductHandler
{
    public static List<Product> productList { get; set; } = new(); // 

    public static void PrintProduct() // varför inte void tex?
    //Om du bara ska skriva ut produkter är void tillräckligt. 
    //Om du vill att metoden också ska "passa vidare" listan kan List<Product> som returtyp vara ett bra alternativ.
    {
        int i = 1;

        foreach (Product p in productList)
        {

            Console.WriteLine(i + ". " + p.MenuItem + ": " + p.Name + " - " + p.Price + " kr " + p.VatItem + "% moms");
            i++;

        }

    }

    public static void AddProduct() //  If food == vatRate._12
    {
        PrintProductType(); // visar alternativen
        Console.WriteLine("q för quit");
        Console.Write("Ange typ av produkt utifrån siffra: ");

        var typearray = Enum.GetValues(typeof(Product.ProductType)); // gör om producttype till array  
        var vatarray = Enum.GetValues(typeof(Product.VatRate)); // gör om vatItem till array 
        string input = Console.ReadLine(); // användare lägger till typ och moms genom att ange heltal
        input = UppercaseFirst(input);

        // If-sats
        bool addmeny = true;
        while (addmeny)
        {
            if (int.TryParse(input, out int intinput)) //TODO kolla upp varför int input funkade utan -1 i array
            {
                if (intinput != 1 && intinput != 2 && intinput != 3)
                {
                    Console.Write("Ogiltig input!");
                    addmeny = false;
                }


                else
                {
                    //OM input = 1, 12% = 0
                    //OM input = 2, 25% = 1
                    //OM input = 3, 12% = 0

                    Product.ProductType selectedItemType = (Product.ProductType)typearray.GetValue(intinput - 1); // hämtar produkttypen efter angivet heltal ??
                    Product.VatRate selectedVatType = Product.VatRate._12; //Standard 12%, alkohol 25  
                    if (intinput == 2) // om input = 2, 25%
                    {
                        selectedVatType = (Product.VatRate)vatarray.GetValue(1);
                    }

                    Console.Write("Produktens namn: ");
                    string? name = UppercaseFirst(Console.ReadLine());
                    Console.Write("Pris: ");
                    double price = double.Parse(Console.ReadLine());
                    Product newProduct = new(name, price, selectedItemType, selectedVatType);
                    productList.Add(newProduct);
                    PrintProduct();
                    addmeny = false;
                }
            }
            else if (input == "Q")
            {
                Console.WriteLine("Hejdå!");
                break;
            }
            else
            {
                Console.WriteLine("Ogiltig inmatning. Försök igen.");
                break;
            }
        }
    }




    public static void PrintProductType()
    {
        int moms;

        foreach (Product.ProductType p in Enum.GetValues(typeof(Product.ProductType)))
        {
            if (p == Product.ProductType.Alcohol)
            {
                moms = 25;
            }
            else
            {
                moms = 12;
            }
            Console.WriteLine((int)p + ". " + " " + p + " " + moms + "% moms");

        }
    }


    public static void RemoveProduct()
    {
        while (true)
        {
            PrintProduct();
            Console.Write("Ange vilken produkt ska tas bort utifrån siffor: ");
            int remove = int.Parse(Console.ReadLine());
            Console.WriteLine("Du har tagit bort produkten " + productList[remove - 1].Name);
            productList.RemoveAt(remove - 1);
            Console.Write("Vill du fortsätta ta bort produkter? j/n: ");
            string answer = Console.ReadLine();
            answer = UppercaseFirst(answer);

            if (answer == "J")
            {
                continue;
            }
            else if (answer == "N")
            {
                break;
            }
            else
            {
                Console.WriteLine("Ogiltig input! Mata enbart in j/n!");
            }
        }


    }


    //TODO Generell prisändring på alla produkter inom kategori
    public static void ModifyProduct(Product product)
    {
        while (true)
        {
            PrintProduct();
            Console.Write("Välj vilken produkt du vill uppdatera, ange siffa: ");
            int pickProduct = int.Parse(Console.ReadLine());

            Console.WriteLine("1. Pris");
            Console.WriteLine("2. Namn");
            Console.WriteLine("3. Produkttyp & moms");
            Console.Write("Välj vad som ska ändras, ange siffa: ");
            int choice = int.Parse(Console.ReadLine());
            foreach (Product p in productList)
            {
                if (choice == 1)
                {
                    Console.Write("Ange nytt pris: ");
                    int newPrice = int.Parse(Console.ReadLine());
                    product.Price = newPrice;
                    break;
                }
                else if (choice == 2)
                {
                    Console.Write("Ange nytt namn: ");
                    string? newName = Console.ReadLine();
                    product.Name = newName;
                    break;
                }
                else if (choice == 3)
                {

                    PrintProductType();
                    Console.Write("Välj ny produkttyp, ange siffra: ");
                    int type = int.Parse(Console.ReadLine());
                    // 0 = Food, 1 = alcohol, 2 = beverage
                    var newItem = (Product.ProductType)Enum.GetValues(typeof(Product.ProductType)).GetValue(type-1); // tilldela newItem till MenuItem av index input. Kom ihåg -1!
                    productList[pickProduct -1].MenuItem = newItem; // -1 för att listan börjar på 0. tilldelar den värdet av type
                    //TODO fixa momsen
                    break;

                }
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