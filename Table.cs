public class Table
{
    int Number {get; set;}
    public enum ReportCategory 
    {
        X,
        Y,
        Z,
    }
    int Size {get;set;}
}
public static class TableHandler
{
    //2D Bordsarray? 
    public static List<Table> tablelist = new();
    public static void AddTable(){}
    public static void RemoveTable(){}
    public static void ChangeTable(){}
}