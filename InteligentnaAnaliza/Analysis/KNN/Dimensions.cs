public class Dimensions : List<string>
{
    public Dimensions Exclude(ExclusionSubset subset)
    {
        var result = new Dimensions();

        foreach (var dimension in this)
        {
            if (!subset.Contains(dimension))
            {
                result.Add(dimension);
            }
        }

        return result;
    }
}
