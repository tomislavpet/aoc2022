    int regX = 1;
    int rayPosition = 0;
    
    void runCycle()
    {
        Console.Write(rayPosition >= regX - 1 && rayPosition <= regX + 1 ? "#" : " ");
    
        if (++rayPosition == 40)
        {
            rayPosition = 0;
            Console.WriteLine();
        }
    }
    
    File.ReadLines("/Users/tpetrovic/Projects/aoc/day10/input.txt")
        .Select(e => e.Split(" ")).ToList()
        .ForEach(parts =>
        {
            switch (parts[0])
            {
                case "noop":
                    runCycle();
                    break;
                case "addx":
                    runCycle();
                    runCycle();
                    regX += int.Parse(parts[1]);
                    break;
            }
        });