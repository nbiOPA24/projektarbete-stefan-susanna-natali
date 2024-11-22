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
        // int number = 0;
        // bool status = false;
        // int size = 0;
        // int receiptNumber = 0;
        // DateTime nu = DateTime.Now;
        //Data.LoadUserList("user.json");

        //User user = new();

        // TableHandler tableHandler = new();
        // // TableHandler.GenerateTables();
        // Receipt receipt = new();
        // Product product = new("Risotto", 120, Product.ProductType.Food);
        // UserInterFace.orderList.Add(product);

        // User user01 = new(User.TypeOfUser.Manager, "Steffe");
        // User user02 = new(User.TypeOfUser.Manager, "Natalie");
        // UserHandler.userList.Add(user01);
        // UserHandler.userList.Add(user02);
        // ProductHandler.productList.Add(product);
        Receipt receipt = new();
        // Table table = new(number, status, size);
        //ReportHandler.DailyReport(receipt);
        Table table01 = new(1, false, 4);
        Table table02 = new(2, false, 4);
        Table table03 = new(3, false, 4);
        Table table04 = new(4, false, 4);
        Table table05 = new(5, false, 4);
        TableHandler.tables.Add(table01);
        TableHandler.tables.Add(table02);
        TableHandler.tables.Add(table03);
        TableHandler.tables.Add(table04);
        TableHandler.tables.Add(table05);

        Data.LoadReceiptList("receipt.json");
        Data.LoadNextReceiptNumber("nextreceiptnumber.json");
        UserInterFace.UserInterFaceStartMenu(receipt);



    }
}

//Spectre för konsol