namespace Tests.EmailService;

public static class TestResults
{
    private static int _counter = 1;

    private static readonly string PathToFile =
        "/Users/kate/RiderProjects/SaucedemoTestProject/SaucedemoTestProject/Tests/ProjectInfo/TestResults.txt";

    public static void GetTestResults(string testName, bool isPassed)
    {
        string result;
        switch (isPassed)
        {
            case true:
                result = $"{_counter}. Test '{testName}' was executed with the passed result.";
                AddTestResultToFile(result);
                break;
            case false:
                result = $"{_counter}. Test '{testName}' was executed with the failed result.";
                AddTestResultToFile(result);
                break;
        }

        _counter++;
    }

    private static void AddTestResultToFile(string result)
    {
        using StreamWriter sw = File.AppendText(PathToFile);
        sw.WriteLine(result);
    }

    public static void CountStatisticToFileResult()
    {
        var lines = File.ReadAllLines(PathToFile);
        var passed = 0;
        var failed = 0;
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

        var statistic = $"Amount of tests: {lines.Length}. Passed: {passed}; Failed: {failed}.";

        AddTestResultToFile(statistic);
    }

    public static void ClearFileBeforeTestExecution()
    {
        File.Create(PathToFile).Close();
    }
}