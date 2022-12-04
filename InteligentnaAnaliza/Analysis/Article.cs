public class Article
{
    public string Place { get; private set; }
    public string Text { get; private set; }

    public List<string> Words
    {
        get => Text.Split(" ").ToList();
    }

    public Article(ArticlesREUTERS deserializedArticle)
    {
        if (!IsValidArticle(deserializedArticle))
            throw new Exception("Article is not valid!");

        Place = deserializedArticle.PLACES[0];
        Text = Util.SanitizeText(deserializedArticle.TEXT.BODY);
    }

    public Article(Article old, string stemmedText)
    {
        if (string.IsNullOrEmpty(stemmedText))
            throw new Exception("Expected stemmedText to not be empty.");

        Place = old.Place;
        Text = Util.SanitizeText(stemmedText);
    }

    protected Article(Article article)
    {
        Place = article.Place;
        Text = Util.SanitizeText(article.Text);
    }

    public static bool IsValidArticle(ArticlesREUTERS deserializedArticle)
    {
        return (
                deserializedArticle.PLACES.Length == 1 &&
                Places.IsValidTag(deserializedArticle.PLACES[0]) &&
                (!string.IsNullOrEmpty(deserializedArticle.TEXT.BODY))
            );
    }
}