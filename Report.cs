/*public class Report
{
    public enum ReportType
    {
        TotalSales,
        WeeklySales,
        DailySales,
        PrintReceipt
    }
}
     public int ReportNumber {get; set;}
     public DateTime Date {get; set;}
     public ReportType Type {get; set;} // TODO ska den heta type? 

     public List<Sale> Sales {get; set;} = new(); 

     public void AddSale(decimal amount, DateTime date)
     {
         Sales.Add(new Sale(amount, date));
     }
*/

 /*public class Sale // för att hantera individuella 'sales' inom 'Report'-rapporter.
                    //TODO gör en egen flik för Sale-klassen?
 {
     public decimal Amount {get; set;} //"decimal" för att hantera monetära beräkningar
     public DateTime Date {get; set;} //TODO kanske fler egenskaper än summa + tid/datum?

     public Sale(decimal amount, DateTime date)
     {
         Amount = amount;
         Date = date;
     }
 }*/
/*public static class ReportHandler
{
    public static List<Report> SalesList {get; set;}  =  new(); 

    public static decimal ReportGenerator(Report.ReportType reportType) //generarar sales-rapporter av de slag vi vill ha
    {
        if (reportType == reportType.Total)
        {
            return TotalSales();
        }
        else if (reportType == reportType.Weekly)
        {
            return WeeklySales();
        }
        else if (reportType == reportType.Daily)
        {
            return DailySales();
        }
        else
        {
            return 0;
        }
    }
    public static decimal TotalSales(){} 
    public static decimal WeeklySales(){}
    public static decimal DailySales(){}
    public static decimal PrintReport(){}
    
}*/
/* public static string PrintReport(Report report)
{
    var report = 
} */