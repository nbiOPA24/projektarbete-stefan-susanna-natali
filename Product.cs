// Klass för en produkt med div. egenskaper.
//TODO ta bort momsen från konstruktorn så den uppdateras automatiskt ist
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
    private static int nextNumber = 1;
    public ProductType MenuItem { get; set; } // typ av produkt som relaterar till vilken moms som gäller för produkten
    public VatRate VatItem { get; set; } // momssats
    public string Description { get; set; }
    public int Quantity { get; set; }

    public Product(string name, double price, ProductType menuItem, VatRate vatItem)
    {
        Name = name;
        Price = price;
        MenuItem = menuItem;
        VatItem = vatItem;

        ProductNumber += nextNumber; //uppdatera produktnummer efter varje skapat objekt
        nextNumber++;

    }
    public Product() { } //behov??
}
public static class ProductHandler
{

    public static List<Product> productList { get; set; } = new();

    public static void PrintProduct() // varför inte void tex?
    //Om du bara ska skriva ut produkter är void tillräckligt. 
    //Om du vill att metoden också ska "passa vidare" listan kan List<Product> som returtyp vara ett bra alternativ.
    {

        foreach (Product p in productList)
        {
            Console.WriteLine(p.ProductNumber + ". " + p.MenuItem + ": " + p.Name + " - " + p.Price + " kr " + p.VatItem + "% moms. Beskrivning: " + p.Description);
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
        input = UserInterFace.UppercaseFirst(input);

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
                    string? name = UserInterFace.UppercaseFirst(Console.ReadLine());
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
            //AdjustVatItem();
            Console.WriteLine((int)p + ". " + " " + p); //TODO lägg till moms här också?

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


    //TODO Generell prisändring på alla produkter inom kategori
    public static void EditProduct()
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
                p.Price = newPrice;
                break;
            }
            else if (choice == 2)
            {
                Console.Write("Ange nytt namn: ");
                string? newName = Console.ReadLine();
                p.Name = newName;
                break;
            }
            else if (choice == 3)
            {

                PrintProductType();
                Console.Write("Välj ny produkttyp, ange siffra: ");
                int type = int.Parse(Console.ReadLine());
                // 0 = Food, 1 = alcohol, 2 = beverage
                var newItem = (Product.ProductType)Enum.GetValues(typeof(Product.ProductType)).GetValue(type - 1); // tilldela newItem till MenuItem av index input. Kom ihåg -1!
                productList[pickProduct - 1].MenuItem = newItem; // -1 för att listan börjar på 0. tilldelar den värdet av type
                AdjustVatItem();
                //TODO fixa momsen
                break;

            }
        }
    }



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
    public static void ProductStartMenu()
    {
        while (true)
        {
            Console.WriteLine("1. Se alla produkter");
            Console.WriteLine("2. Lägg till produkt");
            Console.WriteLine("3. Ta bort produkt");
            Console.WriteLine("4. Ändra en produkt");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    PrintProduct();
                    break;
                case 2:
                    AddProduct();
                    break;
                case 3:
                    RemoveProduct();
                    break;
                case 4:
                    EditProduct();
                    break;
            }
        }
    }

}