using System;
using System.Collections.Generic;
using Enemy;
using Field;
using RunTime;
using UnityEngine;

namespace Turret.Weapon.Projectile.Rocket
{
    public class RocketProjectile: MonoBehaviour, IProjectile
    {
        private float m_Speed = 10;
        private bool m_DidHit = false;
        private EnemyData m_HitEnemy = null;
        private float m_Damage = 30;
        private float m_ExplosionRange = 2;

        public void SetChasingEnemy(EnemyData enemyData) {
            m_HitEnemy = enemyData;
        }
        
        public void SetAsset(RocketProjectileAsset asset)
        {
            m_Speed = asset.m_Speed;
            m_Damage = asset.m_Damage;
        }

        
        public void TickApproaching()
        {
            Vector3 direction = (m_HitEnemy.View.transform.position - transform.position).normalized;
            transform.Translate(direction * (m_Speed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            m_DidHit = true;
        }

        public bool DidHit()
        {
            return m_DidHit;
        }

        public void DestroyProjectile()
        {
            List<Node> touchedNodes = Game.Player.Grid.GetNodesInCircle(m_HitEnemy.View.transform.position, m_ExplosionRange);
            foreach (Node touchedNode in touchedNodes)
            {
                foreach (EnemyData enemyData in touchedNode.EnemyDatas)
                {
                    enemyData.GetDamage(m_Damage);
                }
            }
            // m_HitEnemy.GetDamage(m_Damage);
            Debug.Log("Hit!");
            Destroy(gameObject);
        }
    }
}