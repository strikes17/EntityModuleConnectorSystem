using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TraderAssortmentDispatcherModule : AbstractBehaviourModule
    {
        public event Action<TraderAssortmentDataObject> AssortmentRequested = delegate { };
        
        [SerializeField] private TraderAssortmentCompoundDataObject m_TraderAssortmentDataObject;

        public void RequestInventoryByTier(int tier)
        {
            if (m_TraderAssortmentDataObject.DataObjects.TryGetValue(tier,
                    out TraderAssortmentDataObject assortmentDataObject))
            {
                AssortmentRequested(assortmentDataObject);
            }
        }
    }
}