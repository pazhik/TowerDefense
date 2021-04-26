using System.Collections.Generic;
using Field;
using NUnit.Framework;
using RunTime;

namespace Enemy
{
    public class EnemyReachController: IController
    {
        private Node m_TargetNode;
        private List<EnemyData> m_ReachedEnemyDatas = new List<EnemyData>(); 

        public EnemyReachController(Grid grid)
        {
            m_TargetNode = grid.GetTargetNode();
        }
        public void OnStart()
        {
            
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            foreach (EnemyData enemyData in Game.Player.EnemyDatas)
            {
                if (enemyData.isDead)
                {
                    continue;
                }
                if (enemyData.View.MovementAgent.GetCurrentNode() == m_TargetNode)
                {
                    Game.Player.ApplyDamage(enemyData.Asset.Damage);
                    m_ReachedEnemyDatas.Add(enemyData);
                    enemyData.ReachedTarget(); 
                }
            }
            
            foreach (EnemyData enemyData in m_ReachedEnemyDatas)
            {
                Game.Player.EnemyReachedTarget(enemyData);
            }
            
            m_ReachedEnemyDatas.Clear();
        }
    }
}