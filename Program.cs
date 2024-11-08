﻿class Program

{

    static void Main(string[] args)
    {
        //Console.Clear();

        Product product = new();
        User user = new(User.TypeOfUser.Admin, "Natali");
        TableHandler tableHandler = new();
        int number = 0;
        bool status = false;
        int size = 0;
        Product product1 = new("Risotto", 120, Product.ProductType.Food, Product.VatRate._12);
        Product product2 = new("Carbonara", 100, Product.ProductType.Food, Product.VatRate._12);
        Product product3 = new("Carlsberg", 60, Product.ProductType.Alcohol, Product.VatRate._25);

        ProductHandler.productList.Add(product1);
        ProductHandler.productList.Add(product2);
        ProductHandler.productList.Add(product3);

        TableHandler.TestTables();

        while (true)
        {
            UserInterFace.UserInterFaceStartMenu(product, tableHandler, user, number, status, size);
        }
    }
}