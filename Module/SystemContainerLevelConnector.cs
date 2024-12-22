using UnityEngine;

namespace _Project.Scripts
{
    public class SystemContainerLevelConnector : BehaviourModuleConnector
    {
        [SerializeField] private AbstractEntity m_LevelLoaderEntity;
        [SerializeField] private AbstractEntity m_SystemContainerEntity;

        private SystemEntityContainerModule m_SystemEntityContainerModule;

        private LevelLoaderModule m_LevelLoaderModule;

        protected override void Initialize()
        {
            m_LevelLoaderModule = m_LevelLoaderEntity.GetBehaviorModuleByType<LevelLoaderModule>();
            m_SystemEntityContainerModule =
                m_SystemContainerEntity.GetBehaviorModuleByType<SystemEntityContainerModule>();
            m_LevelLoaderModule.LevelLoaded += LevelLoaderModuleOnLevelLoaded;
        }

        private void LevelLoaderModuleOnLevelLoaded(AbstractEntity levelEntity)
        {
            m_SystemEntityContainerModule.AddElement(levelEntity);
            Debug.Log($"Level entity {levelEntity.name} added!");
        }
    }
}