using RunTime;

namespace Turret.Weapon.Projectile
{
    public class TurretShootController: IController
    {
        public void OnStart()
        {
        }

        public void OnStop()
        {
        }

        public void Tick()
        {
            foreach (TurretData playerTurretData in Game.Player.TurretDatas)
            {
                playerTurretData.Weapon.TickShoot();
            }
        }
    }
}