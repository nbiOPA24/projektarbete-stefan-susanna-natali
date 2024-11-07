using System.Security.Cryptography;

class Program
// TODO kolla metod för ? 
{
    
        static void Main(string[] args)
        {   
            
            //User user = new User();
            //Product product = new();
            //UserHandler.AddUser(user);
            //UserHandler.PrintUser(UserHandler.userList);
            //UserHandler.ModifyUser();
            //Console.WriteLine("Här är listan uppdaterad: ");
            //UserHandler.PrintUser(UserHandler.userList);
            // Console.WriteLine("Uppdaterad lista: ");
            // UserHandler.PrintUser(UserHandler.userList);
            // UserHandler.RemoveUser(user);
            // UserHandler.PrintUser(UserHandler.userList);
            Product product = new Product("Carbonara",95.50, Product.ProductType.Food, Product.VatRate._12);
            Report report = new Report();

            //ProductHandler.AddProduct(product);
            //ProductHandler.PrintProduct();
            // UserHandler.AddUser(user);
            // UserHandler.RemoveUser(user);
            // UserHandler.PrintUser(UserHandler.userList);
            // List<User>userList = new ();
            List<Report> SalesList =  new List<Report>(); 
            ReportHandler.SalesList = new List<Report>();
            // List<Table> tablelist = new();
            //ProductHandler.productList.Add(product);
                               
            //Console.WriteLine(product.Name);
            //Console.WriteLine(product.Price);
            //Console.WriteLine(product.MenuItem);
            //ProductHandler.Add("Pripps", 55, product.MenuItem);

            bool isRunning = true;
            while (isRunning)
            {
            Console.WriteLine("Välkommen till HoReCa Rapport- & Försäljnings-hanteringsmeny.");
            Console.WriteLine("Var god välj 1 eller 2: ");
            Console.WriteLine("Välj 1. För att välja Rapport-Kategori: ");
            Console.WriteLine("Välj 2. För att avsluta: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "2":
                isRunning = false;
                break;

                case "1":
                    Console.WriteLine("Var god välj vilken Rapport du vill generera: ");
                    Console.WriteLine("Tryck 1. För TotalSales: ");
                    Console.WriteLine("Tryck 2. För WeeklySales: ");
                    Console.WriteLine("Tryck 3. För DailySales: ");
                    Console.WriteLine("Tryck 4. För att avsluta: ");
                    string? reportChoice = Console.ReadLine();
                    string reportCategoryName = reportChoice 
                    switch  //Added dynamics to menu thru string interpolation in line 77 & 78
                    {
                        "1" => "TotalSales",   //TODO ändra denna meny - dra index från enum i 'Report' ist?
                        "2" => "WeeklySales",
                        "3" => "DailySales",
                        _   => "Avslutar"
                    };
                    if (reportCategoryName == "Avslutar")
                        {
                            Console.WriteLine("Avslutar. Välkommen åter!");
                            return;
                        }

                    DateTime startDate, endDate;
                    if (!Report.GetDate($"Inputta startdatum för {reportCategoryName} (YYYY-MM-DD):", out startDate) || //Done! justerat för dynamik från user-input
                        !Report.GetDate($"Inputta slutdatum för {reportCategoryName} (YYYY-MM-DD): ", out endDate))     // TODO Lägg in exception för inkorrekt datum-format enligt mall YYYY-MM-DD
                    {
                        Console.WriteLine("Ogiltig input. Försök igen (YYYY-MM-DD).");
                        return;
                    }
                

                    switch (reportChoice)
                    {
                        case "1":
                            decimal TotalSales = ReportHandler.ReportGenerator(Report.ReportCategory.TotalSales, startDate, endDate);
                            Console.WriteLine($"Total försäljning för {reportCategoryName} är: {TotalSales}");
                            break;
                        case "2":
                            decimal WeeklySales = ReportHandler.ReportGenerator(Report.ReportCategory.WeeklySales, startDate, endDate);
                            Console.WriteLine($"Total försäljning för {reportCategoryName} är: {WeeklySales}");
                            break;
                        case "3":
                            decimal DailySales = ReportHandler.ReportGenerator(Report.ReportCategory.DailySales, startDate, endDate);
                            Console.WriteLine($"Total försäljning för {reportCategoryName} är: {DailySales}");
                            break;
                        case "4":
                            Console.WriteLine("Avslutar.");
                            return;
                        default:
                            Console.WriteLine("Ogiltigt val, försök igen: ");
                            break;
                    }
                break;
            
                default:
                    Console.WriteLine("Ogiltigt val, försök igen: ");
                break;
            }
            break;
    
        
            }

            
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
        

                        //Product prod = new Product(name, price, Product.ProductCategory, Product.VatRate._12);
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
