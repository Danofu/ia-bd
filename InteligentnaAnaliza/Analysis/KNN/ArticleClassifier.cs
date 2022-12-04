public class ArticleClassifier : Article
{
    private readonly List<decimal> dimensions;

    public List<decimal> Dimensions => dimensions;

    public ArticleClassifier(Article article, Dimensions dimensions) : base(article)
    {
        if (!dimensions.Any())
            throw new Exception("Expected dimensions to not be empty");

        this.dimensions = dimensions.Select(NormalizedWordOccurrencies).ToList();
    }

    private int WordOccurrencies(string word)
    {
        word = Util.SanitizeText(word.Split(" ")[0]);

        return Words.FindAll(w => w.Equals(word)).Count;
    }

    private decimal NormalizedWordOccurrencies(string word)
    {
        return ((decimal) WordOccurrencies(word)) / Words.Count;
    }
}
