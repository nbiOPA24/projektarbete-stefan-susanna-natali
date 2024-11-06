<<<<<<< HEAD
// public class Report
// {
//     public enum ReportCategory //ändrade från 'type' till 'category'
//     {
//         TotalSales,
//         WeeklySales,
//         DailySales,
//         PrintReceipt
//     }

//      public int ReportNumber {get; set;}
//      public DateTime Date {get; set;}
//      public ReportCategory Type {get; set;} // Ändrade från 'type' till 'Category'

//      public List<Sales> Sales {get; set;} = new(); 

//      public void AddSale(decimal amount, DateTime date)
//      {
//          Sales.Add(new Sales(amount, date));
//      }
// }


//  public class Sales // för att hantera individuella 'sales' inom 'Report'-rapporter.
//                     //TODO gör en egen flik för Sales-klassen?
//                     //TODO länka Sales och produkt-lista
//  {
//      public decimal TotalAmount => Quantity * Product.Price; //kalkylerar total försäljning av 'Product'
//      public Product Product {get; set;} //länka Sales till Product som säljs
//      public DateTime Date {get; set;} //TODO kanske fler egenskaper än summa + tid/datum?
// //   public int SalesId {get; set;} //?? ett sätt att identifiera varje unik 'sale' tillfälle, dvs 'Table'

//      public int Quantity {get; set;} // 'samlar' antal unika sales-tillfällen


//      public Sales(Product product, int quantity, DateTime date)
//      {
//          Product = product;
//          Date = date;
//          Quantity = quantity;
//      }
//  }
// public static class ReportHandler
// {
//     public static List<Report> SalesList {get; set;}  =  new(); 

//     public static decimal ReportGenerator(Report.ReportCategory reportCategory) //generarar sales-rapporter av de slag vi vill ha
//     {
//         if (reportCategory == reportCategory.Total)
//         {
//             return TotalSales();
//         }
//         else if (reportCategory == reportCategory.Weekly)
//         {
//             return WeeklySales();
//         }
//         else if (reportCategory == reportCategory.Daily)
//         {
//             return DailySales();
//         }
//         else
//         {
//             return 0;
//         }
//     }
//     public static decimal TotalSales(){} 
//     public static decimal WeeklySales(){}
//     public static decimal DailySales(){}
//     public static decimal PrintReport(){}
    
// }
// /* public static string PrintReport(Report report)
// {
//     var report = 
// } */
=======
public class Report
{
    public enum ReportType
    {
        TotalSales,
        WeeklySales,
        DailySales,
        PrintReceipt
    }

     public int ReportNumber {get; set;}
     public DateTime Date {get; set;}
     public ReportType Type {get; set;} // TODO ska den heta type? 

     public List<Sale> Sales {get; set;} = new(); 

     public void AddSale(decimal amount, DateTime date)
     {
         Sales.Add(new Sale(amount, date));
    }
 }

 public class Sales // för att hantera individuella 'sales' inom 'Report'-rapporter
                    // TODO en egen SALES-flik...

{
     public decimal Amount {get; set;} //"decimal" för att hantera monetära beräkningar
     public DateTime Date {get; set;} //TODO kanske fler egenskaper än summa + tid/datum?

     public Sales(decimal amount, DateTime date)
     {
         Amount = amount;
         Date = date;
     }
 }
public static class ReportHandler
{
    public static List<Report> saleslist {get; set;}  =  new(); 

    public static decimal ReportGenerator(ReportType reportType) //generarar sales-rapporter av de slag vi vill ha
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
    public static decimal PrintReceipt(){}
    
}
>>>>>>> Susanna
