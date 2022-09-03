using System.Reflection;
using Tests.Models;

namespace Tests.Reflection;

public class UserReflection
{
    private static Type _myType = typeof(User);
    private static User _user = new();

    public static string SetCustomValueToFieldPassword()
    {
        var password = _myType.GetProperty("Password");
        password?.SetValue(_user, "secret_sauce");
        return _user.Password;
    }
}