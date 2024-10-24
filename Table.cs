public class Table
{
    int Number {get; set;}
    int VAT {get; set;}
    public enum Category 
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