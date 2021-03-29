using Field;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class GridMovementAgent: IMovementAgent
    {
        private float m_Speed;

        private const float TOLERANCE = 0.1f;

        private Node m_TargetNode;
        private Node m_CurrentNode;

        private Transform m_Transform;

        private EnemyData m_EnemyData;

        private Grid m_Grid;

        public GridMovementAgent(float speed, Transform transform, Grid grid, EnemyData enemyData)
        {
            m_Speed = speed;
            m_Transform = transform;
            m_EnemyData = enemyData;
            SetStartNode(grid.GetStartNode());
            m_TargetNode.EnemyDatas.Add(m_EnemyData);
            m_Grid = grid;
            m_CurrentNode = m_Grid.GetNodeAtPoint(m_Transform.position);
        }

        public void TickMovement()
        {
            if (m_TargetNode == null)
            {
                return;
            }

            var position = m_Transform.position;
            if (m_Grid.GetNodeAtPoint(position) != m_CurrentNode)
            {
                m_CurrentNode.EnemyDatas.Remove(m_EnemyData);
                m_TargetNode.EnemyDatas.Add(m_EnemyData);
                m_CurrentNode = m_TargetNode;
            }

            Vector3 target = new Vector3(m_TargetNode.Position.x, position.y, m_TargetNode.Position.z);
            float distance = (target - position).magnitude;
            if (distance < TOLERANCE)
            {
                m_TargetNode = m_TargetNode.NextNode;
                return;
            }

            Vector3 dir = (target - m_Transform.position).normalized;
            Vector3 delta = dir * (m_Speed * Time.deltaTime);
            m_Transform.Translate(delta);
        }

        private void SetStartNode(Node node)
        {
            m_TargetNode = node;
        }

        
    }
}