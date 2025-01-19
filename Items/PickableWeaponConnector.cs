using System;
using _Project.Scripts.Camera;
using UnityEngine;
using Object = System.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class PickableWeaponConnector : BehaviourModuleConnector
    {
        [Inject] private WeaponsContainer m_WeaponsContainer;
        [Inject] private GuiInventoryItemsContainerModule m_GuiInventoryItemsContainer;

        [SelfInject] private EntityInteractModule m_EntityInteractModule;

        [SerializeField] private WeaponDataObject m_WeaponDataObject;

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
                    bool canAddItem = inventoryModule.CanAddItem(m_WeaponDataObject);

                    if (canAddItem)
                    {
                        entity.gameObject.SetActive(false);

                        WeaponItem weaponItem = new WeaponItem(m_WeaponDataObject, m_GuiInventoryItemsContainer,
                            m_WeaponsContainer);

                        inventoryModule.AddItem(weaponItem);
                    }
                }
            }
        }
    }
}