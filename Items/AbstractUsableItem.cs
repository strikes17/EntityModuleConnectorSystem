using System;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractUsableItem
    {
        protected AbstractUsableItem(AbstractPickableItemDataObject itemDataObject,
            GuiInventoryItemsContainerModule guiInventoryItemsContainer, EntityContainerModule entityContainerModule)
        {
            m_ItemDataObject = itemDataObject;
            m_GuiInventoryItemsContainer = guiInventoryItemsContainer;
            m_EntityContainerModule = entityContainerModule;
        }

        public int Amount => m_Amount;

        public UsableItemEntity UsableItemEntity => m_UsableItemEntity;

        public AbstractPickableItemDataObject DataObject => m_ItemDataObject;

        public GuiInventoryItemEntity InventoryItemEntity
        {
            get
            {
                if (m_InventoryItemEntity == null)
                {
                    m_InventoryItemEntity =
                        m_GuiInventoryItemsContainer.SpawnInventoryItemEntity(m_ItemDataObject.InventoryItemEntityPrefab);
                }

                m_InventoryItemEntity.GetBehaviorModuleByType<GuiInventoryItemModule>().Item = this;

                return m_InventoryItemEntity;
            }
        }

        protected GuiInventoryItemEntity m_InventoryItemEntity;

        protected GuiInventoryItemsContainerModule m_GuiInventoryItemsContainer;

        protected EntityContainerModule m_EntityContainerModule;

        protected UsableItemEntity m_UsableItemEntity;

        protected AbstractPickableItemDataObject m_ItemDataObject;

        protected int m_Amount;
    }
}