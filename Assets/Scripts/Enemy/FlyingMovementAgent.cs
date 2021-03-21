using Field;
using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class FlyingMovementAgent : IMovementAgent
    {
        private float m_Speed;

        private const float TOLERANCE = 0.1f;

        private Node m_TargetNode;

        private Transform m_Transform;

        public FlyingMovementAgent(float speed, Transform transform, Grid grid)
        {
            m_Speed = speed;
            m_Transform = transform;
            SetStartNode(grid.GetTargetNode());
        }

        public void TickMovement()
        {
            if (m_TargetNode == null)
            {
                return;
            }

            Vector3 target = new Vector3(m_TargetNode.Position.x, m_Transform.position.y, m_TargetNode.Position.z);
            
            float distance = (target - m_Transform.position).magnitude;
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

    };
    
}