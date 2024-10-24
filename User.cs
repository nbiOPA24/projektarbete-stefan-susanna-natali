public class User

{
    public string? Name {get; set;}
    public int UserId {get; set;}
    //TypeOfUser, chef, hov,
    // TODO göra userId 24 DateTime eller ska det bara vara 2-siffor? //ska det vara en algoritm/beräkninssätt
    public User (string name, int userId)
    {
        Name = name;
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
    {
        foreach (User u in userList)
        {
            Console.WriteLine("Namn: " + u.Name + " - " + u.UserId);
            Console.WriteLine("________________________");
        }
        return userList;
    }
    public static void AddUser(User user)
    {
        Console.WriteLine("LÄGG TILL PERSONAL");
        Console.Write("Personalens namn: "); 
        string? name = Console.ReadLine(); 
        Console.WriteLine("Personal " + name + " är tillagd! Tilldelat ID: " + user.UserId); // tilldelas kronologisk ordning
        user.UserId++;
        User newUser = new(name,user.UserId);
        
        userList.Add(newUser);

    }

    public static void RemoveUser(User user)
    {
        Console.WriteLine("TA BORT PERSONAL");
        Console.WriteLine("Här är personallistan:");
        PrintUser(userList);
        Console.Write("Skriv in ID-nummer för personal du vill ta bort: ");
        int id = int.Parse(Console.ReadLine());

        foreach (User u in userList)
        {
            userList.Remove(u);
        }

    }
    public static void ModifyUser(){}
    public static void Print(){}
}