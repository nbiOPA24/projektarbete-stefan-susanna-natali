
class Program

{
    
        static void Main(string[] args)
        {
                List<User>userList = new ();
                List<Report> saleslist =  new(); 
                List<Table> tablelist = new();
                
               Product product = new Product("Carbonara",95.50, Product.ProductType.Food);
               ProductHandler.productList.Add(product);
               Console.WriteLine(product.Name);
               Console.WriteLine(product.Price);
               Console.WriteLine(product.MenuItem);
                //ProductHandler.Add("Pripps", 55, product.MenuItem);
                
        }

}


