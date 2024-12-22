namespace _Project.Scripts
{
    public class EntityGameUpdateHandlerRegisterConnector : BehaviourModuleConnector
    {
        [SelfInject] private EntityContainerModule m_EntityContainerModule;
        [SelfInject] private EntityGameUpdateHandlerRegisterModule m_UpdateHandlerRegisterModule;
        
        protected override void Initialize()
        {
            m_EntityContainerModule.ElementAdded += EntityContainerModuleOnElementAdded;
            m_EntityContainerModule.ElementRemoved += EntityContainerModuleOnElementRemoved;
        }

        private void EntityContainerModuleOnElementRemoved(AbstractEntity abstractEntity)
        {
            m_UpdateHandlerRegisterModule.Unregister(abstractEntity);
        }

        private void EntityContainerModuleOnElementAdded(AbstractEntity abstractEntity)
        {
            m_UpdateHandlerRegisterModule.Register(abstractEntity);
        }
    }
}