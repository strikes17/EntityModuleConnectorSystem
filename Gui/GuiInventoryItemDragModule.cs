using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiInventoryItemDragModule : GuiAbstractBehaviourModule
    {
        public void Setup(List<GuiInventoryGridFillModule> gridFillModules)
        {
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            var position = eventData.position;
            m_GuiDefaultEntity.transform.position = position;
        }
    }

    [Serializable]
    public class GuiInventoryItemDragConnector : BehaviourModuleConnector
    {
        [Inject(typeof(GuiPdaEntity))] private GuiAbstractVisibilityModule m_GuiPdaVisibilityModule;
        [Inject(typeof(GuiStashScreenEntity))] private GuiAbstractVisibilityModule m_GuiStashScreenVisibilityModule;

        [Inject(typeof(GuiTraderScreenEntity))]
        private GuiAbstractVisibilityModule m_GuiTraderScreenVisibilityModule;

        [Inject(typeof(GuiInventoryMainGridFillEntity))]
        private GuiInventoryGridFillModule m_MainInventoryGridFillModule;

        [Inject(typeof(GuiInventoryStashGridFillEntity))]
        private GuiInventoryGridFillModule m_GuiStashGridFillModule;

        [Inject(typeof(GuiTraderAssortmentGridFillEntity))]
        private GuiInventoryGridFillModule m_GuiTraderAssortmentGridFillModule;

        [Inject(typeof(GuiTraderBuyGridFillEntity))]
        private GuiInventoryGridFillModule m_GuiTraderBuyGridFillModule;

        [Inject(typeof(GuiTraderSellGridFillEntity))]
        private GuiInventoryGridFillModule m_GuiTraderSellGridFillModule;

        [Inject(typeof(GuiTraderPlayerInventoryGridFillEntity))]
        private GuiInventoryGridFillModule m_GuiTraderPlayerInventoryGridFillModule;

        [SelfInject] private GuiInventoryItemDragModule m_InventoryItemDragModule;
        [SelfInject] private InventoryItemGridAssignModule m_GridAssignModule;

        private List<GuiInventoryGridFillModule> m_GridFillModules;

        protected override void Initialize()
        {
            m_GridFillModules = new();

            m_GuiPdaVisibilityModule.Shown += GuiPdaVisibilityModuleOnShown;
            m_GuiStashScreenVisibilityModule.Shown += GuiStashScreenVisibilityModuleOnShown;
            m_GuiTraderScreenVisibilityModule.Shown += GuiTraderScreenVisibilityModuleOnShown;
        }

        private void GuiTraderScreenVisibilityModuleOnShown()
        {
            m_GridFillModules.Clear();

            m_GridFillModules.Add(m_GuiTraderAssortmentGridFillModule);
            m_GridFillModules.Add(m_GuiTraderBuyGridFillModule);
            m_GridFillModules.Add(m_GuiTraderSellGridFillModule);
            m_GridFillModules.Add(m_GuiTraderPlayerInventoryGridFillModule);

            m_InventoryItemDragModule.Setup(m_GridFillModules);
        }

        private void GuiStashScreenVisibilityModuleOnShown()
        {
            m_GridFillModules.Clear();

            m_GridFillModules.Add(m_MainInventoryGridFillModule);
            m_GridFillModules.Add(m_GuiStashGridFillModule);
            m_InventoryItemDragModule.Setup(m_GridFillModules);
        }

        private void GuiPdaVisibilityModuleOnShown()
        {
            m_GridFillModules.Clear();

            m_GridFillModules.Add(m_MainInventoryGridFillModule);
            m_InventoryItemDragModule.Setup(m_GridFillModules);
        }
    }
}