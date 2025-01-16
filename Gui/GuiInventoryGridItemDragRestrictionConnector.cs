using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Camera;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiInventoryGridItemDragRestrictionConnector : BehaviourModuleConnector
    {
        [Inject(typeof(PlayerEntity))] private InventoryGridValidationModule m_PlayerInventoryValidationModule;
        [Inject(typeof(PlayerEntity))] private PlayerNpcInteractModule m_PlayerNpcInteractModule;

        [Inject(typeof(GuiPdaEntity))] private GuiAbstractVisibilityModule m_GuiPdaVisibilityModule;

        [Inject(typeof(GuiInventoryMainGridFillEntity))]
        private GuiInventoryGridFillModule m_MainInventoryGridFillModule;

        [Inject(typeof(GuiTraderAssortmentGridFillEntity))]
        private GuiInventoryGridFillModule m_GuiTraderAssortmentGridFillModule;

        [Inject(typeof(GuiTraderBuyGridFillEntity))]
        private GuiInventoryGridFillModule m_GuiTraderBuyGridFillModule;

        [Inject(typeof(GuiPlayerSellGridFillEntity))]
        private GuiInventoryGridFillModule m_GuiPlayerSellGridFillModule;

        [Inject(typeof(GuiTraderPlayerInventoryGridFillEntity))]
        private GuiInventoryGridFillModule m_GuiTraderPlayerInventoryGridFillModule;

        protected override void Initialize()
        {
            m_PlayerNpcInteractModule.TradeInteracted += PlayerNpcInteractModuleOnTradeInteracted;
            m_GuiPdaVisibilityModule.Shown += GuiPdaVisibilityModuleOnShown;
        }

        private void PlayerNpcInteractModuleOnTradeInteracted(NpcEntity npcTrader)
        {
            var npcGrids = new List<GuiInventoryGridFillModule>
            {
                m_GuiTraderAssortmentGridFillModule,
                m_GuiTraderBuyGridFillModule
            };

            var traderInventory = npcTrader.GetBehaviorModuleByType<InventoryGridValidationModule>();
            List<(Vector2Int, AbstractUsableItem)> traderItems = traderInventory.AllItems;
            foreach (var traderItem in traderItems)
            {
                var item = traderItem.Item2;
                var inventoryItemEntity = item.InventoryItemEntity;
                var itemModule = inventoryItemEntity.GetBehaviorModuleByType<GuiInventoryItemModule>();
                itemModule.SetAllowedGrids(npcGrids);
            }

            var playerGrids = new List<GuiInventoryGridFillModule>
            {
                m_GuiTraderPlayerInventoryGridFillModule,
                m_GuiPlayerSellGridFillModule
            };

            List<(Vector2Int, AbstractUsableItem)> playerItems = m_PlayerInventoryValidationModule.AllItems;
            foreach (var valueTuple in playerItems)
            {
                var item = valueTuple.Item2;
                var inventoryItemEntity = item.InventoryItemEntity;
                var itemModule = inventoryItemEntity.GetBehaviorModuleByType<GuiInventoryItemModule>();
                itemModule.SetAllowedGrids(playerGrids);
            }
        }

        private void GuiPdaVisibilityModuleOnShown()
        {
            var playerGrids = new List<GuiInventoryGridFillModule>
            {
                m_MainInventoryGridFillModule,
            };

            List<(Vector2Int, AbstractUsableItem)> playerItems = m_PlayerInventoryValidationModule.AllItems;
            foreach (var valueTuple in playerItems)
            {
                var item = valueTuple.Item2;
                var inventoryItemEntity = item.InventoryItemEntity;
                var itemModule = inventoryItemEntity.GetBehaviorModuleByType<GuiInventoryItemModule>();
                itemModule.SetAllowedGrids(playerGrids);
            }
        }
    }
}