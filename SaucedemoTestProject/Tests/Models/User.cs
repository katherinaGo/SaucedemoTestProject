namespace Tests.Models;

public class User
{
    private string _userName;
    private string _password;

    public User(Enum userName)
    {
        _userName = userName.ToString();
        _password = "secret_sauce";
    }

    public User()
    {
    }

    public string UserName
    {
        get => _userName;
    }

    public string Password
    {
        get => _password;
        private set => _password = value;
    }
}