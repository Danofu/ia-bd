using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

public class Reader
{
    private List<string> files;

    private List<Article>? articles;

    private Frequencies? frequencies;

    public List<Article> Articles 
    {
        get
        {
            if (articles is null)
                throw new Exception("Expected to access Articles after ReadAll() call.");

            return articles;
        }
    }
    public Frequencies Frequencies
    {
        get
        {
            if (frequencies is null)
                throw new Exception("Expected to access Frequencies after ReadAll() call.");

            return frequencies;
        }
    }

    public Reader(string datasetsDirectory)
    {
        if (!Directory.Exists(datasetsDirectory))
            throw new DirectoryNotFoundException("Datasets directory does not exist");

        var match = new Regex(@"reut.*\.sgm");

        files = (
                Directory.EnumerateFiles(datasetsDirectory)
                .ToList()
                .FindAll(file => match.IsMatch(Path.GetFileName(file)))
            );
    }

    private Articles Read(string file)
    {
        string xml = Util.PrepareXML(File.ReadAllText(file));

        var serializer = new XmlSerializer(typeof(Articles), new XmlRootAttribute("Articles"));

        var reader = new StringReader(xml);

        var articles = (Articles?) serializer.Deserialize(reader);

        if (articles is null)
            throw new Exception($"Error deserializing a file: {file}");

        return articles;
    }

    public void ReadAll()
    {
        if (articles is null && frequencies is null)
        {
            articles = new List<Article>();
            frequencies = new Frequencies();

            foreach (var file in files)
            {
                Articles deserializedArticles = Read(file);

                var validArticleReuters = deserializedArticles.REUTERS.Where(
                        deserializedArticle => Article.IsValidArticle(deserializedArticle));

                foreach (var articleReuters in validArticleReuters)
                {
                    var article = new Article(articleReuters);

                    articles.Add(article);

                    frequencies.Increment(article.Place);
                }
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("Files read: " + files.Count);
        sb.AppendLine("Articles found: " + Articles.Count);
        sb.Append("Tags stats:\n" + Frequencies.ToString());

        return sb.ToString();
    }
}