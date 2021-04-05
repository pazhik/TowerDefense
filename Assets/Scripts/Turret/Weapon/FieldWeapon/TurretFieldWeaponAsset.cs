
using UnityEngine;

namespace Turret.Weapon.FieldWeapon
{
    [CreateAssetMenu(menuName = "Assets/Turret Field Weapon Asset", fileName = "Turret Field Weapon")]
    public class TurretFieldWeaponAsset: TurretWeaponAssetBase
    {
        public GameObject FieldPrefab;
        public override ITurretWeapon GetWeapon(TurretView view)
        {
            return new TurretFieldWeapon(this, view);
        }
    }
}