using System;
using _Project.Scripts.Camera;

namespace _Project.Scripts
{
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
            m_InventoryModule.WeaponAdded += InventoryModuleOnWeaponAdded;
        }

        private void InventoryModuleOnWeaponAdded(WeaponItem weaponItem)
        {
            m_UsableItemHolderModule.AddWeaponToHolder(weaponItem);
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
            m_UsableItemHolderModule.ChangeWeaponInHands(weaponItem, oldWeaponItem);
        }
    }
}