using System;
using UnityEngine;

public class Node : IComparable<Node>
{
    public int gCost = 0; //Distance from the starting node
    public Vector2Int gridPosition;
    public int hCost = 0; //Distance from the finishing node
    public Node parentNode;

    public Node(Vector2Int gridPosition)
    {
        this.gridPosition = gridPosition;

        parentNode = null;
    }

    public int FCost
    {
        get { return hCost + gCost; }
    }

    public int CompareTo(Node nodeToCompare)
    {
        //compare will be <0 if this instance FCost is less than the nodeToCompare.FCost
        //compare will be >0 if this instance FCost is more than the nodeToCompare.FCost
        //compare will be ==0 if the values are the same.

        var compare = FCost.CompareTo(nodeToCompare.FCost);

        if (compare == 0) compare = hCost.CompareTo(nodeToCompare.hCost);
        return compare;
    }
}