using System;
using System.Collections.Generic;
using Enemy;
using Field;
using Turret;
using Turret.Weapon;
using TurretSpawn;
using Grid = Field.Grid;
using Object = UnityEngine.Object;

namespace RunTime
{
    // Store all scene
    public class Player {
        private List<EnemyData> m_EnemyDatas = new List<EnemyData>();
        public IReadOnlyList<EnemyData> EnemyDatas => m_EnemyDatas;

        private List<TurretData> m_TurretDatas = new List<TurretData>();

        public IReadOnlyList<TurretData> TurretDatas => m_TurretDatas;


        public readonly GridHolder GridHolder;
        public readonly Grid Grid;
        public readonly TurretMarket TurretMarket;

        public Player()
        {
            GridHolder = Object.FindObjectOfType<GridHolder>();
            GridHolder.CreateGrid();
            Grid = GridHolder.Grid;
            TurretMarket = new TurretMarket(Game.CurrentLevel.TurretMarketAsset);

        }

        public void EnemySpawned(EnemyData data)
        {
            m_EnemyDatas.Add(data);
        }

        public void TurretSpawn(TurretData turret)
        {
            m_TurretDatas.Add(turret);
        }
    }
}