using System;
using Enemy;
using RunTime;
using Turret;
using UnityEngine;

namespace TurretSpawn
{
    public class TurretMarket
    {
        private TurretMarkerAsset m_Asset;
        private int m_Money;

        public int Money => m_Money;

        public event Action<int> MoneyChanged;

        public TurretMarket(TurretMarkerAsset asset)
        {
            m_Asset = asset;
            m_Money = Game.CurrentLevel.StartMoney;
        }

        public TurretAsset ChosenTurret
            => m_Money < m_Asset.TurretAssets[0].Price ? null : m_Asset.TurretAssets[0];

        public void BuyTurret(TurretAsset turretAsset)
        {
            if (turretAsset.Price > m_Money)
            {
                Debug.Log("Not enough money");
                return;
            }
            m_Money -= turretAsset.Price;
            MoneyChanged?.Invoke(m_Money);
        }

        public void GetReward(EnemyData data)
        {
            m_Money += data.Asset.Reward;
            MoneyChanged?.Invoke(m_Money);
        }
    }
}