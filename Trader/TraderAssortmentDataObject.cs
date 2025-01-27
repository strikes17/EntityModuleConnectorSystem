using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "SO/Trader Assortment Single Data")]
    public class TraderAssortmentDataObject : ScriptableObject
    {
        [SerializeField] private List<AbstractPickableItemDataObject> m_ItemsList;

        public IEnumerable<AbstractPickableItemDataObject> Assortment => m_ItemsList;

        public bool HasItem(AbstractPickableItemDataObject itemDataObject)
        {
            return m_ItemsList.Contains(itemDataObject);
        }
    }
}