﻿using UnityEngine;

namespace Turret
{
    public class TurretView: MonoBehaviour
    {
        [SerializeField] private Transform m_ProjectileOrigin;

        [SerializeField] private Animator m_Animator;

        [SerializeField] private Transform m_Tower;
        
        private TurretData m_Data;
        private static readonly int ShotAnimationIndex = Animator.StringToHash("Shot");

        public Transform ProjectileOrigin => m_ProjectileOrigin;

        public TurretData Data => m_Data;

        public void AttachData(TurretData turretData)
        {
            m_Data = turretData;
            transform.position = m_Data.Node.Position;
        }

        public void TowerLookAt(Vector3 point)
        {
            point.y = m_Tower.position.y;
            m_Tower.LookAt(point);
        }

        public void AnimateShot()
        {
            m_Animator.SetTrigger(ShotAnimationIndex);
        }
    }
}