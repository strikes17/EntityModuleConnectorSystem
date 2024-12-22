namespace _Project.Scripts.Camera
{
    public class CameraEntitySelectorLockPlayerBrushConnector : BehaviourModuleConnector
    {
        [SelfInject] private CameraEntitySelectorModule m_EntitySelectorModule;
        [Inject] private PlayerBrushModule m_PlayerBrushModule;
        
        protected override void Initialize()
        {
            m_PlayerBrushModule.BrushUsed += PlayerBrushModuleOnBrushUsed;
        }

        private void PlayerBrushModuleOnBrushUsed(AbstractEntity abstractEntity)
        {
            m_EntitySelectorModule.LockForOneFrame();
        }
    }
}