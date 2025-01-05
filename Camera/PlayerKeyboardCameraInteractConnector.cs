using System;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerKeyboardCameraInteractConnector : BehaviourModuleConnector
    {
        [SelfInject] private PlayerMouseKeyboardInputModule m_PlayerMouseKeyboardInputModule;
        [Inject] private CameraInteractModule m_CameraInteractModule;

        protected override void Initialize()
        {
            m_PlayerMouseKeyboardInputModule.PlayerEntityInteractionKeyPressed +=
                PlayerMouseKeyboardInputModuleOnPlayerMouseEntityInteractionKeyPressed;
        }

        private void PlayerMouseKeyboardInputModuleOnPlayerMouseEntityInteractionKeyPressed()
        {
            m_CameraInteractModule.CameraForwardRaycast(false);
        }
    }
}