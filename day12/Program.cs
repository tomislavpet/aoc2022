var grid = File.ReadAllLines("/Users/tpetrovic/Projects/aoc/day12/input.txt").Select(line => line.ToCharArray())
    .ToArray();

var visited = new int[grid.Length, grid[0].Length];

int startX = 0;
int startY = 0;
int steps = 1;

for (int y = 0; y < grid.Length; y++)
{
    for (int x = 0; x < grid[y].Length; x++)
    {
        if (grid[y][x] == 'E')
        {
            startX = x;
            startY = y;
            grid[y][x] = 'z';
        }
    }
}

visited[startY, startX] = 1;

bool climb()
{
    var newVisited = (int[,]) visited.Clone();

    for (int y = 0; y < grid.Length; y++)
    {
        for (int x = 0; x < grid[y].Length; x++)
        {
            var currentHeight = grid[y][x];

            if (visited[y, x] != 0)
            {
                if (grid[y][x] == 'a')
                {
                    return true;
                }

                if (y > 0 && visited[y - 1, x] == 0 && currentHeight - grid[y - 1][x] <= 1)
                {
                    newVisited[y - 1, x] = steps;
                }

                if (y < grid.Length - 1 && visited[y + 1, x] == 0 && currentHeight - grid[y + 1][x] <= 1)
                {
                    newVisited[y + 1, x] = steps;
                }

                if (x > 0 && visited[y, x - 1] == 0 && currentHeight - grid[y][x - 1] <= 1)
                {
                    newVisited[y, x - 1] = steps;
                }

                if (x < grid[0].Length - 1 && visited[y, x + 1] == 0 && currentHeight - grid[y][x + 1] <= 1)
                {
                    newVisited[y, x + 1] = steps;
                }
            }
        }
    }

    visited = newVisited;

    return false;
}

while (!climb())
{
    steps++;
}

Console.WriteLine($"steps: {steps - 1}");