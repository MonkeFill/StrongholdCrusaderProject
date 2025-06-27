

using System;

namespace AStar_PathFinding;

public class Node
{
    public Coordinate Position {get; set;}
    public Node PreviousNode {get; set;}
    public float CostFromStart  {get; set;}
    public float CostFromEnd {get; set;}
    public float TotalCost => CostFromStart + CostFromEnd;

    public Node(Coordinate position, float costFromStart, float costFromEnd)
    {
        Position = position;
        CostFromStart = costFromStart;
        CostFromEnd = costFromEnd;
    }

    public static float EstimatedDistance(Coordinate Start, Coordinate End) //Estimates the distance between two points
    {
        //Octile distance - diagonal is not an equal move to straight moves
        int DifferentX = Math.Abs(Start.X - End.X); 
        int DifferentY = Math.Abs(Start.Y - End.Y); //Getting differences in positions
        float StraightCost = 1; //Amount it costs to move straight
        float DiagnalCost = 1.414f; //Amount it costs to move diagnally
        int Min = Math.Min(DifferentX, DifferentY); //Biggest Number
        int Max = Math.Min(DifferentX, DifferentY); //Smallest Number
        return StraightCost * (Max - Min) + DiagnalCost * Min; //Octile distance formula
    }

    public override bool Equals(object PossibleNode) //Ovveriding default check as it doesn't work as it won't work when checking the positions of the nodes to make sure we don't get duplicates
    {
        if (PossibleNode is not Node) //Checking if it is even a node
        {
            return false;
        }
        Node CheckNode = (Node)PossibleNode; //Converting it from an object into a node class
        if (CheckNode.Position.X != Position.X || CheckNode.Position.Y != Position.Y) //Checking if the positions are equal
        {
            return false;
        }
        return true;
    }

    public override int GetHashCode() //Creating a hash code for the position of the node
    {
        return HashCode.Combine(Position.X, Position.Y);
    }
    
}
