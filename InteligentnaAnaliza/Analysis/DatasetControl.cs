using System.Text;

public class DatasetControl
{
    private readonly List<Article> dataset;
    private readonly Dimensions dimensions;
    private decimal ratio;

    private int TrainDataCount
    {
        get => (int) Math.Floor(dataset.Count * ratio);
    }

    private int PredictionDataCount
    {
        get => dataset.Count - TrainDataCount;
    }

    public List<ArticleClassifier> TrainDataset
    {
        get => (
            dataset
                .Take(TrainDataCount)
                .Select(a => new ArticleClassifier(a, dimensions))
                .ToList()
        );
    }

    public List<ArticleClassifier> PredictionDataset
    {
        get => (
            dataset
                .Skip(TrainDataCount)
                .Take(PredictionDataCount)
                .Select(a => new ArticleClassifier(a, dimensions))
                .ToList()
        );
    }

    public DatasetControl(List<Article> dataset, Dimensions dimensions, decimal ratio)
    {
        if (ratio < 0 || ratio > 1)
            throw new Exception("Expected ratio to be in range [ 0, 1 ]");

        this.dataset = dataset;
        this.ratio = ratio;
        this.dimensions = dimensions;
    }

    public DatasetControl(List<Article> dataset, Dimensions dimensions, int percentage) : this(dataset, dimensions, (decimal) percentage / 100) { }
    public DatasetControl(List<Article> dataset, Dimensions dimensions) : this(dataset, dimensions, 50) { }

    public override string ToString()
    {
        var sb = new StringBuilder();

        var trainValue = (int) (ratio * 100);
        var predictionValue = (int) ((1 - ratio) * 100);

        sb.AppendLine($"Dataset ratio: {trainValue}/{predictionValue} (train/prediction)");
        sb.AppendLine($"Train data count: {TrainDataCount}");
        sb.AppendLine($"Prediction data count: {PredictionDataCount}");
        sb.AppendLine($"General data count: {dataset.Count}");

        return sb.ToString();
    }
}
