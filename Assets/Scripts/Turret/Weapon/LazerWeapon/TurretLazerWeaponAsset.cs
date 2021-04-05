using Turret.Weapon.Projectile;
using UnityEngine;

namespace Turret.Weapon.LazerWeapon
{
    [CreateAssetMenu(menuName = "Assets/Turret Laser Weapon Asset", fileName = "Turret Laser Weapon")]
    public class TurretLazerWeaponAsset: TurretWeaponAssetBase
    {
        public LineRenderer LineRendererPrefab;
    
        public override ITurretWeapon GetWeapon(TurretView view)
        {
            return new TurretLazerWeapon(this, view);
        }
    }
}