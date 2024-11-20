using Newtonsoft.Json;

public static class Data
{    //Lägg in listorna

    public static void SaveUserList(string filePath) //string filePath = ger oss möjligheten att referea till en .jsonfil som vi har i file explorer
    {
        string json = JsonConvert.SerializeObject(UserHandler.userList, Formatting.Indented); //konverterat userlist till json-språk/i json-filen
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
    #region NextId
    public static void SaveNextId(string filePath)
    {
        File.WriteAllText(filePath, User.NextId.ToString());
    }
    public static void LoadNextId(string filePath)
    {
        if (File.Exists(filePath))
        {
            User.NextId = int.Parse(File.ReadAllText(filePath));
        }
        else
        {
            User.NextId = 2400; // Startvärde om filen saknas
        }
    }
    #endregion
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
    public static void SaveNextProductNumber(string filePath)
    {
        File.WriteAllText(filePath, Product.nextNumber.ToString());
    }
    public static int LoadNextProductNumber(string filePath)
    {
        if (File.Exists(filePath))
        {
            return Product.nextNumber = int.Parse(File.ReadAllText(filePath));
        }
        else
        {
            return Product.nextNumber = 9999; // Startvärde om filen saknas
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
    public static void SaveNextReceiptNumber(string filePath)
    {
        File.WriteAllText(filePath, Receipt.nextReceiptNumber.ToString());
    }
    public static int LoadNextReceiptNumber(string filePath)
    {
        if (File.Exists(filePath))
        {
           return Receipt.nextReceiptNumber = int.Parse(File.ReadAllText(filePath));
        }
        else
        {
            return Receipt.nextReceiptNumber = 8888; // Startvärde om filen saknas
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