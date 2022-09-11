using System.Reflection;
using EASendMail;
using Tests.MyLogger;

namespace Tests.EmailService;

public class EmailSender
{
    private Logger _myLogger = new();

    private readonly string _pathToEmailCreds =
        "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/emailInfo.txt";

    private readonly string _pathToFileWithResults =
        "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/TestResults.txt";

    public string? Email { get; private set; }

    public string? Password { get; private set; }

    public void SendEmailWithResults()
    {
        GetCredsFromFile();
        try
        {
            SmtpMail smtpMail = new SmtpMail("TryIt")
            {
                From = Email,
                To = "hovinkate@gmail.com",
                Subject = "Test Automation results of SaucedemoTestProject",
                TextBody = "The results of executed tests are in the attached file in this email." +
                           $"\nTests were run {DateTime.UtcNow.ToLocalTime()}." +
                           "\nExecution logs can be found in 'Logs' directory of the project." +
                           "\n" +
                           "\nBest Regards" +
                           "\nJet Brains Rider ruled by Kate Hovin =)\n"
            };
            TestResults.CountStatisticToFileResult();
            smtpMail.AddAttachment(_pathToFileWithResults);

            SmtpServer oServer = new SmtpServer("smtp.mail.ru")
            {
                User = Email,
                Password = Password,
                Port = 465,
                ConnectType = SmtpConnectType.ConnectSSLAuto
            };

            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(oServer, smtpMail);

            _myLogger.CreateLogger();
            _myLogger.InfoLogger("Email was sent successfully!",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
        catch (Exception ep)
        {
            _myLogger.ErrorLogger($"Failed to send email with the following error: {ep.Message}, \n{ep.StackTrace}",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
    }

    private void GetCredsFromFile()
    {
        string[] lines =
            File.ReadAllLines(_pathToEmailCreds);

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