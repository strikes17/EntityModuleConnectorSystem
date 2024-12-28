using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class PickableAmmoConnector : BehaviourModuleConnector
    {
        [SelfInject] private EntityInteractModule m_EntityInteractModule;

        [SerializeField] private AmmoDataObject m_AmmoDataObject;
        [SerializeField] private int m_Amount;

        protected override void Initialize()
        {
            m_EntityInteractModule.InteractStarted += EntityInteractModuleOnInteractStarted;
        }

        private void EntityInteractModuleOnInteractStarted(AbstractEntity entity, AbstractEntity user)
        {
            var inventoryModule = user.GetBehaviorModuleByType<InventoryModule>();
            if (inventoryModule != null)
            {
                entity.gameObject.SetActive(false);
                var item = inventoryModule.TryGetItem<AmmoItem>(m_AmmoDataObject.Id);
                if (item != null)
                {
                    item.AddAmount(m_Amount);
                }
                else
                {
                    AmmoItem ammoItem = new AmmoItem(m_AmmoDataObject);
                    inventoryModule.AddItem(ammoItem);
                }
            }
        }
    }
}