// Klass för en produkt med div. egenskaper.
/* using Newtonsoft.Json;

*/
public class Product
{   
    public enum ProductType // förrätt, varmrätt, dessert 
    {
        Food = 1,
        Drinks = 2
    }

//  int[] array = new int[2];
//  string[] ProductType = ["food", "Drinks"];
    public enum VatRate //momssatser för produkter 
    {
        _12 = 1,
        _25 = 2
    }

    public string Name {get ; set ;}
    public double Price {get ; set ;}
    //public double VAT {get; set;}
    public ProductType MenuItem {get ; set ;} // typ av produkt som relaterar till vilken moms som gäller för produkten
    public VatRate VatItem {get ; set ;} // momssats

    public Product (string name, double price, ProductType menuItem, VatRate vatItem)
    {
        Name = name;
        Price = price;
        MenuItem = menuItem;
        VatItem = vatItem;
    }    
}
public static class ProductHandler
{
    public static List<Product> productList {get ; set ;} = new(); // 

    public static void PrintProduct () // varför inte void tex?
    //Om du bara ska skriva ut produkter är void tillräckligt. 
    //Om du vill att metoden också ska "passa vidare" listan kan List<Product> som returtyp vara ett bra alternativ.
    {
        foreach (Product p in productList)
        {
            
            Console.WriteLine(productList.Count());
            Console.WriteLine(p.MenuItem +": "+ p.Name + " - " + p.Price + " kr " + p.VatItem);
            
        }

    }

    public static void AddProduct(Product product) //  If food == vatRate._12
    {
        PrintProductType(); // visar alternativen
        Console.WriteLine("Typ av produkt: ");
        Console.WriteLine("q för quit"); //TODO "global" Quit-metod
        Console.WriteLine("Ange typ utifrån siffra.");
        
        var typearray = Enum.GetValues(typeof(Product.ProductType)); // gör om producttype till array
        var vatarray = Enum.GetValues(typeof(Product.VatRate)); // gör om vatItem till array 
        string input = Console.ReadLine(); // användare lägger till typ och moms genom att ange heltal
        input = UppercaseFirst(input);
        
        // If-sats
        bool addmeny = true;
        while(addmeny)
        { 
            if (int.TryParse(input, out int intinput)) //TODO kolla upp varför int input funkade utan -1 i array
            {
                if (intinput != 1 && intinput != 2) 
                {
                    Console.Write("Ogiltig input!");
                    addmeny = false;
                }
            

                else
                {
                    Product.ProductType selectedItemType = (Product.ProductType)typearray.GetValue(intinput -1); // hämtar produkttypen efter angivet heltal ??
                    Product.VatRate selectedVatType = (Product.VatRate)vatarray.GetValue(intinput -1); // hämtar och sätter momssats efter angivet heltal
                    
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
            else if (input == "Q" )
            {   
                Console.WriteLine("Hejdå!");
                break;
            }
            else
            {
                Console.WriteLine("Ogiltigt tjänstenummer. Försök igen.");
                break;
            }
        }
    }
                            //     List<Officer> dispatchOfficers = new List<Officer>();
                            // bool continueAdding = true;

                            // while (continueAdding)
                            // {
                            //     Console.WriteLine("Tillgängliga poliser");
                            //     newOfficer.PrintRoster(); // Visar alla poliser i roster

                            //     Console.WriteLine("Ange tjänstenummer för att lägga till en polis:");
                            //     badgeInput = Console.ReadLine();

                            //     if (int.TryParse(badgeInput, out badgeNr))
                            //     {
                            //         // Hitta polisen med det angivna tjänstenumret
                            //         Officer officerToAdd = newOfficer.roster.Find(officer => officer.BadgeNr == badgeNr);

                            //         if (officerToAdd != null)
                            //         {
                            //             Console.WriteLine($"Vill du lägga till {officerToAdd.FirstName} {officerToAdd.LastName}? (J/N)");
                            //             input = Console.ReadLine().ToUpper();

                            //             if (input == "J")
                            //             {
                            //                 dispatchOfficers.Add(officerToAdd); // Lägg till polisen i dispatchlistan
                            //                 Console.WriteLine($"{officerToAdd.FirstName} {officerToAdd.LastName} tillagd.");
                            //             }
                            //             else if (input == "N")
                            //             {
                            //                 Console.WriteLine("Åtgärd avbruten.");
                            //             }
                            //             else
                            //             {
                            //                 Console.WriteLine("Ogiltigt val.");
                            //             }
                            //         }
                            //         else
                            //         {
                            //             Console.WriteLine("Ingen polis med detta tjänstenummer hittades.");
                            //         }
                            //     }
                            //     else
                            //     {
                            //         Console.WriteLine("Ogiltigt tjänstenummer. Försök igen.");
                            //     }

                            //     // Fråga om användaren vill lägga till ytterligare en polis
                            //     Console.WriteLine("Vill du lägga till ytterligare en polis? (J/N)");
                            //     string continueInput = Console.ReadLine().ToUpper();

                            //     if (continueInput != "J")
                            //     {
                            //         continueAdding = false;
                            //     }
                            // }

    private static string UppercaseFirst(string str)
    {
    if (string.IsNullOrEmpty(str))
    return string.Empty;
    return char.ToUpper(str[0]) + str.Substring(1).ToLower();   
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

    }
    public static void ModifyProduct()
    {

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