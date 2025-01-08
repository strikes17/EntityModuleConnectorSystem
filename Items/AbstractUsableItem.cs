using System;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractUsableItem
    {
        public AbstractUsableItem(AbstractPickableItemDataObject itemDataObject, UsableItemEntity usableItemEntity,
            GuiInventoryItemEntity inventoryItemEntity)
        {
            m_UsableItemEntity = usableItemEntity;
            m_ItemDataObject = itemDataObject;
            m_InventoryItemEntity = inventoryItemEntity;
        }

        public int Amount => m_Amount;

        public UsableItemEntity UsableItemEntity => m_UsableItemEntity;

        public AbstractPickableItemDataObject DataObject => m_ItemDataObject;

        public GuiInventoryItemEntity InventoryItemEntity => m_InventoryItemEntity;

        protected UsableItemEntity m_UsableItemEntity;

        protected GuiInventoryItemEntity m_InventoryItemEntity;

        protected AbstractPickableItemDataObject m_ItemDataObject;

        protected int m_Amount;
    }
}