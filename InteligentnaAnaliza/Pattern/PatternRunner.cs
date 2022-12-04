public class PatternRunner
{
    public Action<int, int, string>? OnPatternRun { get; set; }

    private readonly List<Article> articles;
    private readonly List<Pattern> patterns;

    public PatternRunner(List<Article> articles, List<Pattern> patterns)
    {
        if (!articles.Any())
            throw new Exception("Expected articles to not be empty");

        this.articles = articles;

        if (!patterns.Any())
            throw new Exception("Expected patterns to not be empty");

        this.patterns = patterns;
    }

    public async Task Run()
    {
        var counter = 1;
        var count = patterns.Count;

        foreach (var pattern in patterns)
        {
            if (OnPatternRun is not null)
                OnPatternRun(counter, count, pattern.Metadata);

            var startTime = DateTime.Now;

            await RunPattern(pattern);

            var diff = DateTime.Now - startTime;

            Logger.WriteLine($"Pattern done in {diff.Hours} hours, {diff.Minutes} minutes, {diff.Seconds} seconds");

            counter++;
        }
    }

    public async Task RunPattern(Pattern pattern)
    {
        var datasetControl = new DatasetControl(articles, pattern.Dimensions, pattern.TrainDataRatio);

        var knn = new KNN(pattern.K, datasetControl, pattern.Metrics)
        {
            OnPredictRun = (progress) =>
            {
                var percentage = decimal.Round(progress * 100, 2);

                Logger.SetCursorPosition(0, Logger.CursorTop - 1);
                Logger.WriteLine($"Progress: {percentage}%", true);
            }
        };

        Logger.WriteLine("Starting KNN analysis... Process may take long time.");
        Logger.WriteLine(knn.ToString());
        Logger.WriteLine("\n");

        await knn.Predict();

        Logger.WriteLine(knn.ToString());
    }
}
