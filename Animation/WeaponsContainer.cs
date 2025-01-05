using System;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponsContainer : EntityContainerModule
    {
        public WeaponEntity SpawnWeapon(WeaponDataObject weaponDataObject)
        {
            var weaponEntity = Object.Instantiate(weaponDataObject.WeaponEntity);
            AddElement(weaponEntity);
            return weaponEntity;
        }
    }
}