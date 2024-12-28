using System;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractUsableItem
    {
        public AbstractUsableItem(AbstractPickableItemDataObject itemDataObject)
        {
            m_ItemDataObject = itemDataObject;
        }
        
        public int Amount => m_Amount;

        public UsableItemEntity UsableItemEntity => m_UsableItemEntity;

        public AbstractPickableItemDataObject DataObject => m_ItemDataObject;

        protected UsableItemEntity m_UsableItemEntity; 
            
        protected AbstractPickableItemDataObject m_ItemDataObject;

        protected int m_Amount;
    }
}