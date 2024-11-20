class Program

//TODO spectra
//TODO få ut orderlist när man skriver ut kvitto
//TODO felhantering!
//TODO lägg till att man kan ändra UserType
//TODO ska inte gå att ändra till ett upptaget 

{

    static void Main(string[] args)
    {
        Console.Clear();
        Receipt receipt = new();
        User user01 = new(User.TypeOfUser.Manager, "Natali");
        UserInterFace.UserInterFaceStartMenu(receipt, user01);

    

    }
}

//Spectre för konsol