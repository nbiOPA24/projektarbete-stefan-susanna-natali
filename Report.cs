using System.Globalization;
using System.Security.Cryptography;

public class Report
{
    public enum ReportCategory
    {
        TotalSales,
        WeeklySales,
        DailySales,
        Receipt
    }


    //tar emot datum-input i formatet YYYY-MM-DD för rapporter. TODO ta 'dagens datum' för dailyrapport
    public static bool GetDate(string prompt, out DateTime date)
    {
        Console.WriteLine(prompt);
        string? dateInput = Console.ReadLine();
        return DateTime.TryParseExact(dateInput, "yyyy-MM-dd",
                                                System.Globalization.CultureInfo.InvariantCulture,
                                                System.Globalization.DateTimeStyles.None, out date);
    }

    public int ReportNumber { get; set; }
    public DateTime Date { get; set; }
    public ReportCategory Category { get; set; }

    public List<Sales> salesList { get; set; } = new();

    public void AddSale(Product product, int quantity, DateTime date)
    {
        salesList.Add(new Sales(product, quantity, date));
    }
}


public class Sales // för att hantera individuella 'sales' inom 'Report'-rapporter.
                   //TODO gör en egen flik för Sales-klassen?
                   //TODO länka Sales och produkt-lista
{
    public Product Product { get; set; } //TODO länka Sales till Product som säljs
    public DateTime Date { get; set; } //TODO kanske fler egenskaper än summa + tid/datum?
                                       //   public int SalesId {get; set;} //?? ett sätt att identifiera varje unik 'sale' tillfälle, dvs varje 'Table'
    public int Quantity { get; set; } // 'samlar' antal unika sales-tillfällen
    public decimal TotalAmount => (decimal)Quantity * (decimal)Product.Price; //kalkylerar total försäljning av 'Product'


    public Sales(Product product, int quantity, DateTime date)
    {
        Product = product;
        Quantity = quantity;
        Date = date;
    }
}

//här hanteras Rapport-val med meny-logik via user input (huvudmenyn från UserInterFace)
public static class ReportHandler
{
    // public static double PaidAmount { get; set; }
    // public static double Tips { get; set; }
    // public static double AmountToPay { get; set; }
    // public static double Vat12 { get; set; }
    // public static double Vat25 { get; set; }
    // public static bool IsCash { get; set; }
    // public static DateTime PaymentAccepted { get; set; }
    public static List<Report> ReportList { get; set; } = new();


    public static void ReportMenu()
    {

        Console.WriteLine("Var god välj vilken Rapport du vill generera: ");
        Console.WriteLine("Tryck 1. För TotalSales: ");
        Console.WriteLine("Tryck 2. För WeeklySales: ");
        Console.WriteLine("Tryck 3. För DailySales: ");
        Console.WriteLine("Tryck Q. För att avsluta till huvudmenyn: ");

        string? reportChoice = Console.ReadLine();
        Report.ReportCategory reportCategory = reportChoice
        switch
        {
            "1" => Report.ReportCategory.TotalSales,   //TODO ändra denna meny - dra index från enum i 'Report' ist?
            "2" => Report.ReportCategory.WeeklySales,
            "3" => Report.ReportCategory.DailySales,
            "4" => Report.ReportCategory.Receipt,
            "Q" or _ => Report.ReportCategory.Receipt,
        };
        if (reportCategory == Report.ReportCategory.Receipt)
        {
            Console.WriteLine("Tillbaka till huvudmenyn. Välkommen åter!");
            return;
        }
        //här tar man in manuell input för datum-spann för vald rapport
        //TODO att kunna välja veckonummer, eller ur en kalender
        if (!Report.GetDate($"Ange startdatum för rapporten (YYYY-MM-DD):", out DateTime startDate) || //Done! justerat för dynamik från user-input
            !Report.GetDate($"Ange slutdatum för rapporten (YYYY-MM-DD): ", out DateTime endDate))     // TODO Lägg in exception för inkorrekt datum-format enligt mall YYYY-MM-DD
        {
            Console.WriteLine("Ogiltig input. Försök igen (YYYY-MM-DD).");
            return;
        }



        //Här visas Rapport-data i konsollen
        //TODO lägga till fler rapport-kategorier samt kanske kolumner för rapport?

        decimal reportTotal = ReportGenerator(reportCategory, startDate, endDate);
        Console.WriteLine($"Total försäljning för {reportCategory} är: {reportTotal: C}");
    }



    public static decimal ReportGenerator(Report.ReportCategory reportCategory, DateTime startDate, DateTime endDate) //generarar sales-rapporter av de slag vi vill ha
                                                                                                                      //lade till manuell DateTime-filtrering for now baserat på user input
                                                                                                                      //TODO integrera maunell datum-input till Table??
                                                                                                                      //TODO tighta till logiken i R-generator, redundans?
    {
        return reportCategory switch
        {
            Report.ReportCategory.TotalSales => TotalSales(startDate, endDate),
            Report.ReportCategory.WeeklySales => WeeklySales(startDate, endDate),
            Report.ReportCategory.DailySales => DailySales(startDate, endDate),
            _ => 0
        };
    }

    //logik för hur rapporterna kompileras
    public static decimal TotalSales(DateTime startDate, DateTime endDate)
    {
        return ReportList
        .Where(report => report.Date >= startDate && report.Date <= endDate)//sorterar efter valt datum-spann
        .Sum(report => report.salesList.Sum(sale => sale.TotalAmount)); //ser till att få en rapport-kategori i taget att funka
    }
    public static decimal WeeklySales(DateTime startDate, DateTime endDate)
    {
        return ReportList
        .Where(report => report.Date >= startDate && report.Date <= endDate)
        .SelectMany(report => report.salesList)
        .GroupBy(sale => CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(sale.Date, CalendarWeekRule.FirstDay, DayOfWeek.Monday))
        .Sum(group => group.Sum(sale => sale.TotalAmount));
    }
    public static decimal DailySales(DateTime startDate, DateTime endDate)
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

        Console.WriteLine("Försäljningsrapport: ");
        foreach (var item in salesReport)
        {
            Console.WriteLine($"Produkt: {item.ProductName}, Total antal styck sålda: {item.TotalQuantitySold}, Total försäljning summa: {item.TotalSalesAmount:C}");
        }
    }

}





//public static decimal PrintReport(){}


/* public static string PrintReport(Report report) {}
{
    var report = 
} */