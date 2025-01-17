using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class InventoryGridValidationConnector : BehaviourModuleConnector
    {
        [Inject(typeof(PlayerEntity))] private InventoryModule m_InventoryModule;
        [SelfInject] private InventoryGridValidationModule m_InventoryGridValidationModule;

        private Vector2Int m_LatestItemGridPosition;

        protected override void Initialize()
        {
            m_InventoryModule.ItemAdded += InventoryModuleOnAddItemValidationCompleted;
            m_InventoryModule.WeaponAdded += InventoryModuleOnAddItemValidationCompleted;

            m_InventoryModule.AddItemValidationStarted += InventoryModuleOnAddItemValidationStarted;
        }

        private void InventoryModuleOnAddItemValidationCompleted(AbstractUsableItem abstractUsableItem)
        {
            m_InventoryGridValidationModule.PlaceItemInGrid(abstractUsableItem, m_LatestItemGridPosition);
        }

        private void InventoryModuleOnAddItemValidationStarted(AbstractPickableItemDataObject dataObject)
        {
            m_LatestItemGridPosition = m_InventoryGridValidationModule.IsItemCanFitTheGrid(dataObject);
            Debug.Log($"isFull: {m_LatestItemGridPosition.x == -1}");
            m_InventoryModule.IsFull = m_LatestItemGridPosition.x == -1;
        }
    }
}