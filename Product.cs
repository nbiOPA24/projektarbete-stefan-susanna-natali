public class Product
{   
    public enum ProductType // förrätt, 
    {
        Food,
        Drinks

    }
    public string Name {get ; set ;}
    public double Price {get ; set ;}
    public ProductType MenuItem {get ; set ;}

    public Product (string name, double price, ProductType menuItem)
    {
        Name = name;
        Price = price;
        MenuItem = menuItem;
        
    }
}
public static class ProductHandler
{
    public static List<Product> productList {get ; set ;} =new();


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