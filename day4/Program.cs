var data = File
    .ReadLines("input.txt")
    .Select(e => e.Split(",").Select(e => e.Split("-").Select(e => Convert.ToInt32(e)).ToArray()).ToArray());

var first = data.Count(e => (e[0][0] <= e[1][0] && e[0][1] >= e[1][1]) || (e[0][0] >= e[1][0] && e[0][1] <= e[1][1]));
var second = data.Count(e => e[0][0] <= e[1][1] && e[0][1] >= e[1][0]);

Console.WriteLine(first);
Console.WriteLine(second);