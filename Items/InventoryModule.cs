using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class InventoryModule : AbstractBehaviourModule
    {
        private List<AbstractUsableItem> m_UsableItems;

        public void AddItem(AbstractUsableItem abstractUsableItem)
        {
            m_UsableItems.Add(abstractUsableItem);
            Debug.Log($"Added {abstractUsableItem.GetType()}");
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_UsableItems = new();
        }
    }
}