using System.Text;

public class KNNConfusionMatrix
{
    private int[][] matrix;
    private string[] places;

    public KNNConfusionMatrix(List<ArticlePrediction> prediction)
    {
        matrix = Zeros();
        places = Places.All.ToArray();

        foreach (var p in prediction)
        {
            matrix[IndexOfTag(p.Prediction)][IndexOfTag(p.Actual)] += 1;
        }
    }

    private int IndexOfTag(string tag)
    {
        if (!Places.IsValidTag(tag))
            throw new Exception($"Tag provided is not valid. Got: {tag}");

        for (var i = 0; i < places.Length; i++)
        {
            if (places[i].Equals(tag))
                return i;
        }

        throw new Exception($"Tag provided is not valid. Got: {tag}");
    }

    private int[][] Zeros()
    {
        var size = Places.All.Count();
        return (
                Enumerable
                .Range(0, size)
                .Select(x =>
                {
                    return (
                        Enumerable
                        .Range(0, size)
                        .Select(y => 0).ToArray()
                    );
                })
                .ToArray()
            );
    }


    public double Accuracy
    {
        get
        {
            var tpSum = places.Aggregate(0.000, (acc, tDiag) =>
            {
                var tp = (double)matrix[IndexOfTag(tDiag)][IndexOfTag(tDiag)];
                return acc + tp;
            });

            var sum = (double)matrix.Sum(row => row.Sum());

            if (sum is .0)
                return double.NaN;

            return Math.Round(tpSum / sum, 3);
        }
    }

    public double Precision
    {
        get
        {
            var precisionSum = places.Aggregate(0.000, (acc, tRow) =>
            {
                if (acc is double.NaN)
                    return acc;

                var tp = (double)matrix[IndexOfTag(tRow)][IndexOfTag(tRow)];
                var sum = (double)matrix[IndexOfTag(tRow)].Sum();

                if (sum is .0)
                    return double.NaN;

                return acc + tp / sum;
            });

            if (precisionSum is double.NaN)
                return precisionSum;

            return Math.Round(precisionSum / places.Length, 3);
        }
    }

    public double Recall
    {
        get
        {
            var recallSum = places.Aggregate(0.000, (acc, tCol) =>
            {
                if (acc is double.NaN)
                    return acc;

                var tp = (double)matrix[IndexOfTag(tCol)][IndexOfTag(tCol)];
                var sum = (double)places.Select(p =>
                {
                    return matrix[IndexOfTag(tCol)][IndexOfTag(p)];
                }).Sum();

                if (sum is .0)
                    return double.NaN;

                return acc + tp / sum;
            });

            if (recallSum is double.NaN)
                return recallSum;

            return Math.Round(recallSum / places.Length, 3);
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine("KNN Prediction results: ");
        sb.AppendLine($"Accuracy: {Accuracy}");
        sb.AppendLine($"Precision: {Precision}");
        sb.AppendLine($"Recall: {Recall}");

        return sb.ToString();
    }
}