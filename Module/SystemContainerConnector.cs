using UnityEngine;

namespace _Project.Scripts
{
    public class SystemContainerConnector : BehaviourModuleConnector
    {
        [SerializeField] private AbstractEntity m_AbstractEntity;

        private SystemEntityContainerModule m_SystemEntityContainerModule;

        public override bool IsResolved => true;

        protected override void Initialize()
        {
            Debug.Log($"Adding {m_AbstractEntity.name}");
            m_SystemEntityContainerModule =
                Utility.FindEntityWithModule<SystemEntityContainerModule>()
                    .GetBehaviorModuleByType<SystemEntityContainerModule>();
            m_SystemEntityContainerModule.AddElement(m_AbstractEntity);
        }
    }
}