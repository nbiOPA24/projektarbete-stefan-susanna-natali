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
            List<Product>productList = new List<Product>
            {
            /*Product product =*/ 
            new Product("Carbonara",95.50, Product.ProductType.Food, Product.VatRate._12),
            new Product("Hawaii-pizza",105, Product.ProductType.Food, Product.VatRate._12),
            new Product("Mozzarella sticks",75, Product.ProductType.Food, Product.VatRate._12),
            new Product("Boquerones",45, Product.ProductType.Food, Product.VatRate._12),
            new Product("Marängsviss",55, Product.ProductType.Food, Product.VatRate._12),

            new Product("Karma Cola",25, Product.ProductType.Food, Product.VatRate._12),
            new Product("Dubbel espresso",25, Product.ProductType.Food, Product.VatRate._12),

            new Product("Pripps",55, Product.ProductType.Drinks, Product.VatRate._25),
            new Product("Pinot Grigio 125ml",75, Product.ProductType.Drinks, Product.VatRate._25),
            new Product("Päroncider 5%",45, Product.ProductType.Drinks, Product.VatRate._25),
            };

            Report salesReport = new Report {ReportNumber = 1, Date = DateTime.Now, Category = Report.ReportCategory.TotalSales};
            //salesReport.AddSale(product, 5, DateTime.Now);

            //ProductHandler.AddProduct(product);
            //ProductHandler.PrintProduct();
            // UserHandler.AddUser(user);
            // UserHandler.RemoveUser(user);
            // UserHandler.PrintUser(UserHandler.userList);
            // List<User>userList = new ();
            //List<Sales>salesData = new List<Sales>
            
                salesReport.AddSale(productList[0], 5, new DateTime(2023, 1, 1));
                salesReport.AddSale(productList[1], 5, new DateTime(2023, 5, 10));
                salesReport.AddSale(productList[2], 5, new DateTime(2023, 8, 31));
                salesReport.AddSale(productList[8], 5, new DateTime(2023, 10, 23));
                salesReport.AddSale(productList[9], 5, new DateTime(2023, 11, 5));

                salesReport.AddSale(productList[3], 5, new DateTime(2024, 10, 1));
                salesReport.AddSale(productList[4], 5, new DateTime(2024, 10, 2));
                salesReport.AddSale(productList[5], 5, new DateTime(2024, 10, 3));
                salesReport.AddSale(productList[6], 5, new DateTime(2024, 10, 4));
                salesReport.AddSale(productList[7], 5, new DateTime(2024, 10, 4));
            
            List<Report> SalesList =  new List<Report>(); 
            ReportHandler.SalesList = new List<Report>();
            ReportHandler.SalesList.Add(salesReport);
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
                    Report.ReportCategory reportCategory = reportChoice 
                    switch  //Added dynamics to menu thru string interpolation in line 77 & 78
                    {
                        "1" => Report.ReportCategory.TotalSales,   //TODO ändra denna meny - dra index från enum i 'Report' ist?
                        "2" => Report.ReportCategory.WeeklySales,
                        "3" => Report.ReportCategory.DailySales,
                        "4" => Report.ReportCategory.PrintReceipt,
                        _   => Report.ReportCategory.TotalSales
                    };
                    if (reportCategory == Report.ReportCategory.PrintReceipt)
                        {
                            Console.WriteLine("Avslutar. Välkommen åter!");
                            break;
                        }

                    if (!Report.GetDate($"Ange startdatum för rapporten (YYYY-MM-DD):", out DateTime startDate) || //Done! justerat för dynamik från user-input
                        !Report.GetDate($"Ange slutdatum för rapporten (YYYY-MM-DD): ", out DateTime endDate))     // TODO Lägg in exception för inkorrekt datum-format enligt mall YYYY-MM-DD
                    {
                        Console.WriteLine("Ogiltig input. Försök igen (YYYY-MM-DD).");
                        continue;
                    }
                
            //Ändrade logik för Reportgenerator-input. Streamlineade för enklare output av rapport-kategori

                        decimal reportTotal = ReportHandler.ReportGenerator(reportCategory, startDate, endDate);
                        Console.WriteLine($"Total försäljning för {reportCategory} är: {reportTotal: C}");
                        
                        Console.WriteLine("Vill du generera en annan rapport? (y/n)");
                        string? anotherReport = Console.ReadLine();
                        if (anotherReport?.ToLower() != "y")
                        {
                            Console.WriteLine("Avslutar. Välkommen åter!");
                            isRunning = false;
                        }
                        break;
                    default:
                        Console.WriteLine("Ogiltigt val, försök igen!");
                        break;
                        
            }
    
        
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
