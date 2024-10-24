using System.Runtime.Intrinsics.Arm;
using System.Runtime.Serialization.Formatters;

public class Product
{   
    public enum ProductType // förrätt, varmrätt, dessert 
    {
        Food = 1,
        Drinks = 2
        
        
        
    }
    public enum VatRate //momssatser för produkter 
    {
        _25,
        _12
    }

    public string Name {get ; set ;}
    public double Price {get ; set ;}
    public double VAT {get; set;}
    public ProductType MenuItem {get ; set ;}
    public VatRate VatItem {get ; set ;} // bättre namn?

    public Product (string name, double price, ProductType menuItem, VatRate vatItem)
    {
        Name = name;
        Price = price;
        MenuItem = menuItem;
        VatItem = vatItem;
        
    }
        public List<Product> PrintProduct (List<Product> productList)
    {
        foreach (Product p in productList )
        {
            Console.WriteLine(p.Name + " - " + p.Price + " kr");
        }
        return productList;
    }
}
public static class ProductHandler
{
    public static List<Product> productList {get ; set ;} = new();



    public static void AddProduct() //  If food == vatRate._12
    {
        Console.Write("Typ av produkt: "); //TODO välja från enumlistan? 

        Console.WriteLine("Ange typ utifrån siffra.");

        //  
        PrintProductType();
        var productarray = Enum.GetValues(typeof(Product.ProductType)); // steffe fattar naaaada
        string input = Console.ReadLine();
        for (int i = 0; i < productarray.Length; i++)
        {
            if (input == "1")
            {
                    //productarray = Enum.GetValues(typeof(Product.ProductType));
            } 
        }




        int? type = int.Parse(Console.ReadLine());
        Console.Write("Produktens namn: ");
        string? name = Console.ReadLine();
        Console.Write("Pris: ");
        double price = double.Parse(Console.ReadLine());
        
        Product product = new(name, price, Product.ProductType.Food, Product.VatRate._12);
                        
                        
                    // PrintJoblist(); = foreach (Staff.Jobs job in Enum.GetValues(typeof(Staff.Jobs)))
                    //Console.WriteLine((int)job + " " + job);
                    //_____________________________________
                    // Console.Write("Välj arbetssyssla utifrån siffor: ");
                    // var jobArray = Enum.GetValues(typeof(Staff.Jobs)); // konvertera enum till en array
                    // int choice2 = int.Parse(Console.ReadLine());// sök efter match i enum-lista (heter det ens så?)
                    // for (int j = 0; j < jobArray.Length; j++)
                    // {

                    //     if (choice2 == j + 1)  // om match är hittad
                    //     {
                    //         stafflist[i].CurrentJob = (Staff.Jobs)jobArray.GetValue(j);
                    //         // staff.stafflist[i].Job = staff.worklist[j];
                    //         Console.WriteLine(stafflist[i].Name + " har just nu arbetsuppgiften " + stafflist[i].CurrentJob);// skriv ut
                    //         staff.PrintStaff(stafflist);
                    //         break;
                    //     }

                    // }
        

                        //Product prod = new Product(name, price, Product.ProductType, Product.VatRate._12);


    }
   
    public static void PrintProductType()
    {
        foreach (Product.ProductType p in Enum.GetValues(typeof(Product.ProductType)))
        {
            Console.WriteLine((int)p + " " + p);
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