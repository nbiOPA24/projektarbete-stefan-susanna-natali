public class User

{
        public enum TypeOfUser
    {
        Admin = 1,
        Manager,
        HeadWaiter,
        Waiter,
        Bartender
    }
    public string? FirstName {get; set;} // TODO fixa ev. lastname
    public int UserId {get; set;}
    public TypeOfUser UserType {get; set;}

    //TypeOfUser, chef, hov,
    // TODO göra userId 24 DateTime eller ska det bara vara 2-siffor? //ska det vara en algoritm/beräkninssätt
    public User (TypeOfUser userType, string firstName, int userId)
    {
        UserType = userType;
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
    public static void PrintUserType()
    {
        Console.WriteLine("Behörighetslista: ");
        foreach(User.TypeOfUser u in Enum.GetValues(typeof(User.TypeOfUser)))
        {
            Console.WriteLine((int)u +". " + u);
        }
    }
    public static void AddUser(User user)
    {
        Console.WriteLine("LÄGG TILL PERSONAL");
        Console.Write("Behörighet, ange utifrån siffra: ");
        PrintUserType();
        int input = int.Parse(Console.ReadLine());
        var userArray = Enum.GetValues(typeof(User.TypeOfUser));
        User.TypeOfUser selectedUserType = (User.TypeOfUser)userArray.GetValue(input); // hämtar produkttypen efter angivet heltal ??

        Console.Write("Personalens förnamn: "); 
        string? firstname = UppercaseFirst(Console.ReadLine());

        user.UserId++;
        Console.WriteLine("Personal " + firstname + " är tillagd! Tilldelat ID: " + user.UserId); // tilldelas id-nummer i kronologisk ordning

        User newUser = new(selectedUserType, firstname,user.UserId);
        userList.Add(newUser);

    }


    public static void RemoveUser()
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
            {
                if (choice == 2)
                {
                if (id == u.UserId) //TODO välj från meny namn eller id-nummer.
                    {
                        Console.Write("ID-nummer valt. Skriv in nytt: ");
                        int newId = int.Parse(Console.ReadLine());
                        u.UserId = newId;
                        Console.WriteLine("Du har uppdaterat " + u.FirstName + " med ID-nummer: " + u.UserId);
                    }
                }
                else if (choice == 2)
                {
                    Console.Write("Namn valt. Skriv in nytt: ");
                    string? newName = Console.ReadLine();
                    u.FirstName = newName;
                    Console.WriteLine("Du har uppdaterat " + u.FirstName + " med ID-nummer: " + u.UserId);
                }
            }

    }
            private static string UppercaseFirst(string str) // TODO gör denna universal till hela programmet
        {
            if (string.IsNullOrEmpty(str))
            return string.Empty;
            return char.ToUpper(str[0]) + str.Substring(1).ToLower();
        }
    
}