﻿using UnityEngine;
using Grid = Field.Grid;

namespace Enemy
{
    public class EnemyView : MonoBehaviour

    {
        private EnemyData m_Data;
        private IMovementAgent m_MovementAgent;
        [SerializeField] private Animator m_Animator;

        public EnemyData Data => m_Data;
        public IMovementAgent MovementAgent => m_MovementAgent;

        public void AttachData(EnemyData data)
        {
            m_Data = data;
        }

        public void CreateMovementAgent(Grid grid)
        {
            if (m_Data.Asset.IsFlyingEnemy)
            {
                m_MovementAgent = new FlyingMovementAgent(m_Data.Asset.Speed, transform, grid, m_Data);
            }
            else
            {
                m_MovementAgent = new GridMovementAgent(m_Data.Asset.Speed, transform, grid, m_Data);
            }
        }

        public void Die()
        {
            m_Animator.SetTrigger("Died");
            Destroy(gameObject, 3f);
        }
        
        public void ReachedTarget()
        {
            Destroy(gameObject);
        }
    }
}