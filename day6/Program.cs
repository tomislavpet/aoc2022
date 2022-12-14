    var HEADER_LENGTH = 14;
    
    var input = File.ReadAllText("input.txt");
    
    var index = HEADER_LENGTH;
    var buffer = input[..HEADER_LENGTH].ToCharArray();
    var occurence = buffer.GroupBy(e => e).ToList().ToDictionary(e => e.Key, e => e.Count());
    
    while (occurence.Keys.Count(e => occurence[e] > 0) < HEADER_LENGTH)
    {
        occurence[buffer[HEADER_LENGTH - 1]] -= 1;
    
        for (var i = HEADER_LENGTH - 1; i > 0; i--)
            buffer[i] = buffer[i - 1];
    
        buffer[0] = input[index++];
        occurence[buffer[0]] = occurence.GetValueOrDefault(buffer[0]) + 1;
    }
    
    Console.WriteLine(index);