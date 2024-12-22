namespace _Project.Scripts
{
    public class HostileNpcSpawnerContainerConnector : BehaviourModuleConnector
    {
        [Inject] private EntityContainerModule m_EntityContainerModule;
        [SelfInject] private HostileNpcSpawnerModule m_HostileNpcSpawnerModule;

        protected override void Initialize()
        {
            m_HostileNpcSpawnerModule.SpawnerEmitted += HostileNpcSpawnerModuleOnSpawnerEmitted;
        }

        private void HostileNpcSpawnerModuleOnSpawnerEmitted(EntityNpc entityNpc)
        {
            m_EntityContainerModule.AddElement(entityNpc);
        }
    }
}