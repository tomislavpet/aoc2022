using System.Text.RegularExpressions;

var stacks = Enumerable.Repeat(0, 9).Select(_ => new List<string>()).ToArray();

File.ReadAllLines("stacks.txt").Reverse().ToList()
    .ForEach(line => Enumerable.Range(0, 9).Where(j => line[1 + j * 4] != ' ').ToList()
        .ForEach(j => stacks[j].Add(line[1 + j * 4].ToString())));

File.ReadAllLines("input.txt")
    .Select(line => Regex.Replace(line, "[a-zA-Z]", "").Split("  ").Select(int.Parse).ToArray()).ToList()
    .ForEach(rules =>
    {
        stacks[rules[2] - 1].AddRange(stacks[rules[1] - 1].TakeLast(rules[0]));
        stacks[rules[1] - 1].RemoveRange(stacks[rules[1]- 1].Count - rules[0], rules[0]);
    });

stacks.ToList().ForEach(e => Console.Write(e.Last()));