namespace Stronghold_Crusader_Project.Code.Units.PathFinding;

/// <summary>
/// Node is used to represent a possible position when using path finding
/// 
/// </summary>

public class Node
{
    //Class Variables
    
    public Vector2 Position;
    public Node PreviousNode;
    public float CostFromStart;
    public float CostFromEnd;
    public float TotalCost => CostFromStart + CostFromEnd;

    //Class Methods

    public Node(Vector2 InputPosition, float InputCostFromStart, float InputCostFromEnd, Node InputPreviousNode)
    {
        Position = InputPosition;
        CostFromStart = InputCostFromStart;
        CostFromEnd = InputCostFromEnd;
        PreviousNode = InputPreviousNode;
    }
    
    #region Public Facing
    //Classes that can be used outside of this class

    public static float EstimatedDistance(Vector2 Start, Vector2 End) //Estimates the distance between two vectors
    {
        
    }
    
    #endregion
}
