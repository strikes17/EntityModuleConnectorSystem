using System;

namespace _Project.Scripts
{
    [Serializable]
    public class TraderAssortmentConnector : BehaviourModuleConnector
    {
        [SelfInject] private TraderAssortmentDispatcherModule m_AssortmentDispatcherModule;
        [SelfInject] private InventoryModule m_InventoryModule;
        
        [Inject] private AmmoContainer m_AmmoContainer;
        [Inject] private WeaponsContainer m_WeaponsContainer;
        [Inject] private GuiInventoryItemsContainerModule m_GuiInventoryItemsContainer;
        
        protected override void Initialize()
        {
            m_AssortmentDispatcherModule.AssortmentRequested += AssortmentDispatcherModuleOnAssortmentRequested;
        }

        private void AssortmentDispatcherModuleOnAssortmentRequested(TraderAssortmentDataObject assortmentDataObject)
        {
            var dataObjects = assortmentDataObject.Assortment;
            foreach (AbstractPickableItemDataObject itemDataObject in dataObjects)
            {
                AbstractUsableItem item = null;
                if (itemDataObject is WeaponDataObject)
                {
                    item = new WeaponItem(itemDataObject, m_GuiInventoryItemsContainer, m_WeaponsContainer);  
                }
                else if (itemDataObject is AmmoDataObject)
                {
                    item = new AmmoItem(itemDataObject, m_GuiInventoryItemsContainer, m_AmmoContainer);
                }

                if (item != null)
                {
                    m_InventoryModule.AddItem(item);
                }
            }
        }
    }
}