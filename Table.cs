using System.Formats.Asn1;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

public class Table
{
    int XPosition { get; set; }
    int YPosition { get; set; }
    int Number {get; set;}
    int Size {get;set;}
}
public class TableHandler
{ 

    public static List<Table> listOfTables = new();
    public static void AddTable(){}
    public static void RemoveTable(){}
    public static void ChangeTable(){}
}

public class TableMap //klass för en bordskarta
{
    public static int XLine { get; set; }
    public static int YLine { get; set; }
    public List<TableMap> tableMaps { get; set; }
    public TableMap(int xLine, int yLine)
    {
        XLine = xLine;
        YLine = yLine;
    }
    public void CreateMap()
    {
        Console.WriteLine("Hur bred ska kartan vara? Ange 1 - 50.\nOm du vill använda standardstorlek tryck enter");
        int xline = int.Parse(Console.ReadLine());

        Console.WriteLine("Hur hög?\nOm du vill använda standardstorlek tryck enter");
        int yLine = int.Parse(Console.ReadLine());
    }
    
}

// public class TableMap
// {   
//     public static string? FrameTop {get; set;} // ska de va char eller kanske struct?
//     public static string? FrameSides {get; set;}
//     public static string? FrameBottom {get; set;}

//     public List<TableMap> map {get; set;}// lista för "ramen" i bordskartan
//     public TableMap(string frameTop, string frameSides, string frameBottom)
//     {
//         map = new List<TableMap>();
//         FrameBottom = frameBottom;
//         FrameTop = frameTop;
//         FrameSides = frameSides;
//     }

//     public void CreateBordskarta()// man kanske behöver en lista för alla x,y botten top?
//     {   
    
//         string? frameSides;
//         string? frameBottom;
//         string? frameTop;
        
//         Console.WriteLine("Hur bred ska kartan vara? Ange 1 - 50.\nOm du vill använda standardstorlek tryck enter");
//         int xLine = int.Parse(Console.ReadLine());
//         Console.WriteLine("Hur hög?\nOm du vill använda standardstorlek tryck enter");
//         int yLine = int.Parse(Console.ReadLine());
        
//         if (xLine == 0)
//         {   
//             xLine = 50;
//             // frameTop = new string('_', xLine); 
//             // Console.WriteLine(frameTop);
//         }
//         if (yLine == 0)
//         {
//             yLine = 25;
//             // for (int i = 0; i < 25; i++)
//             // {   
//             //     map[i] = "|"
//             //     Console.WriteLine(map[i]);
//             // }
//         }
//         frameTop = new string('_', xLine);
//         frameBottom = frameTop;
//         frameSides = $"{yLine}";


//         TableMap nykarta = new(frameBottom, frameTop, frameSides);
//         map.Add(nykarta);

    
//     }
    // public static void DisplayTableMap() // Print table map
    // {
    //     for (int i = 0; i < tableMap.Count; i++)
    //     {
    //         Console.WriteLine();;
    //     }
    // }

   
