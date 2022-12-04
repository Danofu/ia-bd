using System.Text.RegularExpressions;

public class Util
{
    private static string ReplaceHexSymbols(string xml)
    {
        return Regex.Replace(xml, "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]", "", RegexOptions.Compiled);
    }

    public static string PrepareXML(string xml)
    {
        var removedHeader = xml.Replace("<!DOCTYPE lewis SYSTEM \"lewis.dtd\">", string.Empty);

        return "<Articles>" + ReplaceHexSymbols(removedHeader) + "</Articles>";
    }

    public static string GetDatasetsDirectory()
    {
        string rootPath = Path.GetFullPath(@"..\..\..");

        var directory = Path.GetDirectoryName(rootPath);
        ArgumentNullException.ThrowIfNull(directory);

        if (!rootPath.Contains("InteligentnaAnaliza") || !directory.Contains("InteligentnaAnaliza"))
            throw new Exception("Invalid rootPath");

        return Path.Combine(rootPath, "..", "Datasets", "reuters21578");
    }

    public static string GetArticleExportDirectory()
    {
        string rootPath = Path.GetFullPath(@"..\..\..");

        var directory = Path.GetDirectoryName(rootPath);
        ArgumentNullException.ThrowIfNull(directory);

        if (!rootPath.Contains("InteligentnaAnaliza") || !directory.Contains("InteligentnaAnaliza"))
            throw new Exception("Invalid rootPath");

        return Path.Combine(rootPath, "..", "Datasets", "ArticleExport");
    }

    private static string CreateTimestamp()
    {
        var datetime = DateTime.Now;
        return datetime.ToString("yyyy MM dd HH mm ss").Replace(" ", "");
    }

    public static string GetLoggerDestination()
    {
        string rootPath = Path.GetFullPath(@"..\..\..");

        var directory = Path.GetDirectoryName(rootPath);
        ArgumentNullException.ThrowIfNull(directory);

        if (!rootPath.Contains("InteligentnaAnaliza") || !directory.Contains("InteligentnaAnaliza"))
            throw new Exception("Invalid rootPath");

        return Path.Combine(rootPath, "..", "Datasets", $"log-{CreateTimestamp()}.txt");
    }

    public static string SanitizeText(string text)
    {
        var pattern = new Regex(@"[^a-z0-9\s]");
        var whitespacePattern = new Regex(@"\s{2,}");

        return whitespacePattern.Replace(
            pattern.Replace(text.ToLower().Trim(), ""), " ");
    }
}

public static class Logger
{
    private static string? destination;

    public static int CursorLeft => Console.CursorLeft;
    public static int CursorTop => Console.CursorTop;

    public static string? Destination
    {
        get => destination;
        set
        {
            if (File.Exists(value))
                File.Delete(value);

            destination = value;
        }
    }

    private static void AppendToFile(string line)
    {
        if (destination is null)
            throw new Exception("Destination is not set");

        if (!Directory.Exists(Path.GetDirectoryName(destination)))
            throw new Exception("Directory in path specified does not exist.");


        File.AppendAllLines(destination, new[] { line });
    }

    public static void SetCursorPosition(int left, int top)
    {
        Console.SetCursorPosition(left, top);
    }

    public static void WriteLine(string line, bool consoleOnly = false)
    {
        Console.WriteLine(line);

        if (!consoleOnly)
            AppendToFile(line);
    }
}
