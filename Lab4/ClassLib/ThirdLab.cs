using System.Text;

namespace ClassLib;

public static class ThirdLab
{
    private const int MinPathLength = 1;
    private const int MaxPathLength = 200;

    private const char North = 'N';
    private const char East = 'E';
    private const char South = 'S';
    private const char West = 'W';

    public static void Execute(string inputFilePath, string outputFilePath)
    {
        string path;
        try
        {
            path = ReadPathFromFile(inputFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while reading file: {e.Message}");
            return;
        }

        string pathBack;
        try
        {
            pathBack = FindPathBack(path);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while finding solution: {e.Message}");
            return;
        }

        try
        {
            WriteResultToFile(pathBack, outputFilePath);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error while writing file: {e.Message}");
        }
    }

    private static string FindPathBack(string originalPath)
    {
        ValidatePath(originalPath);
        var start = BuildGraph(originalPath);
        var visited = new HashSet<Node>();
        var previous = new Dictionary<Node, Node>();
        var queue = new Queue<Node>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            if (current.X == 0 && current.Y == 0)
            {
                return BuildPath(start, current, previous);
            }

            _ = visited.Add(current);
            if (current.North is not null && !visited.Contains(current.North))
            {
                previous[current.North] = current;
                queue.Enqueue(current.North);
            }

            if (current.East is not null && !visited.Contains(current.East))
            {
                previous[current.East] = current;
                queue.Enqueue(current.East);
            }

            if (current.South is not null && !visited.Contains(current.South))
            {
                previous[current.South] = current;
                queue.Enqueue(current.South);
            }

            if (current.West is not null && !visited.Contains(current.West))
            {
                previous[current.West] = current;
                queue.Enqueue(current.West);
            }
        }

        throw new InvalidOperationException();
    }

    private static void ValidatePath(string path)
    {
        if (path.Length is < MinPathLength or > MaxPathLength)
        {
            throw new ArgumentException($"The length of the path should be between {MinPathLength} and {MaxPathLength} characters", nameof(path));
        }

        if (path.Any(static c => c is not (North or East or South or West)))
        {
            throw new ArgumentException($"The path can only contain characters {North}, {East}, {South} or {West}", nameof(path));
        }
    }

    private static Node BuildGraph(string path)
    {
        var nodes = new Dictionary<(int X, int Y), Node>();
        var currentNode = new Node { X = 0, Y = 0 };
        nodes.Add((0, 0), currentNode);
        foreach (var direction in path)
        {
            var (nextX, nextY) = direction switch
            {
                North => (currentNode.X, currentNode.Y + 1),
                East => (currentNode.X + 1, currentNode.Y),
                South => (currentNode.X, currentNode.Y - 1),
                West => (currentNode.X - 1, currentNode.Y),
                _ => throw new InvalidOperationException(),
            };

            if (!nodes.TryGetValue((nextX, nextY), out var nextNode))
            {
                nextNode = new Node { X = nextX, Y = nextY };
                nodes.Add((nextX, nextY), nextNode);
            }

            switch (direction)
            {
                case North:
                    currentNode.North = nextNode;
                    nextNode.South = currentNode;
                    break;
                case East:
                    currentNode.East = nextNode;
                    nextNode.West = currentNode;
                    break;
                case South:
                    currentNode.South = nextNode;
                    nextNode.North = currentNode;
                    break;
                case West:
                    currentNode.West = nextNode;
                    nextNode.East = currentNode;
                    break;
            }

            currentNode = nextNode;
        }

        return currentNode;
    }

    private static string BuildPath(Node start, Node end, Dictionary<Node, Node> previous)
    {
        var path = new StringBuilder();
        var current = end;
        while (current != start)
        {
            var previousNode = previous[current];
            if (previousNode.North == current)
            {
                _ = path.Insert(0, North);
            }
            else if (previousNode.East == current)
            {
                _ = path.Insert(0, East);
            }
            else if (previousNode.South == current)
            {
                _ = path.Insert(0, South);
            }
            else if (previousNode.West == current)
            {
                _ = path.Insert(0, West);
            }

            current = previousNode;
        }

        return path.ToString();
    }

    private static string ReadPathFromFile(string inputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new InputException($"File {inputFilePath} not found.");
        }

        var lines = File.ReadAllLines(inputFilePath)
            .Select(static line => line.Trim())
            .Where(static line => !string.IsNullOrWhiteSpace(line))
            .ToArray();
        if (lines.Length == 0)
        {
            throw new InputException("File does not contain any text");
        }

        if (lines.Length != 1)
        {
            throw new InputException("File contains more than one line with data");
        }

        return lines[0].Trim();
    }

    private static void WriteResultToFile(string path, string outputFilePath)
    {
        File.WriteAllText(outputFilePath, path);
    }
}
