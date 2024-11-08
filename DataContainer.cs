using Newtonsoft.Json;
public class DataContainer
{
    //Lägg in listorna

    public void SaveJson(string filePath, DataContainer dataContainer)
    {
        try
        {
            string json = JsonConvert.SerializeObject(dataContainer, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while saving the file: {ex.Message}");
        }
    }

    public DataContainer LoadJson(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<DataContainer>(json) ?? new DataContainer();
            }
            else
            {
                Console.WriteLine("File not found.");
                return new DataContainer();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the file: {ex.Message}");
            return new DataContainer();
        }
    }

}