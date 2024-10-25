// Klass för en produkt med div. egenskaper.
public class Product
{   
    public enum ProductType // förrätt, varmrätt, dessert 
    {
        Food,
        Drinks    
    }

//  int[] array = new int[2];
//  string[] ProductType = ["food", "Drinks"];
    public enum VatRate //momssatser för produkter 
    {
        _12,
        _25
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
    public static List<Product> productList {get ; set ;} = new();

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
        Console.Write("Typ av produkt: ");
        Console.WriteLine("Ange typ utifrån siffra.");
        
        var typearray = Enum.GetValues(typeof(Product.ProductType)); // gör om producttype till array
        var vatarray = Enum.GetValues(typeof(Product.VatRate)); // gör om vatItem till array 
        
        int input = int.Parse(Console.ReadLine()); 
        
        Product.ProductType selectedItemType = (Product.ProductType)typearray.GetValue(input); // 
        Product.VatRate selectedVatType = (Product.VatRate)vatarray.GetValue(input);
        Console.Write("Produktens namn: ");
        string? name = Console.ReadLine();
        Console.Write("Pris: ");
        double price = double.Parse(Console.ReadLine());
        Product newProduct = new(name, price, selectedItemType, selectedVatType);
        productList.Add(newProduct);                
        PrintProduct();    
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