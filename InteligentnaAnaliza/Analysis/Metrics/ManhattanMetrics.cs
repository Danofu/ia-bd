public class ManhattanMetrics : IMetrics
{
    public decimal Distance(ArticleClassifier a, ArticleClassifier b)
    {
        if (a.Dimensions.Count != b.Dimensions.Count)
            throw new Exception(
                $"Expected the number of dimensions to be the same. Got: {a.Dimensions.Count}(for a), {b.Dimensions.Count}(for b)");

        var dist = a.Dimensions.Zip(b.Dimensions).Aggregate((double)0, (acc, val) =>
        {
            var (first, second) = val;

            double absOfDiff = Math.Abs((double)(second - first));

            return acc + absOfDiff;
        });

        return new decimal(dist);
    }

    public override string ToString()
    {
        return "Manhattan metrics";
    }
}
