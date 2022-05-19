using UnityEngine;

public class HintRenderer : MonoBehaviour
{
    public MazeSpawner MazeSpawner;
    public GameObject Finish;
    public GameObject Ghost;

    private LineRenderer componentLineRenderer;

    private void Start()
    {
        componentLineRenderer = GetComponent<LineRenderer>();
    }

    public void DrawFinish()
    {
        Maze maze = MazeSpawner.maze;
        int x = maze.finishPosition.x;
        int y = maze.finishPosition.y;
        Instantiate(Finish, new Vector3(x, y, 0), Quaternion.identity);
    }

    public void AddGhosts()
    {
        foreach (var cell in MazeSpawner.maze.ghostsG)
        {
            Instantiate(Ghost, new Vector3(cell.X - 0.5f, cell.Y - 2.5f, 0), Quaternion.identity);
        }
    }
}