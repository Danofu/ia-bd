public class ExclusionSubset : List<string>
{
    public ExclusionSubset(IEnumerable<string> subset, Dimensions dimensions) : base(subset)
    {
        if (!subset.All(d => dimensions.Contains(d)))
            throw new Exception("Expected every subset dimension to occur in dimensions");
    }

    public override string ToString()
    {
        return this.Aggregate("", (acc, val) =>
        {
            if (string.IsNullOrEmpty(acc))
                return val;

            return acc + ", " + val;
        }
        );
    }
}
