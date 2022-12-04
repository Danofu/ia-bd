using System.Text;

public class KNN
{
    private readonly int k;

    private readonly List<ArticleClassifier> trainDataset;
    private readonly List<ArticleClassifier> predictionDataset;
    private readonly DatasetControl datasetControl;

    private readonly IMetrics metrics;

    private KNNConfusionMatrix? knnResult;

    public Action<decimal>? OnPredictRun { get; set; }

    public KNNConfusionMatrix Result
    {
        get
        {
            if (knnResult is null)
                throw new Exception("Expected to access Result after Predict() call.");

            return knnResult;
        }
    }

    public KNN(int k, DatasetControl datasetControl, IMetrics metrics)
    {
        this.k = k;
        this.metrics = metrics;

        trainDataset = datasetControl.TrainDataset;
        predictionDataset = datasetControl.PredictionDataset;
        this.datasetControl = datasetControl;
    }

    private Neighbor? Argmin(ArticleClassifier article, decimal? argument = null)
    {
        if (argument is null)
        {
            return trainDataset
                .Select(tdata =>
                {
                    var distance = metrics.Distance(article, tdata);
                    return new Neighbor(tdata, distance);
                })
                .MinBy(t => t.distance);
        }

        var next = trainDataset
            .Select(tdata =>
            {
                var distance = metrics.Distance(article, tdata);
                return new Neighbor(tdata, distance);
            })
            .Where(t => t.distance > argument);

        if (next.Any())
            return next.MinBy(t => t.distance);

        return null;
    }

    private List<ArticleClassifier> GetNearestNeigbors(ArticleClassifier article)
    {
        decimal? argument = null;
        var neighbors = new List<ArticleClassifier>();

        for (var i = 0; i < k; i++)
        {
            var result = Argmin(article, argument);

            if (result is null)
                return neighbors;

            var (neighbor, distance) = result;

            neighbors.Add(neighbor);
            argument = distance;
        }

        return neighbors;
    }

    public async Task Predict()
    {
        if (knnResult is null)
        {
            await Task.Run(() =>
            {
                var result = new List<ArticlePrediction>();
                var counter = 0.0m;

                foreach (var article in predictionDataset)
                {
                    var neighbors = GetNearestNeigbors(article);
                    result.Add(new ArticlePrediction(article, neighbors));

                    if (OnPredictRun is not null)
                        OnPredictRun(decimal.Round(counter / predictionDataset.Count, 4));

                    counter++;
                }

                knnResult = new KNNConfusionMatrix(result);
            });
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("KNN parameters: ");
        sb.AppendLine($"k = {k}");
        sb.Append(datasetControl.ToString());
        sb.AppendLine($"Metrics used: {metrics.ToString()}");

        if (knnResult is not null)
        {
            sb.Append(knnResult.ToString());
        }

        return sb.ToString();
    }
}
