public class PatternFactory
{
    private readonly Pattern initial;

    private readonly List<IMetrics> metrics;

    private readonly Dimensions dimensions;

    private readonly IEnumerable<ExclusionSubset> exclusionSubsets;

    public PatternFactory(Dimensions dimensions, IEnumerable<ExclusionSubset> exclusionSubsets)
    {
        metrics = new List<IMetrics>
        {
            new EuclideanMetrics(),
            new ManhattanMetrics(),
            new ChebyshevMetrics()
        };

        this.dimensions = dimensions;

        initial = new Pattern(5, 50, metrics[2], dimensions);

        this.exclusionSubsets = exclusionSubsets;
    }

    public List<Pattern> CreatePatterns()
    {
        return VariateDimensions();
    }

    private List<Pattern> VariateK()
    {
        var result = new List<Pattern>();

        for (var i = initial.K; i < 15; i += 1)
        {
            result.Add(
                new Pattern(i, initial.TrainDataRatio, initial.Metrics, initial.Dimensions));
        }

        return result;
    }

    private List<Pattern> VariateTrainDataRatio()
    {
        var result = new List<Pattern>();

        for (var i = initial.TrainDataRatio; i < 70; i += 10)
        {
            result.Add(
                new Pattern(initial.K, i, initial.Metrics, initial.Dimensions));
        }

        return result;
    }

    private List<Pattern> VariateMetrics()
    {
        var result = new List<Pattern>();

        for (var i = 0; i < metrics.Count(); i++)
        {
            result.Add(
                new Pattern(initial.K, initial.TrainDataRatio, metrics.Skip(i).Take(1).First(), initial.Dimensions));
        }

        return result;
    }

    private List<Pattern> VariateDimensions()
    {
        var result = new List<Pattern>();

        foreach (var subset in exclusionSubsets)
        {
            var d = dimensions.Exclude(subset);

            var pattern = new Pattern(initial.K, initial.TrainDataRatio, initial.Metrics, d)
            {
                Metadata = "Excluded dimensions: " + subset.ToString()
            };

            result.Add(pattern);
        }

        return result;
    }
}
