using UnityEngine;

namespace _Project.Scripts
{
    public abstract class AbstractPickableItemDataObject : ScriptableObject
    {
        [SerializeField] protected GuiInventoryItemEntity m_InventoryItemPrefab;
        [SerializeField] protected string m_Id;

        public GuiInventoryItemEntity InventoryItemEntity => m_InventoryItemPrefab;

        public string Id => m_Id;
    }
}