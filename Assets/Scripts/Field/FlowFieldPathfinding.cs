using System;
using System.Collections.Generic;
using UnityEngine;

namespace Field
{
    public struct Connection
    {
        public Vector2Int Coordinate;
        public float Weight;

        public Connection(Vector2Int coordinate, float weight)
        {
            Coordinate = coordinate;
            Weight = weight;
        }
    }
    
    public class FlowFieldPathfinding
    {
        private Grid m_Grid;
        private Vector2Int m_Target;
        private Vector2Int m_Start;

        public FlowFieldPathfinding(Grid grid, Vector2Int target, Vector2Int start)
        {
            m_Grid = grid;
            m_Target = target;
            m_Start = start;
        }

        public void UpdateField()
        {
            foreach (Node node in m_Grid.EnumerableAllNodes())
            {
                node.ResetWeight();
                node.m_OccupationAvailability = OccupationAvailability.Undefined;
            }
            
            
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            
            queue.Enqueue(m_Target);
            m_Grid.GetNode(m_Target).PathWeight = 0f;

            Node currentNode;
            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                currentNode = m_Grid.GetNode(current);

                foreach (Connection neighbour in GetNeighbours(current))
                {
                    Node neighbourNode = m_Grid.GetNode(neighbour.Coordinate);
                    if (currentNode.PathWeight + neighbour.Weight < neighbourNode.PathWeight)
                    {
                        neighbourNode.NextNode = currentNode;
                        neighbourNode.PathWeight = currentNode.PathWeight + neighbour.Weight;
                        queue.Enqueue(neighbour.Coordinate);
                    }
                }
            }
            
            foreach (Node node in m_Grid.EnumerableAllNodes())
            {
                node.m_OccupationAvailability = OccupationAvailability.CanOccupy;
            }

            m_Grid.GetNode(m_Start).m_OccupationAvailability = OccupationAvailability.CanNotOccupy;
            currentNode = m_Grid.GetNode(m_Start).NextNode;
            while (currentNode != m_Grid.GetNode(m_Target))
            {
                currentNode.m_OccupationAvailability = OccupationAvailability.Undefined;
                currentNode = currentNode.NextNode;
            }
            m_Grid.GetNode(m_Target).m_OccupationAvailability = OccupationAvailability.CanNotOccupy;

            
        }

        
        public bool CanOccupy(Node tryNode)
        {
            if (tryNode.m_OccupationAvailability == OccupationAvailability.CanNotOccupy)
            {
                return false;
            }
            if (tryNode.m_OccupationAvailability == OccupationAvailability.CanOccupy)
            {
                return true;
            }
            
            Queue<Vector2Int> queue = new Queue<Vector2Int>();
            queue.Enqueue(m_Target);
            int height = m_Grid.Height;
            int width = m_Grid.Width;
            bool[][] nodes = new bool[width][];
            for (int index = 0; index < width; index++)
            {
                nodes[index] = new bool[height];
            }
            
            
            Debug.Log(height + " " + width);
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    nodes[i][j] = true;
                }
            }
            
            while (queue.Count > 0)
            {
                Vector2Int current = queue.Dequeue();
                foreach (Connection neighbour in GetNeighbours(current))
                {
                    int i = neighbour.Coordinate.x;
                    int j = neighbour.Coordinate.y;
                    
                    if (nodes[i][j])
                    {
                        if (m_Start == neighbour.Coordinate)
                        {
                            tryNode.m_OccupationAvailability = OccupationAvailability.CanOccupy;
                            return true;
                        }
                        nodes[i][j] = false;
                        queue.Enqueue(neighbour.Coordinate);
                    }
                }
            }

            tryNode.m_OccupationAvailability = OccupationAvailability.CanNotOccupy;
            return false;
        }
        

        private IEnumerable<Connection> GetNeighbours(Vector2Int coordinate)
        {
            Vector2Int rightCoordinate = coordinate + Vector2Int.right;
            Vector2Int leftCoordinate = coordinate + Vector2Int.left;
            Vector2Int upCoordinate = coordinate + Vector2Int.up;
            Vector2Int downCoordinate = coordinate + Vector2Int.down;
            
            Vector2Int rightUpCoordinate = coordinate + Vector2Int.right + Vector2Int.up;
            Vector2Int leftUpCoordinate = coordinate + Vector2Int.left + Vector2Int.up;
            Vector2Int rightDownCoordinate = coordinate + Vector2Int.down + Vector2Int.right;
            Vector2Int leftDownCoordinate = coordinate + Vector2Int.down + Vector2Int.left;
            
            bool hasRightNode = rightCoordinate.x < m_Grid.Width && !m_Grid.GetNode(rightCoordinate).IsOccupied;
            bool hasLeftNode = leftCoordinate.x >= 0 && !m_Grid.GetNode(leftCoordinate).IsOccupied;
            bool hasUpNode = upCoordinate.y < m_Grid.Height && !m_Grid.GetNode(upCoordinate).IsOccupied;
            bool hasDownNode = downCoordinate.y >= 0 && !m_Grid.GetNode(downCoordinate).IsOccupied;

            bool hasRightUpNode = rightUpCoordinate.x < m_Grid.Width && rightUpCoordinate.y < m_Grid.Height
                                                                     && !m_Grid.GetNode(rightUpCoordinate).IsOccupied
                                                                     && hasRightNode && hasUpNode;
            bool hasLeftUpNode = leftUpCoordinate.x >= 0 && leftUpCoordinate.y < m_Grid.Height
                                                         && !m_Grid.GetNode(leftUpCoordinate).IsOccupied
                                                         && hasLeftNode && hasUpNode;
            bool hasRightDownNode = rightDownCoordinate.x < m_Grid.Width
                                    && rightDownCoordinate.y >= 0 && !m_Grid.GetNode(rightDownCoordinate).IsOccupied
                                    && hasRightNode && hasDownNode;
            bool hasLeftDownNode =  leftDownCoordinate.x >= 0 && leftDownCoordinate.y >= 0 
                                                              && !m_Grid.GetNode(leftDownCoordinate).IsOccupied
                                                              && hasLeftNode && hasDownNode;
            
            if (hasRightNode)
            {
                yield return new Connection(rightCoordinate, 1f);;
            }
            
            if (hasLeftNode)
            {
                yield return new Connection(leftCoordinate, 1f);
            }
            
            if (hasUpNode)
            {
                yield return new Connection(upCoordinate, 1f);
            }
            
            if (hasDownNode)
            {
                yield return new Connection(downCoordinate, 1f);
            }
            
            if (hasRightUpNode)
            {
                yield return new Connection(rightUpCoordinate, (float)Math.Sqrt(2));
            }
            
            if (hasLeftUpNode)
            {
                yield return new Connection(leftUpCoordinate, (float)Math.Sqrt(2));
            }
            
            if (hasRightDownNode)
            {
                yield return new Connection(rightDownCoordinate, (float)Math.Sqrt(2));
            }
            
            if (hasLeftDownNode)
            {
                yield return new Connection(leftDownCoordinate, (float)Math.Sqrt(2));
            }
        }
    }
}