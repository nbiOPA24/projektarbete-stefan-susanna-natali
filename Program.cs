class Program
//TODO Fixa json
//TODO spectra
//TODO få reports att fungera
//TODO få ut orderlist när man skriver ut kvitto
//TODO felhantering!
//TODO lägg till att man kan ändra UserType
//TODO ska inte gå att ändra till ett upptaget 

{

    static void Main(string[] args)
    {


        Console.Clear();
        int number = 0;
        bool status = false;
        int size = 0;
        int receiptNumber = 0;
        DateTime nu = DateTime.Now;
        //Data.LoadUserList("user.json");

        User user = new();

        TableHandler tableHandler = new();
        // TableHandler.GenerateTables();
        Receipt receipt = new();
        // Product product = new("Risotto", 120, Product.ProductType.Food);
        // UserInterFace.orderList.Add(product);
        
        // User user01 = new(User.TypeOfUser.Manager, "Natali");
        // User user02 = new(User.TypeOfUser.Manager, "Natalie");
        // UserHandler.userList.Add(user01);
        // UserHandler.userList.Add(user02);
        // ProductHandler.productList.Add(product);
        //Receipt receipt = new();
        Table table = new(number, status, size);
        //ReportHandler.DailyReport(receipt);
        Data.LoadReceiptList("receipt.json");
        Data.LoadNextReceiptNumber("nextreceiptnumber.json");
        // Console.WriteLine("Receiptnumber: " +receipt.ReceiptNumber);
        // Console.WriteLine("Siffra: " +receipt.siffra);
        // Console.WriteLine("NextNumber: " +Receipt.nextNumber);
        //ReportHandler.DailyReport();
        //ProductHandler.ProductStartMenu();
        UserInterFace.UserInterFaceStartMenu(receipt, tableHandler, number, status, size, table, user);

    

    }
}

//Spectre för konsol