﻿using System.Collections;
using Assets;
using RunTime;
using UnityEditor;
using UnityEngine;

namespace Enemy
{
    public class EnemyData
    {
        private float m_Health;
        private EnemyView m_View;

        private EnemyAsset m_Asset;
        public EnemyAsset Asset => m_Asset;
        public EnemyView View => m_View;

        public bool isDead => m_Health <= 0;

        public EnemyData(EnemyAsset asset)
        {
            m_Asset = asset;
            m_Health = asset.StartHealth;
        }

        public void AttachView(EnemyView view)
        {
            m_View = view;
            m_View.AttachData(this);
        }

        public void GetDamage(float damage)
        {

            if (isDead)
            {
                 return;
            }
            m_Health -= damage;
            
        }

        public void Die()
        {
            m_View.Die();
            Game.Player.EnemyDied(this);
            m_View.MovementAgent.Die();
            Debug.Log("Die");
        }
        
        public void ReachedTarget()
        {
            m_Health = 0;
            View.ReachedTarget();
        }
    }
}