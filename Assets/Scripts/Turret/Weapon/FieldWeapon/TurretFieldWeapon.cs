using System.Collections.Generic;
using Enemy;
using Field;
using JetBrains.Annotations;
using RunTime;
using UnityEngine;

namespace Turret.Weapon.FieldWeapon
{
    public class TurretFieldWeapon: ITurretWeapon
    {
        private TurretView m_View;
        
        private float m_DamagePerFrame = 10 * Time.deltaTime;
        private float m_MaxDistance = 6;
        
        private List<Node> m_rangeNodes;
        private GameObject m_Field;
        
        public TurretFieldWeapon(TurretFieldWeaponAsset asset, TurretView view)
        {
            m_View = view;
            m_Field = Object.Instantiate(asset.FieldPrefab, m_View.transform.position, Quaternion.identity);
            Renderer renderer = m_Field.GetComponent<Renderer>();
            Color color = new Color(100, 100, 100, 0.5f);
            renderer.material.color = color;
            m_rangeNodes = Game.Player.Grid.GetNodesInCircle(m_View.ProjectileOrigin.position, m_MaxDistance);
        }

        public void TickShoot()
        {
            foreach (Node node in m_rangeNodes)
            {
                foreach (EnemyData enemyData in node.EnemyDatas)
                {
                    enemyData.GetDamage(m_DamagePerFrame);
                }
            }
        }
    }
}