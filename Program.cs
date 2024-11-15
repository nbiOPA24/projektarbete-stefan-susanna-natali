class Program
//TODO Fixa json
//TODO spectra
//TODO få reports att fungera
{

    static void Main(string[] args)
    {
        //Console.Clear();
        int number = 0;
        bool status = false;
        int size = 0;
        DateTime nu = DateTime.Now;


        TableHandler tableHandler = new();
        TableHandler.GenerateTables();
        User user = new(User.TypeOfUser.Bartender, "Natali");
        User user01 = new(User.TypeOfUser.Manager, "Stefan");
        User user02 = new(User.TypeOfUser.Manager, "Susanna");
        Product product1 = new("Risotto", 120, Product.ProductType.Food);
        Product product2 = new("Carbonara", 100, Product.ProductType.Food);
        Product product3 = new("Carlsberg", 60, Product.ProductType.Alcohol);

        ProductHandler.productList.Add(product1);
        ProductHandler.productList.Add(product2);
        ProductHandler.productList.Add(product3);
        UserHandler.userList.Add(user);
        UserHandler.userList.Add(user01);
        UserHandler.userList.Add(user02);

        //Testkör rapport-funktionen i konsollen:
        ProductHandler.PrintProduct();
        var report = new Report
        {
            ReportNumber = 1,
            Date = DateTime.Today,
            Category = Report.ReportCategory.DailySales
        };



        report.AddSale(ProductHandler.productList[0], 5, new DateTime(2023, 1, 1));
        report.AddSale(ProductHandler.productList[1], 5, new DateTime(2023, 5, 10));
        report.AddSale(ProductHandler.productList[2], 5, new DateTime(2023, 8, 31));
        // report.AddSale(ProductHandler.productList[8], 5, new DateTime(2023, 10, 23));
        // report.AddSale(ProductHandler.productList[9], 5, new DateTime(2023, 11, 5));

        // report.AddSale(ProductHandler.productList[3], 5, new DateTime(2024, 10, 1));
        // report.AddSale(ProductHandler.productList[4], 5, new DateTime(2024, 10, 2));
        // report.AddSale(ProductHandler.productList[5], 5, new DateTime(2024, 10, 3));
        // report.AddSale(ProductHandler.productList[6], 5, new DateTime(2024, 10, 4));
        // report.AddSale(ProductHandler.productList[7], 5, new DateTime(2024, 10, 4));


        // report.salesList.Add(salesReport);

        DateTime startDate = DateTime.Today.AddDays(-1);
        DateTime endDate = DateTime.Today;
        decimal totalSalesAmount = ReportHandler.ReportGenerator(Report.ReportCategory.TotalSales, startDate, endDate);

        Console.WriteLine($"\nTotal försäljning för {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}: {totalSalesAmount:C}");
        Receipt newReceipt = new();
        Table table = new(number, status, size);

        UserHandler.UserStartMenu();
        //UserInterFace.UserInterFaceStartMenu(newReceipt, tableHandler, number, status, size, table, user);



    }
}

//Spectre för konsol