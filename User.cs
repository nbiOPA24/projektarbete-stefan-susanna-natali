using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

public class User

{
    public enum TypeOfUser
    {
        Manager = 1, // ha admin true
        Headwaiter,
        Sommelier,
        Waiter,
        Bartender
    }
    public static bool Admin { get; set; } //acceslevel
    public string? FirstName { get; set; } // TODO fixa ev. lastname!
    public int UserId { get; set; }
    public static int NextId = 1;
    public TypeOfUser UserType { get; set; }

    public User(TypeOfUser userType, string firstName)
    {
        UserType = userType;
        FirstName = firstName;
        UserId = 2400;
        UserId += NextId;
        NextId++;
    }


}
public static class UserHandler
{
    public static List<User> userList = new();
    public static void IsAdmin()
    {
        // foreach (User u in userList) User currentUser = UserHandler.userList.Find(user => user.UserId == UserChoice);
        // {
        User currentUser = UserHandler.userList.Find(user => user.UserId == UserInterFace.UserChoice);

        if (currentUser.UserType == User.TypeOfUser.Manager || currentUser.UserType == User.TypeOfUser.Headwaiter || currentUser.UserType == User.TypeOfUser.Sommelier)
        {
            User.Admin = true;
        }
        else
        {
            User.Admin = false;
        }

    }
    public static void PrintUser()
    {
        Console.WriteLine("Här är personallistan: ");
        if (userList.Count == 0) //TODO(if admin)
        {
            Console.WriteLine("------TOM------");
            Console.Write("Lägga till personal? j/n: ");
            string? input = Console.ReadLine().ToUpper();
            if (input == "J")
            {
                AddUser();
            }
            else if (input == "N")
            {
                Console.WriteLine("Bekräftar, lägger inte till personal");
            }
            else
            {
                Console.WriteLine("Ogilig input!");
            }

        }
        else
        {

            foreach (User u in userList)
            {
                Console.WriteLine("Namn: " + u.FirstName + " - " + u.UserId);

            }
            Console.WriteLine("________________________");

        }


    }
    public static void PrintUserType()
    {
        Console.WriteLine("Behörighetslista: ");
        foreach (User.TypeOfUser u in Enum.GetValues(typeof(User.TypeOfUser)))
        {
            Console.WriteLine((int)u + ". " + u);
        }

    }
    public static void AddUser()
    {
        Console.WriteLine("LÄGG TILL PERSONAL");
        PrintUserType();
        Console.Write("Behörighet, ange utifrån siffra: ");
        try
        {
            int input = int.Parse(Console.ReadLine());
            var userArray = Enum.GetValues(typeof(User.TypeOfUser));
            User.TypeOfUser selectedUserType = (User.TypeOfUser)userArray.GetValue(input - 1); // hämtar usertypen efter angivet heltal 

            Console.Write("Personalens förnamn: ");
            string? firstname = UserInterFace.UppercaseFirst(Console.ReadLine());
            if (int.TryParse(firstname, out int number))
            {
                Console.WriteLine("Ogilitg input, enbart bokstäver är tillåtna!");
                return;
            }

            // Kanske lambda för att hitta den som matchar firstname och skirva ut dennes ID? 

            User newUser = new(selectedUserType, firstname);
            userList.Add(newUser);
            User newAddedUser = userList.Find(user => user.FirstName == firstname);
            Console.WriteLine(selectedUserType + " " + firstname + " är tillagd! Tilldelat ID: " + newAddedUser.UserId); // tilldelas id-nummer i kronologisk ordning

        }
        catch (Exception e)
        {
            Console.WriteLine("Ogiltig input!" + e);

        }

    }


    public static void RemoveUser()
    {
        Console.WriteLine("TA BORT PERSONAL");
        PrintUser();
        Console.Write("Skriv in ID-nummer för personal du vill ta bort: ");
        int id = int.Parse(Console.ReadLine());

        for (int i = 0; i < userList.Count; i++)
        {
            if (id == userList[i].UserId)
            {
                Console.WriteLine(userList[i].FirstName + " är borttagen!");
                userList.RemoveAt(i);
            }
            else
            {
                Console.WriteLine("Ogiltig input!");
            }
        }


    }
    public static void SearchForUser() //TODO kanske en global? Tex. menyval först för 1. User, 2. Product osv och en för allt?
    {
        List<User> searchList = new();
        Console.Write("Skriv in sökning: ");
        string? search = Console.ReadLine();
        search = UserInterFace.UppercaseFirst(search);
        foreach (User u in userList)
        {//om u.Usertype.ToString (namn på enum. ToString: konverterar alla värden till sträng) Equals: jämför resultatet med search
            if (search == u.FirstName || u.UserType.ToString().Equals(search))
            {
                searchList.Add(u);
            }
            if (int.TryParse(search, out int number))
            {
                if (number == u.UserId)
                {
                    searchList.Add(u);
                }
            }
        }
        Console.WriteLine("Sökningen resulterade i: ");
        foreach (User s in searchList)
        {
            Console.WriteLine(s.FirstName + " " + s.UserId);

        }

    }

    public static void EditUser()
    {
        Console.WriteLine("ÄNDRA PERSONAL");
        PrintUser();
        Console.Write("Skriv in ID-nummer för personal du vill ändra: ");

        int id = int.Parse(Console.ReadLine());

        Console.WriteLine("Vill du ändra 1. Namn eller 2.ID? ");
        int choice = int.Parse(Console.ReadLine());
        foreach (User u in userList)
        {
            if (choice == 1 && id == u.UserId)
            {
                Console.Write("Namn valt. Skriv in nytt: ");
                string? newName = Console.ReadLine();

                u.FirstName = newName;
                Console.WriteLine("Du har uppdaterat " + u.FirstName + " med ID-nummer: " + u.UserId);
                break;



            }
            else if (choice == 2 && id == u.UserId)
            {
                
                    Console.Write("ID-nummer valt. Skriv in nytt: ");
                    int newId = int.Parse(Console.ReadLine());
                    u.UserId = newId;
                    Console.WriteLine("Du har uppdaterat " + u.FirstName + " med ID-nummer: " + u.UserId);
                    break;
                
            }
           
        }

    }


    public static void UserStartMenu()
    {
        string filePath = "data.json";
        Data.LoadJson(filePath);

        while (true)
        {
            // lägg till en tillbaka knapp
            Console.WriteLine("1. Se personallista");
            Console.WriteLine("2. Lägg till personal");
            Console.WriteLine("3. Ta bort personal");
            Console.WriteLine("4. Redigera personal");
            Console.WriteLine("5. Sökning");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    PrintUser();
                    break;
                case 2:
                    AddUser();
                    Data.SaveJson(filePath);
                    break;
                case 3:
                    RemoveUser();
                    Data.SaveJson(filePath);
                    break;
                case 4:
                    EditUser();
                    Data.SaveJson(filePath);
                    break;
                case 5:
                    SearchForUser();
                    break;
                default:
                    break;
            }
        }
    }

}