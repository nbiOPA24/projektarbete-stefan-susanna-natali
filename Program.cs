class Program
//TODO Fixa json
//TODO spectra
//TODO få reports att fungera
//TODO skriva ut orderlist på kvittot
{

    static void Main(string[] args)
    {
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

        Receipt newReceipt = new();
        Table table = new(number, status, size);

        //UserHandler.UserStartMenu();
        UserInterFace.UserInterFaceStartMenu(newReceipt, tableHandler, number, status, size, table, user);



    }
}

//Spectre för konsol