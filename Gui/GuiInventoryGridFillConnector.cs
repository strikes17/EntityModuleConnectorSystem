using System;
using System.Collections.Generic;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiInventoryGridFillConnector : BehaviourModuleConnector
    {
        [Inject(typeof(PlayerEntity))] private InventoryGridValidationModule m_InventoryGridValidationModule;
        [SelfInject] private GuiInventoryGridFillModule m_InventoryGridFillModule;

        protected override void Initialize()
        {
            m_InventoryGridValidationModule.GridCellUnlocked += InventoryGridValidationModuleOnGridCellUnlocked;
            m_InventoryGridValidationModule.ItemPlacedInGrid += InventoryGridValidationModuleOnItemPlacedInGrid;
        }

        private void InventoryGridValidationModuleOnGridCellUnlocked(Vector2Int position)
        {
            m_InventoryGridFillModule.AddCell(position);
        }

        private void InventoryGridValidationModuleOnItemPlacedInGrid(Vector2Int position,
            AbstractUsableItem abstractUsableItem)
        {
            m_InventoryGridFillModule.SetItem(position, abstractUsableItem);
            Debug.Log($"Set item t");
        }
    }
}