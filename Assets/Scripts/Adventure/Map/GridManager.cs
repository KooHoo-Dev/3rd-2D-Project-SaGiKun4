using System.Collections.Generic;
using Jay;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    /*
     * !몹마다 지역배치 해줄꺼라 개별로 있어야함. 인스턴스로 쓰면 안됨!
    */

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private BoxCollider2D area;

    private Node[,] grid;

    private int gridSizeX;
    private int gridSizeY;

    protected void Awake()
    {
        Bounds bounds = area.bounds;
        gridSizeX = Mathf.RoundToInt(bounds.size.x / cellSize);
        gridSizeY = Mathf.RoundToInt(bounds.size.y / cellSize);

        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];

        Bounds bounds = area.bounds;

        Vector2 worldBottomLeft = new Vector2(bounds.min.x,bounds.min.y);

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                float xPos = (x * cellSize) + (cellSize / 2f);
                float yPos = (y * cellSize) + (cellSize / 2f);
                Vector2 nodeWorldPoint = worldBottomLeft + (Vector2.right * xPos) + (Vector2.up * yPos);

                bool walkable = !Physics2D.OverlapBox(nodeWorldPoint, Vector2.one * cellSize, 0f, wallLayer);

                grid[x, y] = new Node(walkable, nodeWorldPoint, x, y);
            }
        }
    }

    // private void OnDrawGizmos()
    // {
    //     if (grid == null)
    //         return;

    //     foreach (Node node in grid)
    //     {
    //         Gizmos.color = node.walkable ? Color.green : Color.red;
    //         Gizmos.DrawWireCube(node.worldPosition, Vector2.one * cellSize);
    //     }
    // }

    public Node NodePositionFromWorldPoint(Vector2 worldPosition)
    {
        Bounds bounds = area.bounds;

        int x = Mathf.FloorToInt((worldPosition.x - bounds.min.x) / cellSize );

        int y = Mathf.FloorToInt((worldPosition.y - bounds.min.y) / cellSize);

        x = Mathf.Clamp(x, 0, gridSizeX - 1);
        y = Mathf.Clamp(y, 0, gridSizeY - 1);

        return grid[x, y];
    }

    public List<Node> GetNeighbours(Node node)
    {
        List<Node> neighbours = new List<Node>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }
}
