﻿using System.Security.Cryptography;
using System.Globalization;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using System.IO;

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
        
#if !DEBUG
    Console.Clear();
#endif
        int number = 0;
        bool status = false;
        int size = 0;
        DateTime nu = DateTime.Now;
        Data.LoadUserList("user.json");

        User user = new();

        TableHandler tableHandler = new();
        TableHandler.GenerateTables();
        
        // Product product1 = new("Risotto", 120, Product.ProductType.Food);
        // Product product2 = new("Carbonara", 100, Product.ProductType.Food);
        // Product product3 = new("Carlsberg", 60, Product.ProductType.Alcohol);

        // ProductHandler.productList.Add(product1);
        // ProductHandler.productList.Add(product2);
        // ProductHandler.productList.Add(product3);
        // // UserHandler.userList.Add(user);
        // // UserHandler.userList.Add(user01);
        // // UserHandler.userList.Add(user02);

        // //Testkör rapport-funktionen i konsollen:
        // //ProductHandler.PrintProduct();
        // var report = new Report
        // {
        //     ReportNumber = 1,
        //     Date = DateTime.Today,
        //     Category = Report.ReportCategory.DailySales
        // };



        // report.AddSale(ProductHandler.productList[0], 5, new DateTime(2023, 1, 1));
        // report.AddSale(ProductHandler.productList[1], 5, new DateTime(2023, 5, 10));
        // report.AddSale(ProductHandler.productList[2], 5, new DateTime(2023, 8, 31));

        // DateTime startDate = DateTime.Today.AddDays(-1);
        // DateTime endDate = DateTime.Today;
        // decimal totalSalesAmount = ReportHandler.ReportGenerator(Report.ReportCategory.TotalSales, startDate, endDate);

        // Console.WriteLine($"\nTotal försäljning för {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}: {totalSalesAmount:C}");
        Receipt newReceipt = new();
        Table table = new(number, status, size);

        
        UserInterFace.UserInterFaceStartMenu(newReceipt, tableHandler, number, status, size, table, user);



    }
}

//Spectre för konsol