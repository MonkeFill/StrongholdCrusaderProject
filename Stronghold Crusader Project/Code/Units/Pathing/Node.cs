namespace Stronghold_Crusader_Project.Code.Units.PathFinding;

/// <summary>
/// Node is used to represent a possible position when using path finding
/// 
/// </summary>

public class Node
{
    //Class Variables
    
    public Point Position;
    public Node PreviousNode;
    public int CostFromStart;
    public int CostFromEnd;
    public int LastSearchID;
    public int TotalCost => CostFromStart + CostFromEnd;

    //Class Methods

    public Node(Point InputPosition, int InputCostFromStart, int InputCostFromEnd, Node InputPreviousNode = null)
    {
        Position = InputPosition;
        CostFromStart = InputCostFromStart;
        CostFromEnd = InputCostFromEnd;
        PreviousNode = InputPreviousNode;
    }
    
    #region Public Facing
    //Classes that can be used outside of this class

    public int EstimatedDistance(Point Start, Point End) //Estimates the distance between two vectors
    {

        int DifferenceX = Math.Abs(Start.X - End.X);
        int DifferenceY = Math.Abs(Start.Y - End.Y);

        int DiagonalSteps = Math.Min(DifferenceX, DifferenceY);
        int StraightSteps = Math.Max(DifferenceX, DifferenceY) - DiagonalSteps;

        return (PFDiagonalCost * DiagonalSteps) + (PFStraightCost * StraightSteps);

    }

    public override bool Equals(object NodeToCheck) //Method to check if two nodes positions are the same
    {
        if(NodeToCheck is Node CheckNode) //If the node isn't actually a node
        {
            return Position == CheckNode.Position;
        }
        return false;
    }

    public override int GetHashCode() //Override default hash code so it is only using its position
    {
        return Position.GetHashCode();
    }

    #endregion
}
