    var score = File.ReadLines("input.txt")
        .Select(e => e.ToCharArray())
        .Chunk(3)
        .Select(e => e[0].Intersect(e[1]).Intersect(e[2]).First())
        .Select(e => char.IsUpper(e) ? e - 38 : e - 96)
        .Sum();
    
    Console.WriteLine(score);

