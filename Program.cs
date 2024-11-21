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
        //Receipt receipt = new();
        User user01 = new(User.TypeOfUser.Manager, "Natali");
        Table table01 = new(1, false, 4);
        Table table02 = new(2, false, 4);
        Table table03 = new(3, false, 4);
        Table table04 = new(4, false, 4);
        TableHandler.Tables.Add(table01);
        TableHandler.Tables.Add(table02);
        TableHandler.Tables.Add(table03);
        TableHandler.Tables.Add(table04);

        UserInterFace.UserInterFaceStartMenu(user01);




    }
}

//Spectre för konsol