using Field;
using RunTime;
using Turret;
using UnityEngine;
using Grid = Field.Grid;

namespace TurretSpawn
{
    public class TurretSpawnController: IController
    {
        private Grid m_Grid;
        private TurretMarket m_Market;

        public TurretSpawnController(Grid grid, TurretMarket turretMarket)
        {
            m_Grid = grid;
            m_Market = turretMarket;
        }
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            if (m_Grid.HasSelectedNode() && Input.GetMouseButtonDown(0))
            {
                Node selectedNode = m_Grid.GetSelectedNode();
                bool canOccupy = false;
                m_Grid.TryOccupyNode(selectedNode, ref canOccupy);
                if (!canOccupy)
                {
                    return;
                }

                TurretAsset asset = m_Market.ChosenTurret;
                if (asset != null)
                {
                    m_Market.BuyTurret(asset);
                    SpawnTurret(asset, selectedNode);
                    m_Grid.UpdatePathFinding();
                }
                else
                {
                    Debug.Log("Not enough money");
                }
            }
        }

        public void SpawnTurret(TurretAsset asset, Node node)
        {
            TurretView view = Object.Instantiate(asset.ViewPrefab);
            TurretData data = new TurretData(asset, node);
            
            data.AttachView(view);
            Game.Player.TurretSpawn(data);

            node.IsOccupied = true; 
            m_Grid.UpdatePathFinding();
        }
    }
}