public class Report
{
    public enum ReportType
    {
        X,
        Y,
        Z
    }
    public int ReportNumber {get; set;}
    public DateTime Date {get; set;}
    public ReportType Type {get; set;} // TODO ska den heta type? 
}
public static class ReportHandler
{
    public static List<Report> saleslist {get; set;}  =  new(); 
    public static void TotalSales(){}  
    public static void WeeklySales(){}
    public static void DailySales(){}
    public static void PrintReceipt(){}
    

    
}