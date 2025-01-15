using UnityEngine;

namespace _Project.Scripts
{
    public class WeaponItem : AbstractUsableItem
    {
        public WeaponItem(AbstractPickableItemDataObject itemDataObject) : base(itemDataObject)
        {
            var weaponDataObject = itemDataObject as WeaponDataObject;
            var weaponEntity = Object.Instantiate(weaponDataObject.WeaponEntity);
            m_UsableItemEntity = weaponEntity;
        }
    }
}