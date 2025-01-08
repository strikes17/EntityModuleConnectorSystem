namespace _Project.Scripts
{
    public class AmmoItem : AbstractUsableItem
    {
        //Тут будет AmmoEntity
        public AmmoItem(AbstractPickableItemDataObject itemDataObject, WeaponEntity weaponEntity,
            GuiInventoryItemEntity inventoryItemEntity) : base(itemDataObject, weaponEntity, inventoryItemEntity)
        {
        }

        public void AddAmount(int amount)
        {
            m_Amount += amount;
        }
    }
}