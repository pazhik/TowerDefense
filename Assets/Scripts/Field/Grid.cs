﻿using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Field
{
    public class Grid
    {
        private Node[,] m_Nodes;
        private int m_Height;
        private int m_Width;

        public Grid(int width, int height)
        {
            m_Height = height;
            m_Width = width;

            m_Nodes = new Node[m_Width, m_Height];

            for (int i = 0; i < m_Width; i++)
            {
                for (int j = 0; j < m_Height; j++)
                {
                    m_Nodes[i, j] = new Node();
                }
            }
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
    }
}