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
        _12 = 1,
        _25
        
    }

    public string Name {get ; set ;}
    public double Price {get ; set ;}
    //public double VAT {get; set;}
    public ProductType MenuItem {get ; set ;}
    public VatRate VatItem {get ; set ;} // bättre namn?

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

    public static List<Product> PrintProduct (List<Product> productList)
    {
        foreach (Product p in productList )
        {
            PrintProductType();
            Console.WriteLine(p.Name + " - " + p.Price + " kr");
        }
        return productList;
    }

    public static void AddProduct(Product product) //  If food == vatRate._12
    {
        PrintProductType(); // visar alternativen
        Console.Write("Typ av produkt: "); //TODO välja från enumlistan? 
        

        Console.WriteLine("Ange typ utifrån siffra.");
 

        var productarray = Enum.GetValues(typeof(Product.ProductType)); // steffe fattar naaaada gör om till aray

        int input = int.Parse(Console.ReadLine()); 

        for (int i = 0; i < productarray.Length; i++)
        {
            if (input == i+1) // Om input matchar indexet av arrayen +1 (pga enum startar på 1 och inte 0)
            {
                productList[i].MenuItem = (Product.ProductType)productarray.GetValue(i);
                productList[i].VatItem = (Product.VatRate)productarray.GetValue(i);
                Console.Write("Produktens namn: ");
                string? name = Console.ReadLine();
                Console.Write("Pris: ");
                double price = double.Parse(Console.ReadLine());
                Product newProduct = new(name, price, product.MenuItem, product.VatItem);
                productList.Add(newProduct);    
            }
            
            else
            {
                Console.WriteLine("Ogiltigt val!");
            } 
        }
    }


    public static void PrintProductType()
    {
        int i = 1;
        foreach (Product.ProductType p in Enum.GetValues(typeof(Product.ProductType)))
        {
            Console.WriteLine((int)p + ". " + " " + p); 
            i++;
        }
    }

        // int? type = int.Parse(Console.ReadLine());

        

            // Console.Write("Välj personal att schemalägga utifrån ID-nummer: ");
            // int choice1 = int.Parse(Console.ReadLine());
            // for (int i = 0; i < stafflist.Count; i++)
            // { // Sök efter match i stafflist
            //     if (choice1 == stafflist[i].IDNumber)
            //     { // Om en match är hittad

            //         PrintJoblist();
            //         Console.Write("Välj arbetssyssla utifrån siffor: ");
            //         var jobArray = Enum.GetValues(typeof(Staff.Jobs)); // konvertera enum till en array
            //         int choice2 = int.Parse(Console.ReadLine());// sök efter match i enum-lista (heter det ens så?)
            //         for (int j = 0; j < jobArray.Length; j++)
            //         {

            //             if (choice2 == j + 1)  // om match är hittad
            //             {
            //                 stafflist[i-1].CurrentJob = (Staff.Jobs)jobArray.GetValue(j);
            //                 // staff.stafflist[i].Job = staff.worklist[j];
            //                 Console.WriteLine(stafflist[i].Name + " har just nu arbetsuppgiften " + stafflist[i].CurrentJob);// skriv ut
            //                 staff.PrintStaff(stafflist);
            //                 break;
            //             }

            //         }

            //     }

            // }
        

                        //Product prod = new Product(name, price, Product.ProductType, Product.VatRate._12);


    
   



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