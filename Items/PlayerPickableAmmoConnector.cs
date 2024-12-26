using System;
using _Project.Scripts.Camera;

namespace _Project.Scripts
{
    [Serializable]
    public class PlayerPickableAmmoConnector : BehaviourModuleConnector
    {
        [SelfInject] private EntityInteractModule m_EntityInteractModule;

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
                AmmoItem ammoItem = new AmmoItem();
                inventoryModule.AddItem(ammoItem);
            }
        }
    }
}