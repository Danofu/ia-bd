using System.Text;

public class Frequencies
{
    private readonly Dictionary<string, int> frequencies;

    public Dictionary<string, decimal> Normalized => (
            frequencies
            .Select(kv =>
            {
                var value = new decimal(kv.Value) / frequencies.Count;
                return new KeyValuePair<string, decimal>(kv.Key, value); 
            })
            .ToDictionary(kv => kv.Key, kv => kv.Value)
        );

    public Frequencies()
    {
        frequencies = (
                Places.All
                .Select(place => new KeyValuePair<string, int>(place, 0))
                .ToDictionary(kv => kv.Key, kv => kv.Value)
            );
    }

    private Frequencies(Dictionary<string, int> frequencies)
    {
        this.frequencies = frequencies;
    }

    public void Increment(string tag)
    {
        if (!frequencies.ContainsKey(tag))
            throw new Exception($"Invalid tag provided: {tag}");

        frequencies[tag]++;
    }

    public void Increment(List<Article> articles)
    {
        foreach (var article in articles)
        {
            Increment(article.Place);
        }
    }

    public static Frequencies operator +(Frequencies a, Frequencies b)
    {
        var result = (
            Places.All
            .Select(place => {
                var sum = a.frequencies[place] + b.frequencies[place];

                return new KeyValuePair<string, int>(place, sum);
            })
            .ToDictionary(kv => kv.Key, kv => kv.Value));

        return new Frequencies(result);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        foreach (var kv in frequencies)
        {
            sb.AppendLine(kv.Key + ": " + kv.Value);
        }

        return sb.ToString();
    }
}
