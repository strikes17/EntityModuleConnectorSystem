using UnityEngine;

namespace _Project.Scripts
{
    public class ToggleStateSwitcherConnector : BehaviourModuleConnector
    {
        [SerializeField] private GuiDefaultEntity m_ToggleEntity;
        [SerializeField] private AbstractEntity m_SwitcherEntity;

        [SelfInject] private GuiToggleModule m_GuiToggleModule;
        [SelfInject] private StateSwitcherCompoundModule m_StateSwitcherModule;

        protected override void Initialize()
        {
            m_GuiToggleModule.ValueChanged.AddListener(ToggleChanged);
        }

        private void ToggleChanged(bool isOn)
        {
            m_StateSwitcherModule.State = isOn ? 1 : 0;
        }
    }
}