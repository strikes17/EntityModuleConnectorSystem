using UnityEngine;

namespace _Project.Scripts
{
    public class SelectTowerContextMenuConnector : BehaviourModuleConnector
    {
        [Inject] private TowerContextMenuModule m_TowerContextMenuModule;

        [SelfInject] private AbstractEntitySelectableModule m_AbstractEntitySelectableModule;

        protected override void Initialize()
        {
            m_AbstractEntitySelectableModule.Selected += AbstractEntitySelectableModuleOnSelected;
        }

        private void AbstractEntitySelectableModuleOnSelected(AbstractEntity abstractEntity)
        {
            m_TowerContextMenuModule.OpenContextMenu(abstractEntity.transform.position);
        }
    }
}