using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class InventoryModule : AbstractBehaviourModule
    {
        public event Action<WeaponItem> WeaponAdded = delegate(WeaponItem item) { };

        public event Action<WeaponItem, WeaponItem> WeaponInHandsChanged =
            delegate(WeaponItem nextWeapon, WeaponItem oldWeapon) { };

        public event Action<WeaponItem> PrimaryWeaponChanged = delegate(WeaponItem item) { };
        public event Action<WeaponItem> SecondaryWeaponChanged = delegate(WeaponItem item) { };
        public event Action<WeaponItem> PistolWeaponChanged = delegate(WeaponItem item) { };

        public WeaponItem WeaponInHands => m_WeaponInHands;

        private List<AbstractUsableItem> m_UsableItems;

        private WeaponItem m_PrimaryWeapon;
        private WeaponItem m_SecondaryWeapon;
        private WeaponItem m_PistolWeapon;

        private WeaponItem m_WeaponInHands;

        public void AddItem(AbstractUsableItem abstractUsableItem)
        {
            m_UsableItems.Add(abstractUsableItem);
            if (abstractUsableItem is WeaponItem)
            {
                var weaponItem = (WeaponItem)abstractUsableItem;
                var weaponDataObject = weaponItem.DataObject as WeaponDataObject;
                if (m_PrimaryWeapon == null)
                {
                    if (m_PrimaryWeapon != weaponItem)
                    {
                        m_PrimaryWeapon = weaponItem;
                        PrimaryWeaponChanged(m_PrimaryWeapon);
                    }
                }
                else if (m_SecondaryWeapon == null)
                {
                    if (m_SecondaryWeapon != weaponItem)
                    {
                        m_SecondaryWeapon = weaponItem;
                        SecondaryWeaponChanged(m_SecondaryWeapon);
                    }
                }
                else if (m_PistolWeapon == null)
                {
                    if (weaponDataObject != null && weaponDataObject.WeaponType == WeaponType.Pistol)
                    {
                        if (m_PistolWeapon != weaponItem)
                        {
                            m_PistolWeapon = weaponItem;
                            PistolWeaponChanged(m_PistolWeapon);
                        }
                    }
                }

                WeaponAdded(weaponItem);

                if (m_WeaponInHands == null)
                {
                    if (m_PrimaryWeapon != null)
                    {
                        m_WeaponInHands = m_PrimaryWeapon;
                    }
                    else if (m_SecondaryWeapon != null)
                    {
                        m_WeaponInHands = m_SecondaryWeapon;
                    }
                    else if (m_PistolWeapon != null)
                    {
                        m_PistolWeapon = m_PrimaryWeapon;
                    }

                    WeaponInHandsChanged(m_WeaponInHands, null);
                }
            }

            Debug.Log($"Added {abstractUsableItem.GetType()}");
        }

        public T TryGetItem<T>(string id) where T : AbstractUsableItem
        {
            var item = m_UsableItems.FirstOrDefault(x => x.GetType() == typeof(T) && x.DataObject.Id == id);
            if (item != null)
            {
                return item as T;
            }

            return default;
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_UsableItems = new();
        }
    }
}