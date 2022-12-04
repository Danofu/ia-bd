public class ArticleExporter
{
    private readonly string exportDirectory;
    private readonly List<Article> articles;
    private readonly bool overwrite;

    public int ExportCount { get; set; } = 1;
    public int StartFrom { get; set; } = 1;

    public ArticleExporter(List<Article> articles, string exportDirectory, bool overwrite = false)
    {
        this.exportDirectory = exportDirectory;
        this.articles = articles;
        this.overwrite = overwrite;
    }

    public void Export()
    {
        if (overwrite && Directory.Exists(exportDirectory))
            Directory.Delete(exportDirectory, recursive: true);

        if (!Directory.Exists(exportDirectory))
            Directory.CreateDirectory(exportDirectory);
        else
            return;
        
        var baseName = "article";
        var counter = 0;

        foreach (var tag in Places.All)
        {
            var filtered = articles.Where(a => tag.Equals(a.Place)).ToList();

            if (ExportCount < 1 || ExportCount > filtered.Count)
                throw new Exception($"Expected ExportCount to be in range [ 1, {filtered.Count}]. Got: {ExportCount}");

            if (StartFrom < 1 || StartFrom > filtered.Count - ExportCount)
                throw new Exception($"Expected StartFrom to be in range [ 1, {filtered.Count - ExportCount}]. Got: {StartFrom}");

            filtered = filtered.Skip(StartFrom - 1).Take(ExportCount).ToList();

            var fileName = baseName + String.Format("{0:00000}", counter) + ".txt";

            var filePath = Path.Combine(exportDirectory, fileName);

            var lines = new List<string>();
            lines.Add(tag);
            lines.Add("");

            foreach (var article in filtered)
            {
                lines.Add(article.Text);
                lines.Add("");
                lines.Add("");
            }

            File.WriteAllLines(filePath, lines.ToArray());

            counter++;
        }
    }
}
