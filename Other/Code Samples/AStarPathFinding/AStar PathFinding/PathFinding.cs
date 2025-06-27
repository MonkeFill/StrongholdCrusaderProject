using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AStar_PathFinding;

public class PathFind
{
    public static List<Coordinate> FindPath(Char[,] Grid, Coordinate StartCoordinate, Coordinate EndCoordinate)
    {
        
        int GridMaxY = Grid.GetLength(0) - 1;
        int GridMaxX = Grid.GetLength(1) - 1;
        List<Node> NodesToBeEvaluated = new List<Node>();
        HashSet<Node> EvaluatedNodes = new HashSet<Node>(); //Hash sets are faster to access
        Node StartNode =  new Node(StartCoordinate, 0, Node.EstimatedDistance(StartCoordinate, EndCoordinate)); //Setting up our first node which is where we are currently at
        Node EndNode = new Node(EndCoordinate, 0, 0); //Adding an End node so I can use methods and different variables on it
        AddNodeAndSort(NodesToBeEvaluated, StartNode);
        while (NodesToBeEvaluated.Count > 0) //Loop to go through all the nodes needed to be evaluated
        {
            Node CurrentNode = NodesToBeEvaluated[0];
            NodesToBeEvaluated.Remove(CurrentNode);
            EvaluatedNodes.Add(CurrentNode);
            if (CurrentNode.Position.Equals(EndNode.Position)) //If we have reached our end node
            {
                return ReconstructPath(CurrentNode);
            }
            for (int DifferenceX = -1; DifferenceX < 2; DifferenceX++) //Checking the current nodes neighbours
            {
                for (int DifferenceY = -1; DifferenceY < 2; DifferenceY++) 
                {
                    Coordinate NeighbourCoordinate = new Coordinate(DifferenceX + CurrentNode.Position.X, DifferenceY + CurrentNode.Position.Y, true);
                    if (NeighbourCoordinate.X < 0 || NeighbourCoordinate.X > GridMaxX || NeighbourCoordinate.Y < 0 || NeighbourCoordinate.Y > GridMaxY) //Checking if neighbours are out of bounds or not walkable anyways 
                    {
                        continue;
                    }
                    if (Grid[NeighbourCoordinate.Y, NeighbourCoordinate.X] == 'W')
                    {
                        continue;
                    }
                    if (DifferenceX != 0 && DifferenceY != 0) //If it is a diagnal move
                    {
                        if (Grid[CurrentNode.Position.Y, NeighbourCoordinate.X] == 'W' || Grid[NeighbourCoordinate.Y, CurrentNode.Position.X] == 'W') //Blocked by walls next to it
                        {
                            continue;
                        }
                    } //Neighbours coordinate
                    float MoveCost = 1;
                    if (DifferenceX != 0 && DifferenceY != 0)
                    {
                        MoveCost = 1.414f;
                    }
                    float NewCostFromStart = CurrentNode.CostFromStart + MoveCost; //Adding 1 as it is 1 further away 
                    Node NeighbourNode = new Node(NeighbourCoordinate, NewCostFromStart, Node.EstimatedDistance(NeighbourCoordinate, EndCoordinate)); //Creating the new noode
                    if (EvaluatedNodes.Contains(NeighbourNode)) //Skips if it has alreayd be evaluated
                    {
                        continue;
                    }
                    int ExistingNodeIndex = NodesToBeEvaluated.FindIndex(CurrentNode => CurrentNode.Equals(NeighbourNode)); //Getting the index of where the node is
                    if (ExistingNodeIndex == -1) //Doesn't exist in list
                    {
                        NeighbourNode.PreviousNode = CurrentNode; //Assinging it its parent node
                        AddNodeAndSort(NodesToBeEvaluated, NeighbourNode); //Adding neighbour node to the list
                    }
                    else
                    {
                        Node ExistingNode = NodesToBeEvaluated[ExistingNodeIndex];
                        if (NewCostFromStart < ExistingNode.CostFromStart) //checking to see if the path we found is more efficient
                        {
                            ExistingNode.CostFromStart = NewCostFromStart; //Adding the more optimal cost
                            ExistingNode.PreviousNode = CurrentNode; //Adding the more optimal parent
                            NodesToBeEvaluated.RemoveAt(ExistingNodeIndex); //Removing the old node
                            AddNodeAndSort(NodesToBeEvaluated, ExistingNode); //Adding the new node
                        }
                    }
                }
            }
        }
        return new List<Coordinate>(); //No path found
    }

    private static void AddNodeAndSort(List<Node> NodesToBeEvaluated, Node NodeToAdd)
    {
        int NodeIndex = NodesToBeEvaluated.FindIndex(CurrentNode => CurrentNode.TotalCost > NodeToAdd.TotalCost); //Checking to see which part of the list it should be added in, ordered by total cost
        if (NodeIndex == -1) //If the no nodes are bigger then it will be added at the end as its the biggest
        {
            NodesToBeEvaluated.Add(NodeToAdd);
        }
        else
        {
            NodesToBeEvaluated.Insert(NodeIndex, NodeToAdd); //takes the place of the node its smaller then and the rest shift up that are bigger
        }
    }

    private static List<Coordinate> ReconstructPath(Node EndNode) //Recreate the best path
    {
        List<Coordinate> Path = new List<Coordinate>(); //New Path list
        Node CurrentNode = EndNode; //Final node
        while (CurrentNode != null) //Going through all the nodes for their previous positions
        {
            Path.Add(CurrentNode.Position);
            CurrentNode = CurrentNode.PreviousNode;
        }
        Path.Reverse();
        return Path;
    }
}
