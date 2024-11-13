class Program

{

    static void Main(string[] args)
    {
        Console.Clear();
        int number = 0;
        bool status = false;
        int size = 0;


        TableHandler tableHandler = new();
        TableHandler.GenerateTables();
        User user = new(User.TypeOfUser.Admin, "Natali");
        User user01 = new(User.TypeOfUser.Bartender, "Simon");
        Product product1 = new("Risotto", 120, Product.ProductType.Food);
        Product product2 = new("Carbonara", 100, Product.ProductType.Food);
        Product product3 = new("Carlsberg", 60, Product.ProductType.Alcohol);

        ProductHandler.productList.Add(product1);
        ProductHandler.productList.Add(product2);
        ProductHandler.productList.Add(product3);
        UserHandler.userList.Add(user);
        UserHandler.userList.Add(user01);
        Table table = new(number, status, size);

        ProductHandler.PrintProduct();
        

        // while (true)
        // {
        //     UserInterFace.UserInterFaceStartMenu(tableHandler, number, status, size, table);
        // }

    }
}

//Spectre för konsol