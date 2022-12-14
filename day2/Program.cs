var points = new Dictionary<string, Dictionary<string, int>>();

points["X"] = new Dictionary<string, int> {{"A", 3}, {"B", 1}, {"C", 2}};
points["Y"] = new Dictionary<string, int> {{"A", 4}, {"B", 5}, {"C", 6}};
points["Z"] = new Dictionary<string, int> {{"A", 8}, {"B", 9}, {"C", 7}};

var score = File.ReadLines("/Users/tpetrovic/Projects/aoc/day2/input.txt")
    .Select(e => e.Trim())
    .Select(e => points[e.Split(" ")[1]][e.Split(" ")[0]])
    .Sum();

Console.WriteLine(score);


