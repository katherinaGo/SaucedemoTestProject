using Tests.Models;

namespace Tests.Reflection;

public class UserReflection
{
    private static readonly Type _myType = typeof(User);
    private static readonly User _user = new();

    public static string? SetCustomValueToFieldPassword()
    {
        var password = _myType.GetProperty("Password");
        password?.SetValue(_user, "myPassword");
        return _user.Password;
    }
}