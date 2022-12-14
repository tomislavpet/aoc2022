class Node
{
    public Node Parent { get; set; }
    public List<Node> Children { get; set; } = new List<Node>();

    public string Type { get; set; }
    public string Name { get; set; }
    public int Size { get; set; }

    public void AddFile(string name, int size)
    {
        var file = new Node {Type = "file", Name = name, Size = size, Parent = this};
        this.Children.Add(file);

        var n = this;

        while (n != null)
        {
            n.Size += size;
            n = n.Parent;
        }
    }

    public Node AddDirectory(string name)
    {
        if (this.Children.FirstOrDefault(e => e.Type == "dir" && e.Name == name) != null)
            return null;

        var dir = new Node {Type = "dir", Name = name, Size = 0, Parent = this};
        this.Children.Add(dir);

        return dir;
    }

    public Node GetDirectory(string name)
    {
        return this.Children.FirstOrDefault(e => e.Type == "dir" && e.Name == name);
    }
}

class Program
{
    static void Main()
    {
        var FS_SIZE = 70000000;
        var UPDATE_SIZE = 30000000;

        var lines = File.ReadAllLines("/Users/tpetrovic/Projects/aoc/day7/input.txt");
        var root = new Node {Type = "dir", Size = 0};
        var current = root;

        var directories = new List<Node> {root};

        foreach (var line in lines)
        {
            if (line == "$ cd /")
            {
                current = root;
            }
            else if (line == "$ ls")
            { }
            else if (line == "$ cd ..")
            {
                current = current.Parent;
            }
            else if (line.StartsWith("$ cd "))
            {
                var dir = line.Replace("$ cd ", "");
                current = current.GetDirectory(dir);
            }
            else if (line.StartsWith("dir"))
            {
                var newDir = current.AddDirectory(line.Split(" ")[1]);

                if (newDir != null)
                    directories.Add(newDir);
            }
            else
            {
                current.AddFile(line.Split(" ")[1], int.Parse(line.Split(" ")[0]));
            }
        }

        var requiredSpace = UPDATE_SIZE - (FS_SIZE - root.Size);

        var result = directories.Where(e => e.Size > requiredSpace).OrderBy(e => e.Size).First().Size;

        Console.WriteLine(result);
    }
}