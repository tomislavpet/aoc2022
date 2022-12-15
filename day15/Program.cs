using System.Numerics;

class Sensor
{
    public int X { get; set; }
    public int Y { get; set; }
    public int BeaconX { get; set; }
    public int BeaconY { get; set; }

    public int DeltaX { get; set; }

    public Sensor(int x, int y, int beaconX, int beaconY)
    {
        X = x;
        Y = y;
        BeaconX = beaconX;
        BeaconY = beaconY;

        DeltaX = Math.Abs(x - beaconX) + Math.Abs(y - beaconY);
    }

    public int MinXAtY(int y) => X - DeltaX + Math.Abs(Y - y);
    public int MaxXAtY(int y) => X + DeltaX - Math.Abs(Y - y);
}

class Program
{
    static List<Sensor> sensors = new List<Sensor>();

    static void Solve1(int row)
    {
        var minX = sensors.Min(s => s.X - s.DeltaX);
        var maxX = sensors.Max(s => s.X + s.DeltaX);

        var score = 0;

        for (int i = minX; i <= maxX; i++)
        {
            var isBeacon = false;

            foreach (var s in sensors)
            {
                if (s.BeaconX == i && s.BeaconY == row)
                {
                    isBeacon = true;
                    break;
                }
            }

            if (isBeacon)
                continue;

            foreach (var s in sensors)
            {
                if (i >= s.MinXAtY(row) && i <= s.MaxXAtY(row))
                {
                    score++;
                    break;
                }
            }
        }

        Console.WriteLine(score);
    }

    static void Solve2(int maxBound)
    {
        for (var y = 0; y <= maxBound; y++)
        {
            var bounds = sensors.Select(s => new int[] {Math.Max(s.MinXAtY(y), 0), Math.Min(s.MaxXAtY(y), maxBound)})
                .Where(e => e[0] <= e[1]).ToList();

            bounds.Sort((a, b) => a[0].CompareTo(b[0]));

            var isMerged = true;

            while (isMerged && bounds.Count > 1)
            {
                isMerged = false;

                if (bounds[0][0] <= bounds[1][0] && bounds[0][1] >= bounds[1][0])
                {
                    bounds[0][1] = Math.Max(bounds[0][1], bounds[1][1]);
                    bounds.RemoveAt(1);
                    isMerged = true;
                }
            }

            if (!isMerged || bounds[0][0] != 0 || bounds[0][1] != maxBound)
            {
                Console.WriteLine(((BigInteger) (bounds[0][1] + 1)) * 4000000 + y);
                break;
            }
        }
    }

    static void Main()
    {
        File.ReadAllLines("/Users/tpetrovic/Projects/aoc2022/day15/input.txt").ToList().ForEach(line =>
        {
            var parts = line.Replace("Sensor at x=", "").Replace(", y=", ",").Replace(": closest beacon is at x=", ",")
                .Replace(", y=", ",").Split(",");

            sensors.Add(new Sensor(int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3])));
        });

        Solve1(2000000);
        Solve2(4000000);
    }
}