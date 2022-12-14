var data = File.ReadAllLines("/Users/tpetrovic/Projects/aoc/day8/input.txt").Select(e => e.ToCharArray()).ToArray();

var maxX = data[0].Length - 1;
var maxY = data.Length - 1;

var score = new int[data[0].Length, data.Length];
var maxScore = 0;

for (int i = 0; i <= maxY; i++)
{
    for (int j = 0; j <= maxX; j++)
    {
        var height = data[i][j];

        var left = 0;
        var right = 0;
        var up = 0;
        var down = 0;

        var x = j - 1;
        while (x >= 0 && data[i][x] < height)
        {
            left++;
            x--;
        }

        if (x >= 0) left++;
        
        x = j + 1;
        while (x <= maxX && data[i][x] < height)
        {
            right++;
            x++;
        }
        
        if(x <= maxX) right++;

        var y = i - 1;
        while (y >= 0 && data[y][j] < height)
        {
            up++;
            y--;
        }
        
        if(y >= 0) up++;

        y = i + 1;
        while (y <= maxY && data[y][j] < height)
        {
            down++;
            y++;
        }
        
        if(y <= maxY) down++;

        score[i, j] = left * right * up * down;

        if (maxScore < score[i, j])
        {
            maxScore = score[i, j];
        }
    }
}

Console.WriteLine(maxScore);