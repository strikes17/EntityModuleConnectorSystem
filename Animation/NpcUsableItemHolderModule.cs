using System;
using System.Collections;
using _Project.Scripts.Camera;
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
            if (weaponItem == null)
            {
                if (oldWeaponItem != null)
                {
                    oldWeaponItem.UsableItemEntity.gameObject.SetActive(false);
                    AddWeaponToHolder(oldWeaponItem);   
                }
                return;
            }
            
            if (oldWeaponItem != null)
            {
                oldWeaponItem.UsableItemEntity.gameObject.SetActive(false);
                AddWeaponToHolder(oldWeaponItem);
                Debug.Log(
                    $"weaponItem: {weaponItem.UsableItemEntity.name}, oldWeaponItem: {oldWeaponItem.UsableItemEntity.name}");
            }

            Debug.Log($"weaponItem: {weaponItem.UsableItemEntity.name}");

            var data = weaponItem.DataObject as WeaponDataObject;

            WeaponHandsPositionData positionData = null;

            if (m_AbstractEntity.GetType() == typeof(PlayerEntity))
            {
                positionData = data.GetPositionData<PlayerWeaponHandsPositionData>();
                Utility.SetLayerRecursively(weaponItem.UsableItemEntity.gameObject, LayerMask.NameToLayer("Hands"));
            }
            else if (m_AbstractEntity.GetType() == typeof(NpcEntity))
            {
                positionData = data.GetPositionData<NpcWeaponHandsPositionData>();
                Utility.SetLayerRecursively(weaponItem.UsableItemEntity.gameObject, LayerMask.NameToLayer("Entity"));
            }

            var usableItemEntity = weaponItem.UsableItemEntity;
            usableItemEntity.transform.SetParent(m_WeaponHandsTransform);
            usableItemEntity.transform.localPosition = positionData.HandsPosition;
            usableItemEntity.transform.localRotation = Quaternion.Euler(positionData.HandsRotation);
            usableItemEntity.transform.localScale = positionData.HandsScale;
            usableItemEntity.gameObject.SetActive(true);
        }
    }
}