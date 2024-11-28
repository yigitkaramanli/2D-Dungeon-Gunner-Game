using UnityEngine;

public class GridNodes
{
    private readonly Node[,] gridNode;
    private readonly int height;
    private int width;

    public GridNodes(int width, int height)
    {
        this.width = width;
        this.height = height;

        gridNode = new Node[width, height];
        for (var x = 0; x < width; x++)
        {
            for (var y = 0; y < this.height; y++)
            {
                gridNode[x, y] = new Node(new Vector2Int(x, y));
            }
        }
    }

    public Node GetGridNode(int xPosition, int yPosition)
    {
        if (xPosition < width && yPosition < height)
        {
            return gridNode[xPosition, yPosition];
        }
        else
        {
            Debug.Log("Requested grid Node is out of range");
            return null;
        }
    }
}