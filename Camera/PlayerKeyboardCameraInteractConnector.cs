using System;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerKeyboardCameraInteractConnector : BehaviourModuleConnector
    {
        [SelfInject] private PlayerKeyboardInputModule m_PlayerKeyboardInputModule;
        [Inject] private CameraInteractModule m_CameraInteractModule;

        protected override void Initialize()
        {
            m_PlayerKeyboardInputModule.PlayerEntityInteractionKeyPressed +=
                PlayerKeyboardInputModuleOnPlayerEntityInteractionKeyPressed;
        }

        private void PlayerKeyboardInputModuleOnPlayerEntityInteractionKeyPressed()
        {
            m_CameraInteractModule.CameraForwardRaycast(false);
        }
    }
}