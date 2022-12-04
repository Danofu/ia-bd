using Porter2Stemmer;

public class Stemmer
{
    private List<Article> stemmed;

    public List<Article> Stemmed => stemmed;

    public Stemmer(List<Article> articles)
    {
        var stemmer = new EnglishPorter2Stemmer();
        stemmed = new List<Article>();

        foreach (var article in articles)
        {
            string stemmedText = (
                    article.Words
                    .Select(w => stemmer.Stem(w).Value)
                    .Aggregate("", (acc, val) => acc + " " + val)
                );

            stemmed.Add(new Article(article, stemmedText));
        }
    }

    public override string ToString()
    {
        return $"Stemmed {Stemmed.Count} articles";
    }
}
