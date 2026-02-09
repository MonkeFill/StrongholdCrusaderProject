namespace Stronghold_Crusader_Project.Code.Units.PathFinding;

/// <summary>
/// PathFinding is a clss that will be used for path finding within my game
/// it will use nodes and the tileset to find appropirate tiles and paths
/// </summary>
/// 

public class Pathing
{
    Node[,] NodeSet;
    int CurrentSearchID = 0;
    Tile[,] TileSet;

    public Pathing()
    {
        NodeSet = new Node[MapDimensions.X, MapDimensions.Y];
        for (int PositionX = 0; PositionX < MapDimensions.X; PositionX++)
        {
            for (int PositionY = 0; PositionY < MapDimensions.Y; PositionY++)
            {
                NodeSet[PositionX, PositionY] = new Node(new Point(PositionX, PositionY), 0, 0);
            }
        }
    }
    
    #region Public Facing
    //Methods that can be accessed outside of this class

    public List<Point> FindPath(Point Start, Point End, Tile[,] MapTiles) //Finds the path from the start node to the end node
    {
        TileSet = MapTiles;
        if(!CheckIfPointValid(Start) || !CheckIfPointValid(End)) //Making sure both nodes are valid
        {
            return null;
        }

        CurrentSearchID++;
        List<Node> OpenList = new List<Node>(); //List of nodes to look through

        Node StartNode = NodeSet[Start.X, Start.Y];
        Node EndNode = NodeSet[End.X, End.Y];

        StartNode.CostFromStart = 0;
        StartNode.CostFromEnd = StartNode.EstimatedDistance(Start, End);
        StartNode.PreviousNode = null;
        StartNode.LastSearchID = CurrentSearchID;
        OpenList.Add(StartNode);

        while (OpenList.Count > 0) //Loops until there is no more nodes left to look through
        {
            Node ActiveNode = OpenList[0];
            for (int Count = 1; Count < OpenList.Count; Count++)
            {
                if (OpenList[Count].TotalCost > ActiveNode.TotalCost) //If it is more expensive
                {
                    continue;
                }
                else if (OpenList[Count].TotalCost != ActiveNode.TotalCost || OpenList[Count].CostFromEnd > ActiveNode.CostFromEnd) //If the node is further away from the destination
                {
                    continue;
                }
                ActiveNode = OpenList[Count];
            }
            OpenList.Remove(ActiveNode);

            if (ActiveNode.Position == End) //If the node is the end node
            {
                return RetracePath(StartNode, EndNode);
            }

            foreach (Point ActiveNeighbourPosition in GetNeighbours(ActiveNode.Position))
            {
                Node ActiveNeighbourNode = NodeSet[ActiveNeighbourPosition.X, ActiveNeighbourPosition.Y];
                if (ActiveNeighbourNode.LastSearchID == CurrentSearchID && !OpenList.Contains(ActiveNeighbourNode)) //if the node has already been visited
                {
                    continue;
                }

                int MoveCost = ActiveNode.EstimatedDistance(ActiveNode.Position, ActiveNeighbourPosition);
                int NewMovementCost = ActiveNode.CostFromStart + MoveCost;

                if (NewMovementCost < ActiveNeighbourNode.CostFromStart || ActiveNeighbourNode.LastSearchID != CurrentSearchID) //if node is cheaper and hasn't been visited
                {
                    ActiveNeighbourNode.CostFromStart = NewMovementCost;
                    ActiveNeighbourNode.CostFromEnd = ActiveNeighbourNode.EstimatedDistance(ActiveNeighbourPosition, End);
                    ActiveNeighbourNode.PreviousNode = ActiveNode;
                    ActiveNeighbourNode.LastSearchID = CurrentSearchID;

                    if (!OpenList.Contains(ActiveNeighbourNode))
                    {
                        OpenList.Add(ActiveNeighbourNode);
                    }
                }
            }
        }
        return null;
    }
    
    #endregion
    
    #region Helper Classes
    //Methods that will help this class

    private bool CheckIfWalkable(Point Position) //A class to check if a tile is walkable
    {
        return TileSet[Position.X, Position.Y].IsWalkable;
    }

    private List<Point> RetracePath(Node StartNode, Node EndNode) //Rebuilds the path when the final node is found
    {
        List<Point> Path = new List<Point>();
        Node CurrentNode = EndNode;
        while (CurrentNode != StartNode)
        {
            Path.Add(CurrentNode.Position);
            CurrentNode = CurrentNode.PreviousNode;
        }
        Path.Reverse();
        return Path;
    }

    public List<Point> GetNeighbours(Point Position) //returns all Neighbours to the position that are valid
    {
        List<Point> ValidNeighbours = new List<Point>();
        for (int CountX = -1; CountX <= 1; CountX++)
        {
            for (int CountY = -1; CountY <= 1; CountY++)
            {
                if (CountX == 0 && CountY == 0)
                {
                    continue;
                }
                Point ActivePoint = new Point(Position.X + CountX, Position.Y + CountY);
                if (CheckIfPointValid(ActivePoint))
                {
                    ValidNeighbours.Add(ActivePoint);
                }
            }
        }
            
        return ValidNeighbours;
    }

    private bool CheckIfPointValid(Point Position) //Check if a position is within the grid of tiles
    {
        if (Position.X < 0 || Position.X >= MapDimensions.X)
        {
            return false;
        }
        if (Position.Y < 0 || Position.Y >= MapDimensions.Y)
        {
            return false;
        }
        
        if (!CheckIfWalkable(Position))
        {
            return false;
        }
        return true;
    }
    
    #endregion
}