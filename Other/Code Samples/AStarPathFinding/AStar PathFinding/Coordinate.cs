using System;

namespace AStar_PathFinding;

public class Coordinate
{
    //Variables for the coordinate are public so other clases can read them but still cannot be set outside
    public int X {get; private set;}
    public int Y {get; private set;}
    public bool Walkable {get; private set;}

    public Coordinate(int x, int y, bool walkable)
    {
        X = x;
        Y = y;
        Walkable = walkable;
    }

    public override bool Equals(object CheckObject) //Overiding equals so it compares the coordinates 
    {
        if (CheckObject is not Coordinate)
        {
            return false;
        }
        Coordinate CheckCoordinate = (Coordinate)CheckObject;
        if (CheckCoordinate.X != X || CheckCoordinate.Y != Y)
        {
            return false;
        }
        return true;
    }

    public override int GetHashCode() //Ovveride getting hashcode for both incase of using hash sets
    {
        return HashCode.Combine(X, Y);
    }
}
