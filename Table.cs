public class Table
{
    int XPosition { get; set; }
    int YPosition { get; set; }
    int Number {get; set;}
    int Size {get;set;}
}
public class TableHandler
{ 
    public Table Tablemap {get; set;} = new Table();
    public static List<Table> listOfTables = new();
    public static void AddTable(){}
    public static void RemoveTable(){}
    public static void ChangeTable(){}
}

public class Tablemap
{

}
