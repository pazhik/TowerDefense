using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private Node[,] m_Nodes;
        
        private int m_Height;
        private int m_Width;

        private FlowFieldPathfinding m_Pathfinding;

        public int Height => m_Height;

        public int Width => m_Width;
        

        public Grid(int width, int height, Vector3 offset, float nodeSize, Vector2Int target, Vector2Int start)
        {

            m_Height = height;
            m_Width = width;

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
        
        public void TryOccupyNode(Vector2Int coordinate, ref bool occupy)
        {
            Node node = GetNode(coordinate.x, coordinate.y);
            node.IsOccupied = !node.IsOccupied;
            occupy = m_Pathfinding.CanOccupy(coordinate);
            if (!occupy)
            {
                node.IsOccupied = !node.IsOccupied;   
            }
        }
    }
}