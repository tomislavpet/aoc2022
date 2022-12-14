public class Node
{
    public Node ParentNode;
    public int Value = -1;
    public List<Node> Nodes = new List<Node>();

    public string Text;
}

public class Program
{
    static Node getNode(string s, ref int i, Node parentNode)
    {
        var node = new Node();
        node.ParentNode = parentNode;

        while (i < s.Length)
        {
            if (s[i] == '[')
            {
                i++;
                node.Nodes.Add(getNode(s, ref i, node));
            }
            else if (s[i] == ']')
            {
                i++;
                return node;
            }
            else if (s[i] == ',' && node.Value != -1)
            {
                i++;
                return node;
            }
            else if (s[i] == ',' && node.Value == -1)
            {
                i++;
                node.Nodes.Add(getNode(s, ref i, node));
            }
            else if (node.Nodes.Count() > 0)
            {
                node.Nodes.Add(getNode(s, ref i, node));
            }
            else
            {
                var value = "";

                while (char.IsDigit(s[i]))
                {
                    value += s[i].ToString();
                    i++;
                }

                node.Value = int.Parse(value);
                return node;
            }
        }

        return node;
    }

    static void printNode(Node n)
    {
        if (n.Value != -1) Console.Write(n.Value);
        if (n.Nodes.Count > 0)
        {
            Console.Write("[");

            for (int i = 0; i < n.Nodes.Count(); i++)
            {
                printNode(n.Nodes[i]);
                if (i < n.Nodes.Count() - 1) Console.Write(",");
            }

            Console.Write("]");
        }
    }

    static int isInOrder(Node n1, Node n2)
    {
        if ((n1.Value != -1 || n1.Nodes.Count > 0) && (n2.Value == -1 && n2.Nodes.Count == 0))
        {
            Console.WriteLine("Right side ran out of items, so inputs are not in the right order");
            return -1;
        }

        if (n1.Value != -1 && n2.Value != -1)
        {
            Console.WriteLine($"Compare {n1.Value} vs {n2.Value}");

            var result = n1.Value <= n2.Value;

            if (n1.Value < n2.Value) Console.WriteLine("Left side is smaller, so inputs are in the right order");
            if (!result) Console.WriteLine("Right side is smaller, so inputs are not in the right order");

            if (n1.Value == n2.Value) return 0;
            
            return result ? 1 : -1;
        }

        if (n1.Value != -1)
        {
            n1.Nodes.Add(new Node
            {
                ParentNode = n1,
                Value = n1.Value
            });

            n1.Value = -1;
        }

        if (n2.Value != -1)
        {
            n2.Nodes.Add(new Node
            {
                ParentNode = n2,
                Value = n2.Value
            });

            n2.Value = -1;
        }

        int i = 0;

        while (i < n1.Nodes.Count() && i < n2.Nodes.Count())
        {
            if (n1.Nodes[i].Value != -1 && n2.Nodes[i].Value != -1)
            {
                if (n1.Nodes[i].Value < n2.Nodes[i].Value)
                {
                    Console.WriteLine($"xCompare {n1.Nodes[i].Value} vs {n2.Nodes[i].Value}");
                    Console.WriteLine("xLeft side is smaller, so inputs are in the right order");
                    return 1;
                }
                
                if (n1.Nodes[i].Value > n2.Nodes[i].Value)
                {
                    Console.WriteLine($"yCompare {n1.Nodes[i].Value} vs {n2.Nodes[i].Value}");
                    Console.WriteLine("yRight side is smaller, so inputs are not in the right order");
                    return -1;
                }
            }

            var result = isInOrder(n1.Nodes[i], n2.Nodes[i]); 
            
            if (result != 0)
                return result;

            i++;
        }

        if (n1.Nodes.Count > n2.Nodes.Count)
        {
            Console.WriteLine("Right side ran out of items, so inputs are not in the right order");
            return -1;
        }
        else if(n1.Nodes.Count < n2.Nodes.Count)
        {
            Console.WriteLine("Left side ran out of items, so inputs are in the right order");
            return 1;
        }

        Console.WriteLine("same");
        return 0;
    }

    static bool isInOrder(string s1, string s2)
    {
        var i1 = 0;
        var i2 = 0;

        var n1 = getNode(s1, ref i1, null);
        var n2 = getNode(s2, ref i2, null);

        printNode(n1);
        Console.WriteLine();
        printNode(n2);
        Console.WriteLine();

        var result = isInOrder(n1, n2) == 1;

        return result;
    }

    static void Main()
    {
        var lines = File.ReadAllLines("/Users/tpetrovic/Projects/aoc/day13/input.txt").ToArray();

        var result = 0;

        var nodes = new List<Node>();

        for (var i = 0; i < lines.Count(); i++)
        {
            if(lines[i].Trim() == "") continue;
            
            var i1 = 0;
            var n1 = getNode(lines[i], ref i1, null);

            n1.Text = lines[i];
            
            nodes.Add(n1);
        }
        
        nodes.Sort((n1, n2) => isInOrder(n1, n2) * -1);

        foreach (var node in nodes)
        {
            printNode(node);
            Console.WriteLine();
        }
        
        Console.WriteLine(nodes.IndexOf(nodes.FirstOrDefault(e => e.Text == "[[2]]")) + 1);
        Console.WriteLine(nodes.IndexOf(nodes.FirstOrDefault(e => e.Text == "[[6]]")) + 1);
    }
}