using UnityEngine;

namespace _Project.Scripts
{
    public class SelectTowerFXConnector : BehaviourModuleConnector
    {
        [SelfInject] private AbstractEntitySelectableModule m_AbstractEntitySelectableModule;
        [SelfInject] private HighlightModule m_HighlightModule;

        protected override void Initialize()
        {
            m_AbstractEntitySelectableModule.Selected += AbstractEntitySelectableModuleOnSelected;
            m_AbstractEntitySelectableModule.Deselected += AbstractEntitySelectableModuleOnDeselected;
        }

        private void AbstractEntitySelectableModuleOnDeselected(AbstractEntity abstractEntity)
        {
            m_HighlightModule.SetHighlightDisabled(abstractEntity);
        }

        private void AbstractEntitySelectableModuleOnSelected(AbstractEntity abstractEntity)
        {
            m_HighlightModule.SetHighlightEnabled(abstractEntity);
        }
    }
}