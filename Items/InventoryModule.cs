using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class InventoryModule : AbstractBehaviourModule
    {
        public event Action<AbstractPickableItemDataObject> AddItemValidationStarted = delegate { };

        public event Action<AbstractPickableItemDataObject> AddItemValidationCompleted = delegate { };

        public event Action<AbstractPickableItemDataObject> AddItemValidationFailed = delegate { };

        public event Action<AbstractUsableItem> ItemAdded = delegate(AbstractUsableItem item) { };

        public event Action<WeaponItem> WeaponAdded = delegate(WeaponItem item) { };

        public event Action<WeaponItem, WeaponItem> WeaponInHandsChanged =
            delegate(WeaponItem nextWeapon, WeaponItem oldWeapon) { };

        public event Action<WeaponItem> PrimaryWeaponChanged = delegate(WeaponItem item) { };
        public event Action<WeaponItem> SecondaryWeaponChanged = delegate(WeaponItem item) { };
        public event Action<WeaponItem> PistolWeaponChanged = delegate(WeaponItem item) { };

        public WeaponItem WeaponInHands => m_WeaponInHands;

        public bool IsFull;

        private List<AbstractUsableItem> m_UsableItems;

        public IEnumerable<AbstractUsableItem> AllItems => m_UsableItems;

        [SerializeReference, ReadOnly] private WeaponItem m_PrimaryWeapon;
        [SerializeReference, ReadOnly] private WeaponItem m_SecondaryWeapon;
        [SerializeReference, ReadOnly] private WeaponItem m_PistolWeapon;

        [SerializeReference, ReadOnly] private WeaponItem m_WeaponInHands;

        public void SetPrimaryWeaponInHands() => SetTargetWeaponInHands(m_PrimaryWeapon);

        public void SetSecondaryWeaponInHands() => SetTargetWeaponInHands(m_SecondaryWeapon);

        public void SetPistolWeaponInHands() => SetTargetWeaponInHands(m_PistolWeapon);

        private void SetTargetWeaponInHands(WeaponItem weaponItem)
        {
            var oldWeapon = m_WeaponInHands;

            if (m_WeaponInHands == weaponItem)
            {
                m_WeaponInHands = null;
            }
            else
            {
                m_WeaponInHands = weaponItem;
            }

            if (m_WeaponInHands != null && oldWeapon != null)
            {
                Debug.Log(
                    $"m_WeaponInHands: {m_WeaponInHands.UsableItemEntity.name}, oldWeapon: {oldWeapon.UsableItemEntity.name}");
            }
            else if (oldWeapon != null)
            {
                Debug.Log($"m_WeaponInHands: EMPTY_HAND, oldWeapon: {oldWeapon.UsableItemEntity.name}");
            }

            WeaponInHandsChanged(m_WeaponInHands, oldWeapon);
        }

        public bool CanAddItem(AbstractPickableItemDataObject itemDataObject)
        {
            AddItemValidationStarted(itemDataObject);

            if (IsFull)
            {
                AddItemValidationFailed(itemDataObject);
                return false;
            }

            AddItemValidationCompleted(itemDataObject);

            return true;
        }

        public void AddItem(AbstractUsableItem abstractUsableItem)
        {
            m_UsableItems.Add(abstractUsableItem);
            if (abstractUsableItem is WeaponItem)
            {
                var weaponItem = (WeaponItem)abstractUsableItem;
                var weaponDataObject = weaponItem.DataObject as WeaponDataObject;
                if (weaponDataObject != null)
                {
                    if (m_PrimaryWeapon == null && weaponDataObject.WeaponType == WeaponType.Rifle)
                    {
                        if (m_PrimaryWeapon != weaponItem)
                        {
                            m_PrimaryWeapon = weaponItem;
                            PrimaryWeaponChanged(m_PrimaryWeapon);
                        }
                    }
                    else if (m_SecondaryWeapon == null && weaponDataObject.WeaponType == WeaponType.Rifle)
                    {
                        if (m_SecondaryWeapon != weaponItem)
                        {
                            m_SecondaryWeapon = weaponItem;
                            SecondaryWeaponChanged(m_SecondaryWeapon);
                        }
                    }
                    else if (m_PistolWeapon == null && weaponDataObject.WeaponType == WeaponType.Pistol)
                    {
                        if (m_PistolWeapon != weaponItem)
                        {
                            m_PistolWeapon = weaponItem;
                            PistolWeaponChanged(m_PistolWeapon);
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
                            m_WeaponInHands = m_PistolWeapon;
                        }

                        WeaponInHandsChanged(m_WeaponInHands, null);
                    }

                    // Debug.Log($"Added {abstractUsableItem.GetType()}");
                }
            }
            else
            {
                ItemAdded(abstractUsableItem);
            }
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