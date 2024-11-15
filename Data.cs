using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

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
        UserHandler.userList = JsonConvert.DeserializeObject<List<User>>(json);
    }
    else 
    {
        Console.WriteLine("Inga objekt hittade!");
    }
}
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