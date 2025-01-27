namespace _Project.Scripts
{
    public class AmmoItem : AbstractUsableItem
    {
        //Тут будет AmmoEntity
        public AmmoItem(AbstractPickableItemDataObject itemDataObject,
            GuiInventoryItemsContainerModule guiInventoryItemsContainer,
            AmmoContainer entityContainerModule) : base(itemDataObject, guiInventoryItemsContainer,
            entityContainerModule)
        {
        }

        public void AddAmount(int amount)
        {
            m_Amount += amount;
        }
    }
}