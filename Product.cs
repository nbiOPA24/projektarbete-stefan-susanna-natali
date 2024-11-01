// Klass för en produkt med div. egenskaper.
/* using Newtonsoft.Json;

*/
public class Product
{
    public enum ProductType // förrätt, varmrätt, dessert 
    {
        Food = 1,
        Alcohol = 2,
        Beverages = 3

    }

    //  int[] array = new int[2];
    //  string[] ProductType = ["food", "Drinks"];
    public enum VatRate //momssatser för produkter 
    {
        Moms_12 = 1,
        Moms_25 = 2,

    }


    public string Name { get; set; }
    public double Price { get; set; }
    //public double VAT {get; set;}
    public ProductType MenuItem { get; set; } // typ av produkt som relaterar till vilken moms som gäller för produkten
    public VatRate VatItem { get; set; } // momssats
    //TODO beskrivning till menyer

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

            Console.WriteLine(i + ". " + p.MenuItem + ": " + p.Name + " - " + p.Price + " kr " + p.VatItem); //TODO gör sån -45 för att få det centrerat
            i++;

        }

    }

    public static void AddProduct() //  If food == vatRate._12
    {
        PrintProductType(); // visar alternativen
        Console.WriteLine("q för quit"); //TODO "global" Quit-metod
        Console.Write("Ange typ av produkt utifrån siffra: ");

        var typearray = Enum.GetValues(typeof(Product.ProductType)); // gör om producttype till array //TODO ev global för klassen? 
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
                    Product.VatRate selectedVatType = Product.VatRate.Moms_12; //Standard 12%, alkohol 25  
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

        foreach (Product.ProductType p in Enum.GetValues(typeof(Product.ProductType)))
        {
            Console.WriteLine((int)p + ". " + " " + p);

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
        //TODO sortera efter produkttyp? 


    }

    //TODO Generell prisändring på alla produkter inom kategori
    //TODO sortera efter produkttyp?
    public static void ModifyProduct()
    {
        while (true)
        {
            PrintProduct();
            Console.Write("Välj vilken produkt du vill uppdatera? Ange siffa: ");
            int pickProduct = int.Parse(Console.ReadLine());

            Console.WriteLine("1. Pris");
            Console.WriteLine("2. Namn");
            Console.WriteLine("3. Produkttyp & moms");
            Console.Write("Välj vad som ska ändras, ange siffa: ");
            int choice = int.Parse(Console.ReadLine());
            foreach (Product p in productList)
                if (choice == 1)
                {
                    Console.Write("Ange nytt pris: ");
                    int newPrice = int.Parse(Console.ReadLine());
                    p.Price = newPrice;
                }
                else if (choice == 2)
                {
                    Console.Write("Ange nytt namn: ");
                    string? newName = Console.ReadLine();
                    p.Name = newName;
                }
                else if (choice == 3)
                {
                    Console.Write("Ange ny produkttyp: ");
                }
        }

    }
    public static void DisplayMenu()
    {
        Console.WriteLine("\t\tRestaurangmeny\n\n");
        
        foreach (Product p in productList)
        {
            if (p.MenuItem == Product.ProductType.Food)
            {
                Console.WriteLine("\t{0, -20} {1}", p.Name, p.Price);
                continue;
            }
            Console.WriteLine("\n\n\t\tBarmeny\n\n");
            if (p.MenuItem == Product.ProductType.Alcohol)
            {
                Console.WriteLine("\t{0, -20} {1}", p.Name, p.Price);
            }


        }

    }
    private static string UppercaseFirst(string str)
    {
        if (string.IsNullOrEmpty(str))
            return string.Empty;
        return char.ToUpper(str[0]) + str.Substring(1).ToLower();
    }


    // static ProductHandler()
    // {
    //    productList = new();
    // }
    /*public static void Add()
    {
        Product addProduct = new Product();
        productList.Add(addProduct);
        
    } */
}