using System.Text.Json.Serialization;
using System.Xml;
using Newtonsoft.Json;
<<<<<<< HEAD:Data.cs
using System;
using System.Collections.Generic;
using System.IO;
=======
using System.IO;
public class DataContainer
{    //Lägg in listorna
    public static List<User> userList = new();
public void SaveOrderAsJson(string filePath)
{
    string json = JsonConvert.SerializeObject(userList);
    File.WriteAllText(filePath, json);
}
>>>>>>> d4ef7b1 (merge med upstream main):DataContainer.cs

public static class Data
{    //Lägg in listorna

public static void SaveJson(string filePath)
{
    string json = JsonConvert.SerializeObject(UserHandler.userList, Formatting.Indented);
    File.WriteAllText(filePath, json);
}

    public static void LoadJson(string filePath)
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            User user = JsonConvert.DeserializeObject<User>(json);
            UserHandler.userList = new List<User> { user };
        }
        else 
        {
            Console.WriteLine("Inga objekt hittade!");
        }
    }
<<<<<<< HEAD:Data.cs
=======
}

public static void SaveProductJson(List<Product>productList)
{
    string productJson = JsonConvert.SerializeObject(productList);
    File.WriteAllText("product.json", productJson);
}

public static void SaveSalesJson(List<Sales> Sales)
{
    string salesJson = JsonConvert.SerializeObject(Sales);
    File.WriteAllText("sales.json", salesJson);
}
>>>>>>> d4ef7b1 (merge med upstream main):DataContainer.cs
    // public static void SaveJson(string filePath, DataContainer dataContainer)
    // {
    //     try
    //     {
    //         string json = JsonConvert.SerializeObject(dataContainer, Formatting.Indented);
    //         File.WriteAllText(filePath, json);
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
    //     }
    // }

    // public static DataContainer LoadJson(string filePath)
    // {
    //     try
    //     {
    //         if (File.Exists(filePath))
    //         {
    //             string json = File.ReadAllText(filePath);
    //             return JsonConvert.DeserializeObject<DataContainer>(json) ?? new DataContainer();
    //         }
    //         else
    //         {
    //             Console.WriteLine("File not found.");
    //             return new DataContainer();
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"An error occurred while loading the file: {ex.Message}");
    //         return new DataContainer();
    //     }
    // }

}