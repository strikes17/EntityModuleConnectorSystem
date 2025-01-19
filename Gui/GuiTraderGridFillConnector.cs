using System;
using _Project.Scripts.Camera;

namespace _Project.Scripts
{
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