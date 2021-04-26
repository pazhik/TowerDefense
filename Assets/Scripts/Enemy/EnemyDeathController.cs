using System.Collections.Generic;
using RunTime;
using UnityEngine;

namespace Enemy
{
    public class EnemyDeathController: IController
    {
        private List<EnemyData> m_DiedEnemyDatas = new List<EnemyData>();
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            foreach (EnemyData playerEnemyData in Game.Player.EnemyDatas)
            {
                if (playerEnemyData.isDead)
                {
                    m_DiedEnemyDatas.Add(playerEnemyData);
                    Game.Player.TurretMarket.GetReward(playerEnemyData);
                    // Debug.Log("Added money");
                }
            }
            
            foreach (var diedEnemyData in m_DiedEnemyDatas)
            {
                Game.Player.EnemyDied(diedEnemyData);
                diedEnemyData.Die();
            }
            
            m_DiedEnemyDatas.Clear();
        }
    }
}