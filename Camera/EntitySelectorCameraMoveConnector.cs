namespace _Project.Scripts.Camera
{
    public class EntitySelectorCameraMoveConnector : BehaviourModuleConnector
    {
        [SelfInject] private CameraEntitySelectorModule m_EntitySelectorModule;
        [SelfInject] private CameraMoveModule m_CameraMoveModule;

        protected override void Initialize()
        {
            m_CameraMoveModule.MoveStateUpdated += CameraMoveModuleOnMoveStateUpdated;
        }

        private void CameraMoveModuleOnMoveStateUpdated(bool cameraNotMoved)
        {
            if (!cameraNotMoved)
            {
                m_EntitySelectorModule.TryDeselectCurrentEntity();
            }
        }
    }
}