using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "SO/Trader Assortment Compound Data")]
    public class TraderAssortmentCompoundDataObject : ScriptableObject
    {
        [SerializeField] private TraderAssortmentDictionary m_DataObjects;

        public TraderAssortmentDictionary DataObjects => m_DataObjects;
    }
}