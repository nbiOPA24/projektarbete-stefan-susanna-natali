public class User

{
    public string? FirstName {get; set;} // TODO fixa ev. lastname
    
    public int UserId {get; set;}
    //TypeOfUser, chef, hov,
    // TODO göra userId 24 DateTime eller ska det bara vara 2-siffor? //ska det vara en algoritm/beräkninssätt
    public User (string firstName, int userId)
    {
        FirstName = firstName;
        UserId = userId;
        
    }
    public User() // tom konstruktor som sätter default-värden
    {
        UserId = 2400;
    }

}
public static class UserHandler
{
    public static List<User> userList = new ();

    public static List<User> PrintUser (List<User> userList)
    {   Console.WriteLine("Här är personallistan: ");
        foreach (User u in userList)
        {
            Console.WriteLine("Namn: " + u.FirstName + " - " + u.UserId);
            
        }
        Console.WriteLine("________________________");
        return userList;
    }
    public static void AddUser(User user)
    {
        Console.WriteLine("LÄGG TILL PERSONAL");
        Console.Write("Personalens förnamn: "); 
        string? firstname = UppercaseFirst(Console.ReadLine());

        user.UserId++;
        Console.WriteLine("Personal " + firstname + " är tillagd! Tilldelat ID: " + user.UserId); // tilldelas id-nummer i kronologisk ordning

        User newUser = new(firstname,user.UserId);
        userList.Add(newUser);

    }
        private static string UppercaseFirst(string str) // TODO gör denna universal till hela programmet
        {
            if (string.IsNullOrEmpty(str))
            return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }

    public static void RemoveUser(User user)
    {
        Console.WriteLine("TA BORT PERSONAL");
        PrintUser(userList);
        Console.Write("Skriv in ID-nummer för personal du vill ta bort: ");
        int id = int.Parse(Console.ReadLine());

        for (int i = 0; i < userList.Count; i++)
        {
            if (id == userList[i].UserId)
            {
                Console.WriteLine(userList[i].FirstName + " är borttagen!");
                userList.RemoveAt(i);
            }
        }
        
    // public static void SearchForUser(){}
    }
    public static void ModifyUser()
    {
        Console.WriteLine("ÄNDRA PERSONAL");
        PrintUser(userList);
        Console.Write("Skriv in ID-nummer för personal du vill ändra: ");
        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("Vill du ändra 1. Namn eller 2.ID? ");
        int choice = int.Parse(Console.ReadLine());
        foreach (User u in userList)
            {if (choice == 2)
                if (id == u.UserId) //TODO välj från meny namn eller id-nummer.
                {
                    Console.Write("ID-nummer valt. Skriv in nytt: ");
                    int newId = int.Parse(Console.ReadLine());
                    u.UserId = newId;
                    Console.WriteLine("Du har uppdaterat " + u.FirstName + " med ID-nummer: " + u.UserId);
                }
            }

    }
    
}