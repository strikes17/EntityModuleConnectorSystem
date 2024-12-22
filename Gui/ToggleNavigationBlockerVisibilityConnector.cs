using UnityEngine;

namespace _Project.Scripts
{
    public class ToggleNavigationBlockerVisibilityConnector : BehaviourModuleConnector
    {
        [SelfInject] private GuiToggleModule m_GuiToggleModule;
        [Inject] private NavigationBlockerEntityContainerModule m_BlockerEntityContainerModule;

        protected override void Initialize()
        {
            m_GuiToggleModule.ValueChanged.AddListener(ToggleChanged);
        }

        private void ToggleChanged(bool isOn)
        {
            var blockers = m_BlockerEntityContainerModule.ContainerCollection;
            foreach (var abstractEntity in blockers)
            {
                var stateSwitcher = abstractEntity.GetBehaviorModuleByType<StateSwitcherCompoundModule>();
                if (stateSwitcher != null)
                {
                    stateSwitcher.State = isOn ? 1 : 0;
                }
            }
        }
    }
}