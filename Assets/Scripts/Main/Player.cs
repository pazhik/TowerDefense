using System;
using System.Collections.Generic;
using Enemy;
using Field;
using RunTime;
using Turret;
using TurretSpawn;
using UnityEngine;
using Grid = Field.Grid;
using Object = UnityEngine.Object;

namespace Main
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

        private bool m_AllWavesSpawned = false;
        private int m_Health;

        public int Health => m_Health;
        public event Action<int> HealthChanged;

        public Player()
        {
            GridHolder = Object.FindObjectOfType<GridHolder>();
            GridHolder.CreateGrid();
            Grid = GridHolder.Grid;
            TurretMarket = new TurretMarket(Game.CurrentLevel.TurretMarketAsset);
            m_Health = Game.CurrentLevel.StartHealth;
        }

        public void EnemySpawned(EnemyData data)
        {
            m_EnemyDatas.Add(data);
        }

        public void TurretSpawn(TurretData turret)
        {
            m_TurretDatas.Add(turret);
        }

        public void LastWaveSpawned()
        {
            m_AllWavesSpawned = true;
        }

        public void ApplyDamage(int damage)
        {
            m_Health -= damage;
            HealthChanged?.Invoke(m_Health);
        }

        public void EnemyDied(EnemyData data)
        {
            m_EnemyDatas.Remove(data);
        }

        public void EnemyReachedTarget(EnemyData enemy)
        {
            m_EnemyDatas.Remove(enemy);
        }

        private void GameWon()
        {
            Debug.Log("Victory");
        }

        public void CheckForWin()
        {
            if (m_AllWavesSpawned && m_EnemyDatas.Count == 0)
            {
                GameWon();
            }
        }

        public void CheckForLose()
        {
            if (m_Health < 0)
            {
                GameLost();
            }
        }
        
        private void GameLost()
        {
            Game.StopPlayer();
            Debug.Log("Lose!");
        }
    }
}