namespace Tests.EmailService;

public sealed class TestResults
{
    private static int _counter = 1;

    private static readonly string PathToFile =
        "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/TestResults.txt";

    public static void GetTestResults(string testName, bool isPassed)
    {
        string result;
        if (isPassed.Equals(true))
        {
            result = $"{_counter}. Test '{testName}' was executed with the passed result.";
            AddTestResultToFile(result);
        }

        if (isPassed.Equals(false))
        {
            result = $"{_counter}Test '{testName}' was executed with the failed result.";
            AddTestResultToFile(result);
        }

        _counter++;
    }

    private static void AddTestResultToFile(string result)
    {
        using (StreamWriter sw =
               File.AppendText(PathToFile))
        {
            sw.WriteLine(result);
        }
    }

    public static void CountStatisticToFileResult()
    {
        string[] lines = File.ReadAllLines(PathToFile);
        int passed = 0;
        int failed = 0;
        foreach (var line in lines)
        {
            if (line.Contains("passed"))
            {
                passed++;
            }

            if (line.Contains("failed"))
            {
                failed++;
            }
        }

        string statistic = $"Amount of tests: {lines.Length}. Passed: {passed}; Failed: {failed}.";

        AddTestResultToFile(statistic);
    }

    public static void ClearFileBeforeTestExecution()
    {
        File.Create(PathToFile).Close();
    }
}