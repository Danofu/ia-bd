public class ArticlePrediction
{
    private ArticleClassifier article;

    private readonly Dictionary<string, decimal> prediction;

    public ArticlePrediction(ArticleClassifier article, List<ArticleClassifier> neighbors)
    {
        this.article = article;

        prediction = (
            Places.All
            .Select(place => new KeyValuePair<string, decimal>(place, 0.0m))
            .ToDictionary(kv => kv.Key, kv => kv.Value)
        );

        var frequencies = new Frequencies();
        frequencies.Increment(neighbors.Cast<Article>().ToList());

        prediction = frequencies.Normalized;
    }

    public string Prediction => prediction.MaxBy(kv => kv.Value).Key;
    public string Actual => article.Place;
}
