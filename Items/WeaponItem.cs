using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponItem : AbstractUsableItem
    {
        [SerializeField, ReadOnly] private string m_Name;

        public WeaponItem(AbstractPickableItemDataObject itemDataObject, WeaponEntity weaponEntity,
            GuiInventoryItemEntity inventoryItemEntity,
            EntityGameUpdateHandlerRegisterModule handlerRegisterModule) : base(itemDataObject, weaponEntity,
            inventoryItemEntity, handlerRegisterModule)
        {
            m_Name = weaponEntity.name;
        }
    }
}