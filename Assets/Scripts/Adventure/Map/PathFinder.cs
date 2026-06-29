using System.Collections.Generic;
using Jay;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    public static PathFinder Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public List<Node> FindPath(GridManager grid, Vector2 startPos, Vector2 targetPos)
    {
        Node startNode = grid.NodePositionFromWorldPoint(startPos);
        Node targetNode = grid.NodePositionFromWorldPoint(targetPos);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while (openSet.Count > 0)
        {
            Node currentNode = openSet[0];

            for (int i = 1; i < openSet.Count; i++)
            {
                if (openSet[i].totalCost < currentNode.totalCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if (currentNode == targetNode)
            {
                return RetracePath(startNode, targetNode);
            }

            foreach (Node neighbour in grid.GetNeighbours(currentNode))
            {
                if (!neighbour.walkable)
                    continue;

                if (closedSet.Contains(neighbour))
                    continue;

                int newCost = currentNode.hereCost + GetDistance(currentNode, neighbour);

                bool isNewNode = (!openSet.Contains(neighbour));

                if (newCost < neighbour.hereCost || isNewNode)
                {
                    neighbour.hereCost = newCost;

                    neighbour.remainCost = GetDistance(neighbour, targetNode);

                    neighbour.prevNode = currentNode;

                    if (isNewNode)
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        return null;
    }

    private List<Node> RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new();

        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.prevNode;
        }

        path.Reverse();

        return path;
    }

    private int GetDistance(Node a, Node b)
    {
        int distanceX = Mathf.Abs(a.gridX - b.gridX);
        int distanceY = Mathf.Abs(a.gridY - b.gridY);

        // 에이스타에서 대각선 이동은 14로 근사값을 잡음
        // (실제 거리 기준 직선 거리 1 : 대각선 거리 1.41414...)의 근사값
        // 그거 계산
        if (distanceX > distanceY)
            return 14 * distanceY + 10 * (distanceX - distanceY);

        return 14 * distanceX + 10 * (distanceY - distanceX);
    }
}