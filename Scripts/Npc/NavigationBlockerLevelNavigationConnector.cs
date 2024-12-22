using System.Collections.Generic;

namespace _Project.Scripts
{
    public class NavigationBlockerLevelNavigationConnector : BehaviourModuleConnector
    {
        [Inject] private LevelNavigationModule m_LevelNavigationModule;
        [SelfInject] private NavigationBlockerModule m_NavigationBlockerModule;

        protected override void Initialize()
        {
            m_NavigationBlockerModule.Placed += NavigationBlockerModuleOnPlaced;
        }

        private void NavigationBlockerModuleOnPlaced(List<(int, int)> blockedIndexes)
        {
            foreach (var (x, z) in blockedIndexes)
            {
                m_LevelNavigationModule.BlockCell(x, z);
            }
        }
    }
}