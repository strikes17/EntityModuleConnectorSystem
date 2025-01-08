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
        [Inject] private GuiContainerModule m_GuiContainerModule;
        [SelfInject] private GuiInventoryItemDragModule m_InventoryItemDragModule;
        [Inject(typeof(GuiPdaEntity))] private GuiAbstractVisibilityModule m_GuiPdaVisibilityModule;

        private List<GuiInventoryGridFillModule> m_GridFillModules;

        protected override void Initialize()
        {
            m_GridFillModules = new();

            m_GuiPdaVisibilityModule.Shown += GuiPdaVisibilityModuleOnShown;
            m_GuiContainerModule.ElementAdded += GuiContainerModuleOnElementAdded;

            List<AbstractEntity> guiEntities = m_GuiContainerModule.ContainerCollection.ToList();
            foreach (var abstractEntity in guiEntities)
            {
                GuiContainerModuleOnElementAdded(abstractEntity);
            }
        }

        private void GuiPdaVisibilityModuleOnShown()
        {
            
            m_InventoryItemDragModule.Setup(m_GridFillModules);
        }

        private void GuiContainerModuleOnElementAdded(AbstractEntity abstractEntity)
        {
            var guiEntity = abstractEntity as GuiDefaultEntity;
            if (guiEntity != null)
            {
                var gridFillModule = guiEntity.GetBehaviorModuleByType<GuiInventoryGridFillModule>();
                if (gridFillModule != null)
                {
                    m_GridFillModules.Add(gridFillModule);
                }
            }
        }
    }
}