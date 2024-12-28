namespace _Project.Scripts
{
    public class AmmoItem : AbstractUsableItem
    {
        public AmmoItem(AbstractPickableItemDataObject itemDataObject) : base(itemDataObject)
        {
        }

        public void AddAmount(int amount)
        {
            m_Amount += amount;
        }
    }
}