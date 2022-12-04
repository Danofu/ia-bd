Logger.Destination = Util.GetLoggerDestination();

// Reading
var reader = new Reader(Util.GetDatasetsDirectory());

Logger.WriteLine("Reading files...");
reader.ReadAll();
Logger.WriteLine(reader.ToString());

Logger.WriteLine("Stemming texts... This may take a while.");
var stemmer = new Stemmer(reader.Articles);
Logger.WriteLine(stemmer.ToString());

// Exporting
/*
Logger.WriteLine("Exporting articles...");
var exporter = (
    new ArticleExporter(stemmer.Stemmed, Util.GetArticleExportDirectory(), overwrite: true)
    {
        ExportCount = 12,
        StartFrom = 101
    });
exporter.Export();
*/

// Analysis

var dimensions = new Dimensions
{
    // west-germany
    "Alcan",
    "rebuff",
    "Board",
    "banks",
    "Italy",
    "president",
    "paragraph",
    "farmers",
    "campaign",
    "significant",
    "budget",
    "pact",
    "Bundesbank",
    "disclose",
    "festivities",
    "major",
    "customers",
    // usa
    "economy",
    "production",
    "NAPM",
    "employment",
    "payments",
    "foreign",
    "equilibrium",
    "position",
    "site",
    "Express",
    "profitable",
    "stake",
    "beyond",
    "Shearson",
    "1987",
    "Association",
    // france
    "maize",
    "commercial",
    "substantial",
    "themselves",
    "describing",
    "starting",
    "soft",
    "requests",
    "Cooperation",
    "OECD",
    "disinflationary",
    "spokesman",
    "Luxembourg",
    "Boeing",
    "Cooperatif",
    // uk
    "Western",
    "payable",
    "estimate",
    "export",
    "delegate",
    "market",
    "lending",
    "concession",
    "Province",
    "indefinitely",
    "visit",
    "foreign",
    "political",
    "solution",
    "inflation",
    // canada
    "Aluminium",
    "extraordinary",
    "reduction",
    "metal",
    "basis",
    "underway",
    "Biltmore",
    "Ontario",
    "Crosbie",
    "Polar",
    "offshore",
    "player",
    "liberalized",
    "chiefly",
    "insurance",
    "telecommunications",
    "publishing",
    // japan
    "daily",
    "manufacturing",
    "carmakers",
    "shipbuilders",
    "government",
    "stability",
    "Narusawa",
    "Machiko",
    "Takashi",
    "Yasuhiro",
    "Sumitomo",
    "Tsukihara",
    "Keidanren",
    "bilateral",
    "semiconductor",
};

var exclusionSubsets = new List<ExclusionSubset>
{
    new ExclusionSubset(new[] { "pact", "profitable", "Luxembourg", "metal", "bilateral" }, dimensions),
    new ExclusionSubset(new[] { "major", "equilibrium", "soft", "foreign", "basis" }, dimensions),
    new ExclusionSubset(new[] { "employment", "maize", "Western", "telecommunications", "shipbuilders"  }, dimensions),
    new ExclusionSubset(new[] { "farmers", "stake", "OECD", "market", "Takashi" }, dimensions),
    new ExclusionSubset(new[] { "rebuff", "1987", "NAPM", "inflation", "bilateral" }, dimensions)
};

var articles = stemmer.Stemmed;

var patternFactory = new PatternFactory(dimensions, exclusionSubsets);

var patterns = patternFactory.CreatePatterns();

var patternRunner = new PatternRunner(articles, patterns)
{
    OnPatternRun = (counter, count, metadata) =>
    {
        Logger.WriteLine($"Pattern {counter} of {count}");

        if (!string.IsNullOrEmpty(metadata))
            Logger.WriteLine($"Additional info: {metadata}");
    }
};

Logger.WriteLine("Running patterns. ");
await patternRunner.Run();