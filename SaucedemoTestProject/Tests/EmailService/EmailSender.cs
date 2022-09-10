using System.Reflection;
using EASendMail;
using Tests.MyLogger;

namespace Tests.EmailService;

public class EmailSender
{
    private string _email;
    private string _password;
    private Logger MyLogger = new();

    public string Email
    {
        get => _email;
        private set => _email = value;
    }

    public string Password
    {
        get => _password;
        private set => _password = value;
    }

    public void SendEmailWithResults()
    {
        SetCredsFromFile();
        try
        {
            SmtpMail smtpMail = new SmtpMail("Kate Hovin 2022");
            smtpMail.From = Email;
            smtpMail.To = "hovinkate@gmail.com";
            smtpMail.Subject = "Test Automation results";
            smtpMail.TextBody = "The results of executed tests in the file.";
            smtpMail.AddAttachment(
                "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/TestResults.txt");

            SmtpServer oServer = new SmtpServer("smtp.mail.ru");
            oServer.User = Email;
            oServer.Password = Password;
            oServer.Port = 465;
            oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(oServer, smtpMail);

            MyLogger.CreateLogger();
            MyLogger.InfoLogger("Email was sent successfully!",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
        catch (Exception ep)
        {
            Console.WriteLine("Failed to send email with the following error: ");
            Console.Write(ep.Message);
        }
    }

    private void SetCredsFromFile()
    {
        string[] lines =
            File.ReadAllLines(
                "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/emailInfo.txt");

        foreach (string line in lines)
        {
            if (line.Contains("email"))
            {
                Email = line.Remove(0, "email: ".Length);
            }

            if (line.Contains("password"))
            {
                Password = line.Remove(0, "password: ".Length);
            }
        }
    }
}