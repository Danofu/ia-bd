public class EuclideanMetrics : IMetrics
{
    public decimal Distance(ArticleClassifier a, ArticleClassifier b)
    {
        if (a.Dimensions.Count != b.Dimensions.Count)
            throw new Exception(
                $"Expected the number of dimensions to be the same. Got: {a.Dimensions.Count}(for a), {b.Dimensions.Count}(for b)");

        var sum = a.Dimensions.Zip(b.Dimensions).Aggregate((double) 0, (acc, val) =>
        {
            var (first, second) = val;

            double sqOfDiff = Math.Pow((double)(second - first), 2);

            return acc + sqOfDiff;
        });

        var dist = Math.Sqrt(sum);

        return new decimal(dist);
    }

    public override string ToString()
    {
        return "Euclidean metrics";
    }
}
