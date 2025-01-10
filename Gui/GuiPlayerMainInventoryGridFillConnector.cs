using System;
using System.Collections.Generic;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiPlayerMainInventoryGridFillConnector : BehaviourModuleConnector
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

    [Serializable]
    public class GuiTraderGridFillConnector : BehaviourModuleConnector
    {
        [Inject] private PlayerNpcInteractModule m_PlayerNpcInteractModule;

        [SelfInject] private GuiInventoryGridFillModule m_InventoryGridFillModule;

        protected override void Initialize()
        {
            m_PlayerNpcInteractModule.TradeInteracted += PlayerNpcInteractModuleOnTradeInteracted;
        }

        private void PlayerNpcInteractModuleOnTradeInteracted(NpcEntity npcEntity)
        {
            m_InventoryGridFillModule.Clear();

            var gridValidationModule = npcEntity.GetBehaviorModuleByType<InventoryGridValidationModule>();
            for (int i = 0; i < gridValidationModule.UnlockedCellsCount; i++)
            {
                m_InventoryGridFillModule.AddCell(gridValidationModule.GetCellPositionByIndex(i));
            }

            var items = gridValidationModule.AllItems;
            foreach (var valueTuple in items)
            {
                var position = valueTuple.Item1;
                var abstractUsableItem = valueTuple.Item2;
                var gridAssignModule =
                    abstractUsableItem.InventoryItemEntity.GetBehaviorModuleByType<InventoryItemGridAssignModule>();
                gridAssignModule.AssignNewGrid(GridFillInventoryType.TraderBuy);
                gridAssignModule.AssignNewGrid(GridFillInventoryType.TraderAssortment);
                m_InventoryGridFillModule.SetItem(position, abstractUsableItem);
            }
        }
    }
}