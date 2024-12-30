using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcUsableItemHolderModule : AbstractBehaviourModule
    {
        [SerializeField] private Transform m_PrimaryHandTransform;
        [SerializeField] private Transform m_SecondaryHandTransform;
        [SerializeField] private Transform m_WeaponHolderTransform;
        [SerializeField] private Transform m_WeaponVisualHolderTransform;
        [SerializeField] private Transform m_WeaponHandsTransform;

        public void AddWeaponToHolder(WeaponItem weaponItem)
        {
            var usableItemEntity = weaponItem.UsableItemEntity;
            usableItemEntity.transform.SetParent(m_WeaponHolderTransform);
            usableItemEntity.transform.localPosition = Vector3.zero;
            usableItemEntity.gameObject.SetActive(false);
        }

        public void ChangeWeaponInHands(WeaponItem weaponItem, WeaponItem oldWeaponItem)
        {
            if (oldWeaponItem != null)
            {
                oldWeaponItem.UsableItemEntity.gameObject.SetActive(false);
            }

            var usableItemEntity = weaponItem.UsableItemEntity;
            usableItemEntity.gameObject.SetActive(true);
            usableItemEntity.transform.SetParent(m_WeaponHandsTransform);
        }
    }
}