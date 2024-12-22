namespace _Project.Scripts
{
    public class PlayerBrushEntityContainerConnector : BehaviourModuleConnector
    {
        [SelfInject] private PlayerBrushModule m_PlayerBrushModule;
        [Inject] private EntityContainerModule m_EntityContainerModule;

        protected override void Initialize()
        {
            m_PlayerBrushModule.BrushUsed += PlayerBrushModuleOnBrushUsed;
        }

        private void PlayerBrushModuleOnBrushUsed(AbstractEntity abstractEntity)
        {
            m_EntityContainerModule.AddElement(abstractEntity);
        }
    }
}