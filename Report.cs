using System.Collections;
using System.Globalization;


public class Report

{
    public double PaidAmount;
    public DateTime Date;
    public string Product;
    public string User;

    public static bool GetDate(string prompt, out DateTime date)
    {
        Console.WriteLine(prompt);
        string? dateInput = Console.ReadLine();
        return DateTime.TryParseExact(dateInput, "yyyy-MM-dd",
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.None, out date);

    }


    public Report(double paidamount, DateTime date, string product, string user)
    {
        PaidAmount = paidamount;
        Date = date;
        Product = product;
        User = user;
    }
}
public static class ReportHandler
{
    public static List<Receipt> reportList = new(); 
    public static List<Product> reportProductList = new();
    public static List<User> reportUserList = new();
    public static Dictionary<string, (double TotalSales, int ProductSold, double ProductNameAmount)> userReport = new(); //ProductNameAmount är 

    public static void TotalReport()
    {
        double totalSum = 0;
        Dictionary<string, int> productReport = new();
        Dictionary<string, double> userReport = new();

        foreach (Receipt r in Payment.receiptList)
        {
            reportList.Add(r);
            totalSum += r.PaidAmount;

            foreach (Product p in ProductHandler.productList)
            //foreach (Product p in Receipt.paidProductList)
            {
                //reportList.Add(p);
                //totalSum += p.PaidAmount;
            }


        }
        Console.WriteLine($"--------------------------------------------");
        Console.WriteLine($"Rapport för total försäljning: {totalSum} kr");
        Console.WriteLine($"Lista av sålda produkter: {reportProductList.Count} st");
        Console.WriteLine($"Total av sålda produkter: {reportProductList.Count} st"); //här ska det vara en lista av produkt-namn + antal sålda
        Console.WriteLine($"--------------------------------------------");
        //TODO totalt antal sålda produkter (st)

    }

    public static void GetSoldProducts()
    {
        foreach (Product p in UserInterFace.orderList)
        {
            reportProductList.Add(p);

        }

    }

    public static void UserSales()   //försäljning per användare
    {
        double userSum = 0;
        
        Console.WriteLine("Inputta startdatum");
        Report.GetDate("YYYY-MM-DD", out DateTime startDate);
        Console.WriteLine("Inputta sluttdatum");
        Report.GetDate("YYYY-MM-DD", out DateTime endDate);

        foreach (Receipt r in Payment.receiptList)      //försöker gå igenom listorna av kvitton med summa och antal sålda produkter per user inom ett datum-spann
        {
            reportList.Add(r);
            userSum += r.PaidAmount;

            string FirstName = r.CurrentFirstName;
            if (!userReport.ContainsKey(FirstName))
            {
                userReport[FirstName] = (0, 0, 0);
            }
            var currentData = userReport[FirstName];
            currentData.TotalSales += r.PaidAmount;

         /*   foreach (Product p in r.Product.receiptList)
            {
            currentData.ProductSold ++;
            currentData.ProductNameAmount ++;
            userReport[FirstName] = currentData;
            }*/
        }
    }

    public static void GetUserSales()    //en metod att kalla på i report-menu
    {
        foreach(var user in userReport)
        {
            Console.WriteLine($"--------------------------------------------");
            Console.WriteLine($"{user.Key} total försäljning: {user.Value.TotalSales} kr");
            Console.WriteLine($"{user.Key} total försäljning: {user.Value.ProductNameAmount}"); //minns inte vad ProductNameAmount skulle va? Lista på vilka produkter??
            Console.WriteLine($"{user.Key} antal produkter: {user.Value.ProductSold} st");
            Console.WriteLine($"--------------------------------------------");
        }
    }

    public static void PrintSoldProducts()
    {
        Dictionary<string, int> productSum = new();
        // Söker upp alla matchande produkter och räknar
        foreach (Product p in reportProductList)
        {
            if (productSum.ContainsKey(p.Name)) // kollar matchande p.Name
            {
                productSum[p.Name]++; // Räknar antal träffar av samma name
            }
            else
            {
                productSum[p.Name] = 1; // om bara en träff så = 1
            }
        }
        foreach (var p in productSum)
        {
            Console.WriteLine($"{p.Key} {p.Value} st");

            if (productSum.Count < 1)                       // funkar denna?
            {
                Console.WriteLine("Inga produkter sålda."); 
            }
        }
    }

    public static void DailyReport()
    {
        double dailySum = 0;
        Report.GetDate("Ange datum: ", out DateTime date);
        foreach (Receipt r in Payment.receiptList)
        {
            if (r.PaymentAccepted.Date.Date == date.Date)
            {
                dailySum += r.PaidAmount;
            }

            //DailyReport(date);
        }
        Console.WriteLine($"--------------------------------------------");
        Console.WriteLine($"Försäljning för {date} är: {dailySum}"); //TODO ta bort klockslag
        Console.WriteLine($"--------------------------------------------");
    }

    public static void CustomReport()
    {
        double weeklySum = 0;
        Report.GetDate("Ange startdatum (YYYY-MM-DD): ", out DateTime startDate);
        Report.GetDate("Ange slutdatum (YYYY-MM-DD): ", out DateTime endDate);
        foreach (Receipt r in Payment.receiptList)
        {
            if (r.PaymentAccepted.Date >= startDate && r.PaymentAccepted.Date <= endDate)
            {
                weeklySum += r.PaidAmount;
            }
        }
    }

    // lista och summa på TUTTI: sales, user, product, VAT, fritt datum-spann
    //     public void WeeklyReport () {}// lista och summa på sales, välj vecka enligt 7 dagar
    //     public void DailyReport () {}//lista och summa på sales, dagens datum
    //     public void SalesReport () {}//lista och summa på sales, fritt datum-spann
    //     public void UserReport () {}// lista och summa på sales, per user, fritt datum-spann
    //     public void TableReport () {}// lista och summa på sales per bord, per user, fritt datum-spann
    //     public void ProductReport() {}// lista och summa på antal sales per produkter per bord, per user, fritt datum-spann
    //     public void TipsReport() {}// lista och summa på dricks per bord, per user, fritt datum-spann
    //     {
    //         public double Tips { get; set; }
    //     }
    public static void ReportMenu() //metod att kalla på i UserInterFace
    {

        Console.WriteLine("Var god välj vilken Rapport du vill generera: ");
        Console.WriteLine("Tryck 1. För TotalReport: ");
        Console.WriteLine("Tryck 2. För DailyReport: ");
        Console.WriteLine("Tryck 3. För CustomReport: ");
        Console.WriteLine("Tryck 4. För ProductReport: ");
        Console.WriteLine("Tryck 5. För UserReport: ");
          /*Console.WriteLine("Tryck 3. För TableReport: ");
          Console.WriteLine("Tryck 3. För TipsReport: "); */
        Console.WriteLine("Tryck Q. För att avsluta till huvudmenyn: ");

        string? reportChoice = Console.ReadLine();

        switch (reportChoice)
        {
            case "1":
                ReportHandler.TotalReport();
                break;

            case "3":
                ReportHandler.CustomReport();
                break;

            case "4":
                ReportHandler.GetSoldProducts();
                ReportHandler.PrintSoldProducts();
                break;

            case "5":
                ReportHandler.GetUserSales();
                break;

            default:
                break;
        }

    }
    // if (reportCategory == Report.ReportCategory.Receipt)
    // {
    //     Console.WriteLine("Tillbaka till huvudmenyn. Välkommen åter!");
    //     return;
    // }
    /* if (!Report.GetDate($"Ange startdatum för rapporten (YYYY-MM-DD):", out DateTime startDate) ||
         !Report.GetDate($"Ange slutdatum för rapporten (YYYY-MM-DD): ", out DateTime endDate))
     {
         Console.WriteLine("Ogiltig input. Försök igen (YYYY-MM-DD).");
         return;
     }*/
}