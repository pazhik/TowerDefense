using Turret;

namespace TurretSpawn
{
    public class TurretMarket
    {
        private TurretMarkerAsset m_Asset;

        public TurretMarket(TurretMarkerAsset asset)
        {
            m_Asset = asset;
        }

        public TurretAsset ChosenTurret => m_Asset.TurretAssets[0];
    }
}