public class User

{
    public enum TypeOfUser
    {
        Admin = 1,
        Manager,
        HeadWaiter,
        Sommelier,
        Waiter,
        Bartender
    }
    public string? FirstName { get; set; } // TODO fixa ev. lastname!
    public int UserId { get; set; }
    public static int NextId = 1;
    public TypeOfUser UserType { get; set; }

    // TODO göra userId 24 DateTime eller ska det bara vara 2-siffor? //ska det vara en algoritm/beräkninssätt
    public User(TypeOfUser userType, string firstName)
    {
        UserType = userType;
        FirstName = firstName;




    }
    public User() // tom konstruktor som sätter default-värden
    {
        FirstName = "N/A";
        UserId = 2400;
        UserId += NextId;
        NextId++; //Todo userID fungerar inte, blir 0
    }

}
public static class UserHandler
{
    public static List<User> userList = new();

    public static void PrintUser(User user)
    {
        Console.WriteLine("Här är personallistan: ");
        if (userList.Count == 0)
        {
            Console.WriteLine("------TOM------");
            Console.Write("Lägga till personal? j/n: ");
            string? input = Console.ReadLine();
            input = UserInterFace.UppercaseFirst(input);
            if (input == "J")
            {
                AddUser(user);
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
        //TODO om behörighetslistan är tom "Finns ingen behöriget"?? 

    }
    public static void AddUser(User user)
    {
        Console.WriteLine("LÄGG TILL PERSONAL");
        PrintUserType();
        Console.Write("Behörighet, ange utifrån siffra: ");
        try
        {
            int input = int.Parse(Console.ReadLine());
            var userArray = Enum.GetValues(typeof(User.TypeOfUser));
            User.TypeOfUser selectedUserType = (User.TypeOfUser)userArray.GetValue(input - 1); // hämtar produkttypen efter angivet heltal ??

            Console.Write("Personalens förnamn: ");
            string? firstname = UserInterFace.UppercaseFirst(Console.ReadLine());
            if (int.TryParse(firstname, out int number))
            {
                Console.WriteLine("Ogilitg input, enbart bokstäver är tillåtna!");
                return;
            }

            // user.UserId++;
            Console.WriteLine(selectedUserType + " " + firstname + " är tillagd! Tilldelat ID: " + user.UserId); // tilldelas id-nummer i kronologisk ordning

            User newUser = new(selectedUserType, firstname);
            userList.Add(newUser);
        }
        catch (Exception e)
        {
            Console.WriteLine("Ogiltig input!" + e);

        }

    }


    public static void RemoveUser(User user)
    {
        Console.WriteLine("TA BORT PERSONAL");
        PrintUser(user);
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

    public static void EditUser(User user) //TODO ändra usertype
    {
        Console.WriteLine("ÄNDRA PERSONAL");
        PrintUser(user);
        Console.Write("Skriv in ID-nummer för personal du vill ändra: ");

        int id = int.Parse(Console.ReadLine());
        Console.WriteLine("Vill du ändra 1. Namn eller 2.ID? ");
        int choice = int.Parse(Console.ReadLine());
        foreach (User u in userList)
        {
            if (choice == 1)
            {
                Console.Write("Namn valt. Skriv in nytt: ");
                string? newName = Console.ReadLine();
                u.FirstName = newName;
                Console.WriteLine("Du har uppdaterat " + u.FirstName + " med ID-nummer: " + u.UserId);
            }
            else if (choice == 2)
            {
                if (id == u.UserId)
                {
                    Console.Write("ID-nummer valt. Skriv in nytt: ");
                    int newId = int.Parse(Console.ReadLine());
                    u.UserId = newId;
                    Console.WriteLine("Du har uppdaterat " + u.FirstName + " med ID-nummer: " + u.UserId);
                }
            }
            else
            {
                Console.WriteLine("Ogilig input!");
            }

        }

    }
    public static void UserStartMenu(User user)
    {
        while (true)
        {
            Console.WriteLine("1. Se personallista");
            Console.WriteLine("2. Lägg till personal");
            Console.WriteLine("3. Ta bort personal");
            Console.WriteLine("4. Redigera personal");
            Console.WriteLine("5. Sökning");

            int choice = int.Parse(Console.ReadLine());
            switch (choice)
            {
                case 1:
                    PrintUser(user);
                    break;
                case 2:
                    AddUser(user);
                    break;
                case 3:
                    RemoveUser(user);
                    break;
                case 4:
                    EditUser(user);
                    break;
                case 5:
                    SearchForUser();
                    break;
            }
        }
    }
    public static void NotValidInput()
    {
        Console.WriteLine("Ogiltig input! Tas tillbaka till startmeny..");

    }

}