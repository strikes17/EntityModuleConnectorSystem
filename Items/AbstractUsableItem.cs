using System;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractUsableItem
    {
        public AbstractUsableItem(AbstractPickableItemDataObject itemDataObject, UsableItemEntity usableItemEntity,
            GuiInventoryItemEntity inventoryItemEntity, EntityGameUpdateHandlerRegisterModule handlerRegisterModule)
        {
            m_UsableItemEntity = usableItemEntity;
            m_ItemDataObject = itemDataObject;
            m_InventoryItemEntity = inventoryItemEntity;
            m_HandlerRegisterModule = handlerRegisterModule;
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
                    m_InventoryItemEntity = UnityEngine.Object.Instantiate(m_ItemDataObject.InventoryItemEntity);
                    m_InventoryItemEntity.GetBehaviorModuleByType<GuiInventoryItemModule>().Item = this;
                    m_HandlerRegisterModule.Register(m_InventoryItemEntity);
                }

                return m_InventoryItemEntity;
            }
        }

        protected UsableItemEntity m_UsableItemEntity;

        protected GuiInventoryItemEntity m_InventoryItemEntity;

        protected AbstractPickableItemDataObject m_ItemDataObject;

        protected int m_Amount;

        protected EntityGameUpdateHandlerRegisterModule m_HandlerRegisterModule;
    }
}