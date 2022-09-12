using System.Reflection;
using EASendMail;
using Tests.Driver;
using Tests.MyLogger;

namespace Tests.EmailService;

public class EmailSender
{
    private readonly string _pathToEmailCreds =
        "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/emailInfo.txt";

    private readonly string _pathToFileWithResults =
        "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/TestResults.txt";

    public void SendEmailWithResults()
    {
        Tuple<string, string>? emailAndPassword = GetCredsFromFile();

        try
        {
            SmtpMail smtpMail = new SmtpMail("TryIt")
            {
                From = emailAndPassword?.Item1,
                To = "hovinkate@gmail.com",
                Subject = "Test Automation results of SaucedemoTestProject",
                TextBody = "The results of executed tests are in the attached file in this email." +
                           $"\nTests were run on {DriverInstance.GetDefaultBrowserName()} browser." +
                           "\nExecution logs can be found in 'Logs' directory of the project." +
                           "\n" +
                           "\nBest Regards" +
                           "\nJet Brains Rider ruled by Kate Hovin =)\n"
            };
            TestResults.CountStatisticToFileResult();
            smtpMail.AddAttachment(_pathToFileWithResults);

            SmtpServer oServer = new SmtpServer("smtp.mail.ru")
            {
                User = emailAndPassword?.Item1,
                Password = emailAndPassword?.Item2,
                Port = 465,
                ConnectType = SmtpConnectType.ConnectSSLAuto
            };

            SmtpClient oSmtp = new SmtpClient();
            oSmtp.SendMail(oServer, smtpMail);

            // _myLogger.CreateLogger();
            Logger.InfoLogger("Email was sent successfully!",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
        catch (Exception ep)
        {
            Logger.ErrorLogger($"Failed to send email with the following error: {ep.Message}, \n{ep.StackTrace}",
                GetType().Namespace!,
                GetType().Name,
                MethodBase.GetCurrentMethod()?.Name!);
        }
    }

    private Tuple<string, string> GetCredsFromFile()
    {
        string[] lines = File.ReadAllLines(_pathToEmailCreds);

        string email = "";
        string password = "";

        foreach (var line in lines)
        {
            if (line.Contains("email"))
            {
                email = line.Split(":").Last().Trim();
            }

            if (line.Contains("password"))
            {
                password = line.Split(":").Last().Trim();
            }
        }

        return Tuple.Create(email, password);
    }
}