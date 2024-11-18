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
    

    public Report (double paidamount, DateTime date, string product, string user)
    {
        PaidAmount = paidamount;
        Date=date;
        Product=product;
        User=user;
    }
}

public class TotalReport : Report {} // lista och summa på TUTTI: sales, user, product, VAT, fritt datum-spann
/*public class WeeklyReport : Report {} // lista och summa på sales, välj vecka enligt 7 dagar
public class DailyReport : Report {} //lista och summa på sales, dagens datum
public class SalesReport : Report {} //lista och summa på sales, fritt datum-spann
public class UserReport : Report {} // lista och summa på sales, per user, fritt datum-spann
public class TableReport : Report {} // lista och summa på sales per bord, per user, fritt datum-spann
public class ProductReport : Report {} // lista och summa på antal sales per produkter per bord, per user, fritt datum-spann

public class TipsReport : Report // lista och summa på dricks per bord, per user, fritt datum-spann
    {
        public double Tips {get; set;}
    } 
    */

//varje ärvd klass ovan innehåller en egen unik metod?? för att generera rapport? eller en generisk metod??
//en generisk metod med anpassad datum-logik??
public static List<Report> ReportList { get; set; } = new();    


public static decimal ReportGenerator (DateTime startDate, DateTime endDate)
{
        return ReportList
        .Where(report => report.Date >= startDate && report.Date <= endDate)//sorterar efter valt datum-spann
        .Sum(report => report.salesList.Sum(sale => sale.TotalAmount)); //ser till att få en rapport-kategori i taget att funka
}
    public static decimal TotalReport(DateTime startDate, DateTime endDate)
    {
        return ReportList
        .Where(report => report.Date >= startDate && report.Date <= endDate)//sorterar efter valt datum-spann
        .Sum(report => report.salesList.Sum(sale => sale.TotalAmount)); //ser till att få en rapport-kategori i taget att funka
    }
    public static decimal WeeklyReport(DateTime startDate, DateTime endDate)
    {
        return ReportList
        .Where(report => report.Date >= startDate && report.Date <= endDate)
        .SelectMany(report => report.salesList)
        .GroupBy(sale => CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(sale.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
        .Sum(group => group.Sum(sale => sale.TotalAmount));
    }
    public static decimal DailyReport(DateTime startDate, DateTime endDate)
    {
        return ReportList
        .Where(report => report.Date >= startDate && report.Date <= endDate)
        .SelectMany(report => report.salesList)
        .GroupBy(sale => sale.Date.Date)
        .Sum(group => group.Sum(sale => sale.TotalAmount));
    }

    //här vill jag ha 'itemised' och grupperad produkt-försäljning
    public static void ProductSalesReport(DateTime startDate, DateTime endDate) //visar Rapport på lista av produkter
    {
        var salesReport = ReportList
            .Where(report => report.Date >= startDate && report.Date <= endDate)
            .SelectMany(report => report.salesList) //tar sales-listan på alla rapporter
            .GroupBy(sale => sale.Product.Name) //tar total sales/försäljning per product-namn??? eller?
            .Select(group => new
            {
                ProductName = group.Key,
                TotalQuantitySold = group.Sum(s => s.Quantity),
                TotalSalesAmount = group.Sum(s => s.TotalAmount)
            })
            .OrderByDescending(report => report.TotalSalesAmount);
} 

// metod som håller logik för Report-kompilering för vald kategori - i en switch

public static class ReportHandler {}; //metod att kalla på

    // public static double PaidAmount { get; set; }
    // public static double Tips { get; set; }
    // public static double AmountToPay { get; set; }
    // public static double Vat12 { get; set; }
    // public static double Vat25 { get; set; }
    // public static bool IsCash { get; set; }
    // public static DateTime PaymentAccepted { get; set; }
    

    public static void ReportMenu() //metod att kalla på i UserInterFace
    {

        Console.WriteLine("Var god välj vilken Rapport du vill generera: ");
        Console.WriteLine("Tryck 1. För TotalSales: ");
        Console.WriteLine("Tryck 2. För WeeklySales: ");
        Console.WriteLine("Tryck 3. För DailySales: ");
        Console.WriteLine("Tryck 3. För SalesReport: ");
        Console.WriteLine("Tryck 3. För UserReport: ");
        Console.WriteLine("Tryck 3. För TableReport: ");
        Console.WriteLine("Tryck 3. För ProductReport: ");
        Console.WriteLine("Tryck 3. För TipsReport: ");
        Console.WriteLine("Tryck Q. För att avsluta till huvudmenyn: ");

        string? reportChoice = Console.ReadLine();
        Report reportCategory = reportChoice
        switch
        {
            case "1":
                { Report.TotalSales,   
            "2" => Report.WeeklySales,
            "3" => Report.DailySales,
            "4" => Report.User,
            "Q" or _ => Report.ReportCategory.Receipt,
        };
        if (reportCategory == Report.ReportCategory.Receipt)
        {
            Console.WriteLine("Tillbaka till huvudmenyn. Välkommen åter!");
            return;
        }
        if (!Report.GetDate($"Ange startdatum för rapporten (YYYY-MM-DD):", out DateTime startDate) || 
            !Report.GetDate($"Ange slutdatum för rapporten (YYYY-MM-DD): ", out DateTime endDate))     
        {
            Console.WriteLine("Ogiltig input. Försök igen (YYYY-MM-DD).");
            return;
        }
    }
    }



//TODO metoder som tar listor för: total, weekly och daily sales
//TODO ReportHandler. vad gör den?
//Insert: ReportMenu så att val hanteras för de olika rapporterna
// TODO de olika egenskaperna för Rapport kan samlas ihop eller köras enskilt, alltid mha DATUM
//TODO jSon. fil för uppsamling av rapporter




/*    public List<Sales> salesList {get; set;} = new();
    public void AddSale(Product product, int quantity, DateTime date)
    {
        salesList.Add(new Sales(product, quantity, date));
    }

    */