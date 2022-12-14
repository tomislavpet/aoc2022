class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }

    public void Move(string direction)
    {
        if (direction == "U") Y += 1;
        else if (direction == "D") Y -= 1;
        else if (direction == "L") X -= 1;
        else if (direction == "R") X += 1;
    }

    public void Follow(Point target)
    {
        if (Math.Abs(target.X - X) <= 1 && Math.Abs(target.Y - Y) <= 1) return;

        var stepX = target.X - X;
        var stepY = target.Y - Y;

        X += Math.Sign(stepX);
        Y += Math.Sign(stepY);
    }
}

class Program
{
    static void Main()
    {
        var lines = File.ReadAllLines("input.txt");
        var visited = new HashSet<string>();

        var segments = Enumerable.Range(0, 10).Select(_ => new Point(0, 0)).ToArray();
        var positionHead = segments[0];
        var positionTail = segments[9];

        visited.Add(positionTail.ToString());

        foreach (var line in lines)
        {
            var direction = line.Split(" ")[0];
            var places = Convert.ToInt32(line.Split(" ")[1]);

            for (var i = 0; i < places; i++)
            {
                positionHead.Move(direction);

                for (var j = 1; j < segments.Length; j++)
                {
                    segments[j].Follow(segments[j - 1]);
                }

                visited.Add(positionTail.ToString());
            }
        }

        Console.WriteLine(visited.Count);
    }
}