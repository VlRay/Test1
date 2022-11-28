
using System.Diagnostics;

class LogWatch
{
    public static string logText = "";
    public static Stopwatch sw = new Stopwatch();
    public static TimeSpan totalElapsed;


    public static void Start()
    {
        sw.Start();
    }

    public static void StopAndLog(string textLog)
    {
        sw.Stop();

        if (logText != "") logText += " - ";

        logText += $"{textLog} {sw.Elapsed}";

        totalElapsed += sw.Elapsed;
    }

    public static void StopAndLogFinal(string textLog)
    {
        StopAndLog(textLog);

        if (logText != "") logText += " - ";

        logText += $"Total {totalElapsed}";
    }




}
