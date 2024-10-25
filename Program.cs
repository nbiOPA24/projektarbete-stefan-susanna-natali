class Program
// TODO kolla metod för ? 
{
    
        static void Main(string[] args)
        {   
            
            User user = new User();
            //Product product = new();
            //UserHandler.AddUser(user);
            // UserHandler.PrintUser(UserHandler.userList);
            // UserHandler.AddUser(user);
            // Console.WriteLine("Uppdaterad lista: ");
            // UserHandler.PrintUser(UserHandler.userList);
            // UserHandler.RemoveUser(user);
            // UserHandler.PrintUser(UserHandler.userList);
            Product product = new Product("Carbonara",95.50, Product.ProductType.Food, Product.VatRate._12);
            
            ProductHandler.AddProduct(product);
            //ProductHandler.PrintProduct();
            // UserHandler.AddUser(user);
            // UserHandler.RemoveUser(user);
            // UserHandler.PrintUser(UserHandler.userList);
            // List<User>userList = new ();
            // List<Report> saleslist =  new List<Report>(); 
            // List<Table> tablelist = new();
            //ProductHandler.productList.Add(product);
                               
            //Console.WriteLine(product.Name);
            //Console.WriteLine(product.Price);
            //Console.WriteLine(product.MenuItem);
            //ProductHandler.Add("Pripps", 55, product.MenuItem);
            
            // while (true)
            // {
                //Console.WriteLine("1. lägg till");
                //Console.WriteLine("2. printa");
                //Console.WriteLine("3. avsluta");
                // string? choice = Console.ReadLine();
                // switch(choice)
                // {
                //     case "1":
                //     {
                        //Console.WriteLine("Ange produktnamn");
                        //string? name = Console.ReadLine();
                        //Console.WriteLine("Ange pris");
                        //double? price = double.Parse(Console.ReadLine());
                        //Console.WriteLine("välj produkttyp");

                        //Konvertera till array:  
                        //var jobArray = Enum.GetValues(typeof(Staff.Jobs));
                        // FOREACHLOOP
                        //              foreach (Staff.Jobs job in Enum.GetValues(typeof(Staff.Jobs)))
                        // {
                        //     Console.WriteLine((int)job + " " + job); // int skriver ut indexet
                        // }
        

                        //Product prod = new Product(name, price, Product.ProductType, Product.VatRate._12);
            //             break;
            //         }
            //         case "2":
            //         {
            //             //newProduct.Print();
            //             break;
            //         }
            //         case "3":
            //         {
            //             return;
            //         }
            //     }   
                    
            // }
        }

}