using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class PickableWeaponConnector : BehaviourModuleConnector
    {
        [SelfInject] private EntityInteractModule m_EntityInteractModule;

        protected override void Initialize()
        {
            m_EntityInteractModule.InteractStarted += EntityInteractModuleOnInteractStarted;
        }

        private void EntityInteractModuleOnInteractStarted(AbstractEntity entity, AbstractEntity user)
        {
            if (entity == m_AbstractEntity)
            {
                var inventoryModule = user.GetBehaviorModuleByType<InventoryModule>();
                if (inventoryModule != null)
                {
                    entity.gameObject.SetActive(false);
                    WeaponItem weaponItem = new WeaponItem();
                    inventoryModule.AddItem(weaponItem);
                }   
            }
        }
    }
}