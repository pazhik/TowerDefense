using System.Collections.Generic;
using RunTime;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private Node[,] m_Nodes;
        
        private int m_Height;
        private int m_Width;
        
        private Vector2Int m_StartCoordinate;
        private Vector2Int m_TargetCoordinate;

        private Node m_SelectedNode = null;

        private FlowFieldPathfinding m_Pathfinding;

        public int Height => m_Height;

        public int Width => m_Width;
        

        public Grid(int width, int height, Vector3 offset, float nodeSize, Vector2Int start, Vector2Int target)
        {

            m_Height = height;
            m_Width = width;

            m_TargetCoordinate = target;
            m_StartCoordinate = start;

            m_Nodes = new Node[m_Width, m_Height];

            for (int i = 0; i < m_Width; i++)
            {
                for (int j = 0; j < m_Height; j++)
                {
                    m_Nodes[i, j] = new Node(offset + new Vector3(i + .5f, 0 , j + .5f) * nodeSize);
                }
            }
    
            // todo replace zero
            m_Pathfinding = new FlowFieldPathfinding(this, target, start);
            
            m_Pathfinding.UpdateField();
        }
        
        
        public Node GetStartNode()
        {
            return GetNode(m_StartCoordinate);
        }

        public Node GetTargetNode()
        {
            return GetNode(m_TargetCoordinate);
        }

        public void SelectCoordinate(Node node)
        {
            m_SelectedNode = node;
            Debug.Log(m_Nodes[1, 0].Position.x - m_Nodes[0, 0].Position.x);
        }

        public void UnselectNode()
        {
            m_SelectedNode = null;
        }

        public bool HasSelectedNode()
        {
            return m_SelectedNode != null;
        }

        public Node GetSelectedNode()
        {
            return m_SelectedNode;
        }
        
        public Node GetNode(Vector2Int coordinate)
        {
            return GetNode(coordinate.x, coordinate.y);
        }
        
        public Node GetNode(int i, int j)
        {
            if (i < 0 || i >= m_Width)
            {
                return null;
            }

            if (j < 0 || j >= m_Height)
            {
                return null;
            }

            return m_Nodes[i, j];
        }

        public IEnumerable<Node> EnumerableAllNodes()
        {
            for (int i = 0; i < m_Width; i++)
            {
                for (int j = 0; j < m_Height; j++)
                {
                    yield return GetNode(i, j);
                }
                
            }
        }

        public void UpdatePathFinding()
        {
            m_Pathfinding.UpdateField();
        }
        
        public void TryOccupyNode(Node node, ref bool occupy)
        {
            occupy = m_Pathfinding.CanOccupy(node);
            if (occupy)
            {
                node.IsOccupied = !node.IsOccupied;   
            }
        }

        public Node GetNodeAtPoint(Vector3 point)
        {
            float m_NodeSize = m_Nodes[1, 0].Position.x - m_Nodes[0, 0].Position.x;
            Vector3 Offset = m_Nodes[0, 0].Position -  (new Vector3(m_NodeSize, 0f, m_NodeSize) * 0.5f);
            Vector3 difference = point - Offset;
            int x = (int) (difference.x / m_NodeSize);
            int y = (int) (difference.z / m_NodeSize);
            return GetNode(x, y);
        }
        
        public List<Node> GetNodesInCircle(Vector3 point, float radius)
        {
            Vector3 nodeCenter;
            float sqrRadius = radius * radius;
            float halfNodeSize = (m_Nodes[1, 0].Position.x - m_Nodes[0, 0].Position.x) / 2;
            List<Node> nodes = new List<Node>();
            foreach (Node node in m_Nodes)
            {
                nodeCenter = node.Position;
                Vector3 left = nodeCenter + new Vector3(halfNodeSize,0,0);
                Vector3 right = nodeCenter + new Vector3(-halfNodeSize,0,0);
                Vector3 up = nodeCenter + new Vector3(0, 0, halfNodeSize);
                Vector3 down = nodeCenter + new Vector3(0, 0, -halfNodeSize);
                Vector3 leftUp = nodeCenter + new Vector3(halfNodeSize,0,halfNodeSize);
                Vector3 leftDown = nodeCenter + new Vector3(halfNodeSize,0,-halfNodeSize);
                Vector3 rightUp = nodeCenter + new Vector3(-halfNodeSize,0,halfNodeSize);
                Vector3 rightDown = nodeCenter + new Vector3(-halfNodeSize,0,-halfNodeSize);
                float leftDistanceSqr = (left - point).sqrMagnitude;
                float rightDistanceSqr = (right - point).sqrMagnitude;
                float upDistanceSqr = (up - point).sqrMagnitude;
                float downDistanceSqr = (down - point).sqrMagnitude;
                float leftUpDistanceSqr = (leftUp - point).sqrMagnitude;
                float leftDownDistanceSqr = (leftDown - point).sqrMagnitude;
                float rightUpDistanceSqr = (rightUp - point).sqrMagnitude;
                float rightDownDistanceSqr = (rightDown - point).sqrMagnitude;
                if (leftDistanceSqr < sqrRadius || rightDistanceSqr < sqrRadius ||
                    upDistanceSqr < sqrRadius || downDistanceSqr < sqrRadius ||
                    leftUpDistanceSqr < sqrRadius || leftDownDistanceSqr < sqrRadius ||
                    rightUpDistanceSqr < sqrRadius || rightDownDistanceSqr < sqrRadius)
                {
                    nodes.Add(node);
                }
            }

            return nodes;
        }
    }
}