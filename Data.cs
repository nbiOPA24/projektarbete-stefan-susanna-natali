using Newtonsoft.Json;

public static class Data
{    //Lägg in listorna

    public static void SaveUserList(string filePath)
    {
        string json = JsonConvert.SerializeObject(UserHandler.userList, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static void LoadUserList(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            UserHandler.userList = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }
        else
        {
            Console.WriteLine("No user data found. Creating a new empty list.");
            UserHandler.userList = new List<User>();
        }
    }
    public static void SaveProductList(string filePath)
    {
        string json = JsonConvert.SerializeObject(ProductHandler.productList, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static void LoadProductList(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            ProductHandler.productList = JsonConvert.DeserializeObject<List<Product>>(json) ?? new List<Product>();
        }
        else
        {
            Console.WriteLine("No product data found. Creating a new empty list.");
            ProductHandler.productList = new List<Product>();
        }
    }
    public static void SaveReceiptList(string filePath)
    {
        string json = JsonConvert.SerializeObject(Payment.receiptList, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static void LoadReceiptList(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Payment.receiptList = JsonConvert.DeserializeObject<List<Receipt>>(json) ?? new List<Receipt>();
        }
        else
        {
            Console.WriteLine("No product data found. Creating a new empty list.");
            Payment.receiptList = new List<Receipt>();
        }
    }
    public static void SaveTableList(string filePath)
    {
        string json = JsonConvert.SerializeObject(TableHandler.tables, Formatting.Indented);
        File.WriteAllText(filePath, json);
    }

    public static void LoadTableList(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            TableHandler.tables = JsonConvert.DeserializeObject<List<Table>>(json) ?? new List<Table>();
        }
        else
        {
            Console.WriteLine("No product data found. Creating a new empty list.");
            TableHandler.tables = new List<Table>();
        }
    }


}