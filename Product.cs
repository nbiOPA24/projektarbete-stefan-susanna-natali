
public class Product
{
    public enum ProductType // förrätt, varmrätt, dessert? 
    {
        Food = 1,
        Alcohol = 2,
        Beverage = 3

    }

    public enum VatRate //momssatser för produkter 
    {
        _12 = 1,
        _25 = 2,

    }


    public string Name { get; set; }
    public double Price { get; set; }
    public int ProductNumber { get; set; }
    public static int nextNumber = Data.LoadNextProductNumber("nextproductnumber.json");
    public ProductType MenuItem { get; set; } // typ av produkt som relaterar till vilken moms som gäller för produkten
    public VatRate VatItem { get; set; } // momssats
    public string Description { get; set; }

    public Product(string name, double price, ProductType menuItem)
    {
        Name = name;
        Price = price;
        MenuItem = menuItem;

        ProductNumber = nextNumber; //uppdatera produktnummer efter varje skapat objekt
        nextNumber++;

    }
    public Product() { } //behov??
}
public static class ProductHandler
{

    public static List<Product> productList { get; set; } = new();
    #region PrintProduct
    public static void PrintProduct()
    {
        AdjustVatItem();

        foreach (Product p in productList)
        {
            Console.WriteLine(p.ProductNumber + ". " + p.MenuItem + ": " + p.Name + " - \t" + p.Price + " kr " + p.VatItem + "% moms. Beskrivning: " + p.Description);
        }

    }
    #endregion
    #region AddProduct
    public static void AddProduct() //  If food == vatRate._12
    {
        PrintProductType(); // visar alternativen
        Console.WriteLine("q för quit");
        Console.Write("Ange typ av produkt utifrån siffra: ");

        var typearray = Enum.GetValues(typeof(Product.ProductType)); // gör om producttype till array  
        string input = Console.ReadLine(); // användare lägger till typ och moms genom att ange heltal
        input = UserInterFace.UppercaseFirst(input);

        // If-sats  
        bool addmeny = true;
        while (addmeny)
        {
            if (int.TryParse(input, out int intinput))
            {
                if (intinput != 1 && intinput != 2 && intinput != 3)
                {
                    Console.Write("Ogiltig input!");
                    addmeny = false;
                }
                else
                {
                    Product.ProductType selectedItemType = (Product.ProductType)typearray.GetValue(intinput - 1); // hämtar produkttypen efter angivet heltal
                    Console.Write("Produktens namn: ");
                    string? name = UserInterFace.UppercaseFirst(Console.ReadLine());
                    Console.Write("Pris: ");
                    double price = double.Parse(Console.ReadLine());
                    Product newProduct = new(name, price, selectedItemType);
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

    #endregion
    #region PrintProType

    public static void PrintProductType()
    {
        foreach (Product.ProductType p in Enum.GetValues(typeof(Product.ProductType)))
        {
            Console.WriteLine((int)p + ". " + " " + p);
        }
    }
    #endregion
    #region RemoveProd
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
            answer = UserInterFace.UppercaseFirst(answer);

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
    #endregion
    #region EditProduct
    //TODO Generell prisändring på alla produkter inom kategori
    public static void EditProduct()
    {
        PrintProduct();
        Console.Write("Välj vilken produkt du vill uppdatera, ange siffa: ");
        int pickProduct = int.Parse(Console.ReadLine());
        ReadInt(pickProduct);
        Console.WriteLine("1. Pris");
        Console.WriteLine("2. Namn");
        Console.WriteLine("3. Produkttyp & moms");
        Console.Write("Välj vad som ska ändras, ange siffa: ");
        int choice = int.Parse(Console.ReadLine());
        foreach (Product p in productList)
        {
            if (choice == 1 && pickProduct == p.ProductNumber) //TODO fungerar denna ens?
            {
                Console.Write("Ange nytt pris: ");
                int newPrice = int.Parse(Console.ReadLine());
                p.Price = newPrice;
                break;
            }
            else if (choice == 2 && pickProduct == p.ProductNumber)
            {
                Console.Write("Ange nytt namn: ");
                string? newName = Console.ReadLine();
                p.Name = newName;
                break;
            }
            else if (choice == 3 && pickProduct == p.ProductNumber)
            {

                PrintProductType();
                Console.Write("Välj ny produkttyp, ange siffra: ");
                int type = int.Parse(Console.ReadLine());
                // 0 = Food, 1 = alcohol, 2 = beverage
                var newItem = (Product.ProductType)Enum.GetValues(typeof(Product.ProductType)).GetValue(type - 1); // tilldela newItem till MenuItem av index input. Kom ihåg -1 då listan börjar på 0!
                productList[pickProduct - 1].MenuItem = newItem; // -1 för att listan börjar på 0. tilldelar den värdet av type
                break;

            }

        }
    }

    #endregion
    #region AdjVatItem
    public static void AdjustVatItem()
    {
        for (int i = 0; i < productList.Count; i++)
        {
            if (productList[i].MenuItem == Product.ProductType.Alcohol)
            {
                productList[i].VatItem = Product.VatRate._25;

            }
            else
            {
                productList[i].VatItem = Product.VatRate._12;

            }
        }
    }
    #endregion
    #region StartMenu
    public static void ProductStartMenu()
    {
        while (true)
        {
            Console.WriteLine("1. Se alla produkter");
            Console.WriteLine("2. Lägg till produkt");
            Console.WriteLine("3. Ta bort produkt");
            Console.WriteLine("4. Ändra en produkt");
            Console.Write("Q för tillbaka: ");

            string? choice = Console.ReadLine().ToUpper();
            switch (choice)
            {
                case "1":
                    PrintProduct();
                    Data.SaveProductList("product.json");
                    break;
                case "2":
                    AddProduct();
                    Data.SaveProductList("product.json");
                    Data.SaveNextProductNumber("nextproductnumber.json");
                    break;
                case "3":
                    RemoveProduct();
                    Data.SaveProductList("product.json");
                    break;
                case "4":
                    EditProduct();
                    Data.SaveProductList("product.json");
                    break;
                case "Q":
                    return;
                default:
                    Console.WriteLine("Ogiltig input!");
                    break;
            }
        }
    }
    #endregion
        public static int ReadInt(int input)
    {
        
        while (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("Ogiltig inmatning, försök igen:");
        }
        return input;
    }
}