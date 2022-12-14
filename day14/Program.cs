public class Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Program
{
    static int minX = 0;
    static int maxX = 0;
    static int maxY = 0;
    static int startX = 0;
    static char[,] space = new char[1000, 1000];

    static bool isFreeFalling = false;

    static void printSpace()
    {
        Console.Clear();
        Console.WriteLine($"{minX}, 0");
        for (var j = 0; j <= maxY; j++)
        {
            for (var i = minX; i <= maxX; i++)
            {
                Console.Write(space[i, j] == 0 ? '.' : space[i, j]);
            }

            Console.WriteLine();
        }
    }

    private static void drawRock(Point p1, Point p2)
    {
        if (p1.X == p2.X)
        {
            var y = p1.Y;
            var dy = Math.Sign(p2.Y - p1.Y);

            while (y != p2.Y)
            {
                space[p1.X, y] = '#';
                y += dy;
            }

            space[p1.X, y] = '#';
        }

        if (p1.Y == p2.Y)
        {
            var x = p1.X;
            var dx = Math.Sign(p2.X - p1.X);

            while (x != p2.X)
            {
                space[x, p1.Y] = '#';
                x += dx;
            }

            space[x, p1.Y] = '#';
        }
    }

    static void sandFall(int x, int y)
    {
        var isMoving = true;

        while (isMoving)
        {
            // if (x < minX || x > maxX || y > maxY)
            // {
            //     isFreeFalling = true;
            //     return;
            // }

            if (space[x, y + 1] == 0) y++;
            else if (space[x - 1, y + 1] == 0) x--;
            else if (space[x + 1, y + 1] == 0) x++;
            else
            {
                space[x, y] = 'o';
                isMoving = false;
                
                if(x == 500 && y == 0) isFreeFalling = true;
            }
        }
    }

    static void Main()
    {
        var lines = File.ReadAllLines("/Users/tpetrovic/Projects/aoc/day14/input.txt")
            .Select(e =>
                e.Split(" -> ").Select(p => new Point {X = int.Parse(p.Split(",")[0]), Y = int.Parse(p.Split(",")[1])})
                    .ToArray())
            .ToArray();

        minX = lines.Min(e => e.Min(p => p.X));
        maxX = lines.Max(e => e.Max(p => p.X));
        maxY = lines.Max(e => e.Max(p => p.Y));
        startX = 500;

        space[500, 0] = '+';

        foreach (var line in lines)
        {
            for (int i = 0; i < line.Length - 1; i++)
            {
                drawRock(line[i], line[i + 1]);
            }
        }

        drawRock(new Point {X = 0, Y = maxY + 2}, new Point {X = 999, Y = maxY + 2});

        var unitsOfSand = 0;

        while (!isFreeFalling)
        {
            sandFall(startX, 0);
            if (!isFreeFalling) unitsOfSand++;
        }

        printSpace();

        Console.WriteLine(unitsOfSand + 1);
    }
}