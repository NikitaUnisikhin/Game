using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    public MazeGeneratorCell[,] cells;
    public List<MazePoint> ghosts;
    public Vector2Int finishPosition;
}

public class MazePoint
{
    public int X;
    public int Y;

    public MazePoint(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool Visited = false;
    public int DistanceFromStart;
}