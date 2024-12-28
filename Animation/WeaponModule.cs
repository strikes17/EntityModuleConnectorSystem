using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, Vector3, WeaponEntity> WeaponFired = delegate { };

        [SerializeField] private WeaponDataObject m_WeaponDataObject;
        [SerializeField] private Transform m_WeaponFirePoint;

        public void Fire(AbstractEntity weaponUser, AbstractEntity targetEntity)
        {
            var direction = (targetEntity.transform.position - m_WeaponFirePoint.position).normalized;
            Ray ray = new Ray(m_WeaponFirePoint.transform.position, direction);
            if (Physics.Raycast(ray, Mathf.Infinity))
            {

            }
            WeaponFired(weaponUser, direction, m_AbstractEntity as WeaponEntity);
        }
    }

    [Serializable]
    public class NpcUsableItemHolderModule : AbstractBehaviourModule
    {
        [SerializeField] private Transform m_PrimaryHandTransform;
        [SerializeField] private Transform m_SecondaryHandTransform;
        [SerializeField] private Transform m_WeaponHolderTransform;

        public void SetWeapon(WeaponItem weaponItem, WeaponItem oldWeaponItem)
        {
            oldWeaponItem.UsableItemEntity.gameObject.SetActive(false);

            var usableItemEntity = weaponItem.UsableItemEntity;
            usableItemEntity.gameObject.SetActive(true);
            usableItemEntity.transform.SetParent(m_WeaponHolderTransform);
        }
    }

    [Serializable]
    public class NpcAiWeaponConnector : BehaviourModuleConnector
    {
        [SelfInject] private InventoryModule m_InventoryModule;
        [SelfInject] private NpcUsableItemHolderModule m_UsableItemHolderModule;
        [SelfInject] private NpcAiLogicModule m_LogicModule;

        protected override void Initialize()
        {
            m_InventoryModule.WeaponInHandsChanged += InventoryModuleOnWeaponInHandsChanged;
            m_LogicModule.DecidedToEliminateEntity += LogicModuleOnDecidedToEliminateEntity;
        }

        private void LogicModuleOnDecidedToEliminateEntity(AbstractEntity targetEntity, NpcEntity npcEntity)
        {
            var activeWeapon = m_InventoryModule.WeaponInHands;
            var weaponEntity = activeWeapon.UsableItemEntity as WeaponEntity;
            if (weaponEntity != null)
            {
                var weaponModule = weaponEntity.GetBehaviorModuleByType<WeaponModule>();
                weaponModule.Fire(npcEntity, targetEntity);
            }
        }

        private void InventoryModuleOnWeaponInHandsChanged(WeaponItem weaponItem, WeaponItem oldWeaponItem)
        {
            m_UsableItemHolderModule.SetWeapon(weaponItem, oldWeaponItem);
        }
    }
}