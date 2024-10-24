public class User

{
    public string Name {get; set;}
    public int UserId {get; set;}


}

public static class UserHandler
{
    public static List<User> userList = new ();
    public static void AddUser(){}
    public static void RemoveUser(){}
    public static void ModifyUser(){}
    public static void Print(){}
}