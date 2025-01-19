using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponItem : AbstractUsableItem
    {
        public WeaponItem(AbstractPickableItemDataObject itemDataObject, GuiInventoryItemsContainerModule guiInventoryItemsContainer,
            WeaponsContainer entityContainerModule) : base(itemDataObject, guiInventoryItemsContainer,
            entityContainerModule)
        {
            m_UsableItemEntity = entityContainerModule.SpawnWeapon(itemDataObject as WeaponDataObject);
        }
    }
}