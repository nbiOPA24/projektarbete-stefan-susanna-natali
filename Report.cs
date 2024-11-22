using System.Collections;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;


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
    public static List<Receipt> reportReceiptList = new(); 
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
            reportReceiptList.Add(r);
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

    public static void UserSales()   //försäljning per användare
    {
        List<User>userList = new(); //
        Data.LoadUserList("user.json"); //visa personal-listan
        
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Välj personal enligt ID: ");
        foreach (User u in UserHandler.userList)
        {
            Console.WriteLine($"\nNamn: {u.FirstName}");
            Console.WriteLine($"Personal-ID: {u.UserId}");
            Console.WriteLine($"Roll: {u.UserType}\n");
        }

            Console.Write("Ange personal-ID (4 siffror): ");
            string userIdInput = Console.ReadLine();
            User selectedId = null;  //för att komma åt namnet på vald personal via ID utanför if-satsen

            if(int.TryParse(userIdInput, out int userId)) //gör om id-numret till string
            {
                selectedId = UserHandler.userList.FirstOrDefault(user=> user.UserId == userId);
            
                if(selectedId != null)
                {
                    Console.WriteLine($"Du valde: {selectedId.FirstName} ");
                }
                else
                {
                    Console.WriteLine("Ingen användare med detta Id. Försök igen.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Ingen användare med detta Id. Försök igen.");
                return;
            }
                Console.WriteLine("Inputta startdatum för rapport: ");
                Report.GetDate("YYYY-MM-DD", out DateTime startDate);
                Console.WriteLine("Inputta sluttdatum för rapport: ");
                Report.GetDate("YYYY-MM-DD", out DateTime endDate);

                double userSum = 0;
                foreach (Receipt r in Payment.receiptList)      //försöker gå igenom listorna av kvitton med summa och antal sålda produkter per user inom ett datum-spann
                {
                    reportReceiptList.Add(r);
                    userSum += r.PaidAmount;
                }

            Console.WriteLine($"Försäljning för {selectedId.FirstName} är {userSum} kr.\n"); //total försäljning för vald personal inom valt datum-spann
    }

    public static void GetUserSales(){} //behövs denna?

    public static void GetSoldProducts()
    {
        Data.LoadReceiptList("receipt.json");
        foreach (Product p in UserInterFace.orderList)
        {
            reportProductList.Add(p);
        }
    }
    // public static void PrintSoldProducts() //väntar på produkt-listan
    // {
    //     List<Product>reportProductList = new();//
    //     Data.LoadProductList("product.json"); //visa produkt-listan

    //     Console.WriteLine("Inputta startdatum för rapport: ");
    //     Report.GetDate("YYYY-MM-DD", out DateTime startDate);
    //     Console.WriteLine("Inputta sluttdatum för rapport: ");
    //     Report.GetDate("YYYY-MM-DD", out DateTime endDate);

    //     Dictionary<string, int> productSummary = new();
    //     Dictionary<string, int> productSum = new();

    //     // Söker upp alla matchande produkter och räknar
    //     Console.WriteLine("Total lista av sålda produkter: \n ");
    //     foreach(Product p in reportProductList)
    //     {
    //         //foreach (Receipt r in Payment.receiptList)
            
    //             if (productSummary.ContainsKey(p.Name)) // kollar matchande p.Name
    //             {
    //                 productSummary[p.Name]++; // Räknar antal träffar av samma name
    //             }
    //             else
    //             {
    //                 productSummary[p.Name] = 1; // om bara en träff så = 1
    //             }
    //         }
    //         foreach (var p in productSum)
    //         {
    //             Console.WriteLine($"{p.Key} {p.Value} st");

    //             if (productSummary.Count < 1)                       // funkar denna ens?
    //             {
    //                 Console.WriteLine("Inga produkter sålda."); 
    //             }
    //         }
    //     }

    public static void FlashReport() //en flash-rapport som ger dagens försäljning från start of play -> just nu
    {
        double flashSum =0;
        DateTime today = DateTime.Now;
        DateTime startOfDay = today.Date;

        
        foreach (Receipt r in Payment.receiptList)
        {
            if (r.PaymentAccepted.Date == today.Date)
            {
                flashSum += r.PaidAmount;
            }
        }
    
    Console.WriteLine($"--------------------------------------------");
    Console.WriteLine($"Datum och tid är:{today: yyyy-MM-dd HH:mm:ss}");
    Console.WriteLine($"Försäljning: {flashSum} kr");
    Console.WriteLine($"--------------------------------------------");
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
        Console.WriteLine($"Försäljning för {date:yyyy-MM-dd} är: {dailySum} kr");
        Console.WriteLine($"--------------------------------------------");
    }

    public static void CustomReport() //en rapport med valfria datum
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

        Console.WriteLine($"--------------------------------------------");
        Console.WriteLine($"Datum: {startDate:yyyy-MM-dd} - {endDate:yyyy-MM-dd}"); 
        Console.WriteLine($"Försäljning: {weeklySum} kr ");
        Console.WriteLine($"--------------------------------------------");
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
    public static void ReportMenu() //metod att kalla på i UserInterFace huvudmeny
    {

        Console.WriteLine("Välj vilken Rapport du vill generera: ");
        Console.WriteLine("Tryck 1. För TotalReport: ");
        Console.WriteLine("Tryck 2. För X-FlashReport: ");
        Console.WriteLine("Tryck 3. För DailyReport: ");
        Console.WriteLine("Tryck 4. För CustomReport: ");
        /*Console.WriteLine("Tryck 5. För ProductReport: ");*/ //kan inte användas just nu eftersom produkt inte sparas i kvitto-listan
        Console.WriteLine("Tryck 6. För UserReport: ");
          /*Console.WriteLine("Tryck 7. För TableReport: ");
          Console.WriteLine("Tryck 8. För TipsReport: "); */
        Console.WriteLine("Tryck Q. För att avsluta till huvudmenyn: ");

        string? reportChoice = Console.ReadLine();

        switch (reportChoice)
        {
            case "1":
                ReportHandler.TotalReport();
                break;

            case "2":
                ReportHandler.FlashReport();
                break;

            case "3":
                ReportHandler.DailyReport();
                break;

            case "4":
                ReportHandler.CustomReport();
                break;

            case "5":
                ReportHandler.GetSoldProducts();
                //ReportHandler.PrintSoldProducts();
                Console.WriteLine("Rapport under konstruktion. ");
                break;

            case "6":
                ReportHandler.UserSales();              
                break;

            default:
                break;
        }

    }

}