using UnityEngine;

namespace _Project.Scripts
{
    public class EntityContainerLevelNavigationConnector : BehaviourModuleConnector
    {
        [SelfInject] private LevelNavigationModule m_LevelNavigationModule;
        [Inject] private EntityContainerModule m_EntityContainerModule;
        
        protected override void Initialize()
        {
            m_EntityContainerModule.ElementAdded += EntityContainerModuleOnElementAdded;
            m_EntityContainerModule.ElementRemoved += EntityContainerModuleOnElementRemoved;
        }

        private void EntityContainerModuleOnElementRemoved(AbstractEntity abstractEntity)
        {
            int x = Mathf.RoundToInt(abstractEntity.transform.position.x);
            int z = Mathf.RoundToInt(abstractEntity.transform.position.z);
            m_LevelNavigationModule.UnblockCell(x, z);
        }

        private void EntityContainerModuleOnElementAdded(AbstractEntity abstractEntity)
        {
            int x = Mathf.RoundToInt(abstractEntity.transform.position.x);
            int z = Mathf.RoundToInt(abstractEntity.transform.position.z);
            m_LevelNavigationModule.BlockCell(x, z);
        }
    }
}