namespace ClassLib;

public class Node
{
    public required int X { get; init; }

    public required int Y { get; init; }

    public Node? North { get; set; }

    public Node? East { get; set; }

    public Node? South { get; set; }

    public Node? West { get; set; }
}
