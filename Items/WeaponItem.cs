using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponItem : AbstractUsableItem
    {
        [SerializeField, ReadOnly] private string m_Name;
        
        public WeaponItem(AbstractPickableItemDataObject itemDataObject, WeaponEntity weaponEntity) : base(itemDataObject)
        {
            m_UsableItemEntity = weaponEntity;
            m_Name = weaponEntity.name;
        }
    }
}