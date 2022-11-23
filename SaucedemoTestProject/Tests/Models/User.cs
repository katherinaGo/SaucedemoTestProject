namespace Tests.Models;

public class User
{
    public User()
    {
    }

    public User(Enum userName)
    {
        UserName = userName.ToString();
        Password = "secret_sauce";
    }

    public string? UserName { get; }

    public string? Password { get; private set; }
}