using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using NUnit.Framework;
using RunTime;
using Turret.Weapon.Projectile;
using UnityEngine;

namespace Turret.Weapon.LazerWeapon
{
    public class TurretLazerWeapon : ITurretWeapon
    {
        private LineRenderer m_LineRenderer;
        private TurretView m_View;
        [CanBeNull]
        private EnemyData m_ClosestEnemy;
        private float m_MaxDistance = 15;
        private float m_Damage = 20 * Time.deltaTime;
        private List<Node> m_rangeNodes;

        public TurretLazerWeapon(TurretLazerWeaponAsset asset, TurretView view)
        {
            m_View = view;
            m_LineRenderer = Object.Instantiate(asset.LineRendererPrefab, m_View.transform.position, Quaternion.identity);
            m_rangeNodes = Game.Player.Grid.GetNodesInCircle(m_View.ProjectileOrigin.position, m_MaxDistance);
        }

        public void TickShoot()
        {
            m_ClosestEnemy = EnemySearch.GetClosestEnemy(m_View.transform.position, m_MaxDistance, m_rangeNodes);
            if (m_ClosestEnemy == null)
            {
                m_LineRenderer.gameObject.SetActive(false);
            } else {
                TickTower();
                Vector3 originPosition = m_View.ProjectileOrigin.position;
                m_LineRenderer.transform.position = originPosition;
                m_LineRenderer.SetPosition(1, m_ClosestEnemy.View.transform.position - originPosition);
                m_LineRenderer.gameObject.SetActive(true);
                m_ClosestEnemy.GetDamage(m_Damage);
            }
        }
        
        private void TickTower()
        {
            if (m_ClosestEnemy != null)
            {
                m_View.TowerLookAt(m_ClosestEnemy.View.transform.position);
            }
        }
    }
    
}
