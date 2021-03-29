﻿using System;
using Enemy;
using UnityEngine;

namespace Turret.Weapon.Projectile
{
    public class BulletProjectile: MonoBehaviour, IProjectile
    {
        private float m_Speed = 5;
        private bool m_DidHit = false;
        private EnemyData m_HitEnemy = null;
        private int m_Damage = 10;
        public void TickApproaching()
        {
            transform.Translate(transform.forward * (m_Speed * Time.deltaTime), Space.World);
        }

        private void OnTriggerEnter(Collider other)
        {
            m_DidHit = true;
            if (other.CompareTag("Enemy"))
            {
                EnemyView enemyView = other.GetComponent<EnemyView>();
                if (enemyView != null)
                {
                    m_HitEnemy = enemyView.Data;
                }
            }
        }

        public bool DidHit()
        {
            return m_DidHit;
        }

        public void DestroyProjectile()
        {
            if (m_HitEnemy != null)
            {
                m_HitEnemy.GetDamage(m_Damage);
                Debug.Log("Hit!");
            }
            Destroy(gameObject);
        }
    }
}