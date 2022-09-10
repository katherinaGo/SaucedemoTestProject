namespace Tests.EmailService;

public sealed class TestResults
{
    public static void GetTestResults(string testName, bool isPassed)
    {
        string result;
        if (isPassed.Equals(true))
        {
            result = $"Test '{testName}' was executed with the passed result.";
            AddTestResultToFile(result);
        }

        if (isPassed.Equals(false))
        {
            result = $"Test '{testName}' was executed with the failed result.";
            AddTestResultToFile(result);
        }
    }

    private static void AddTestResultToFile(string result)
    {
        using (StreamWriter sw =
               File.AppendText(
                   "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/TestResults.txt"))
        {
            sw.WriteLine(result);
        }
    }
}