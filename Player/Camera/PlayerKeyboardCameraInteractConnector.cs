using System;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerKeyboardCameraInteractConnector : BehaviourModuleConnector
    {
        [SelfInject] private PlayerInputModule m_PlayerInputModule;
        [Inject] private CameraInteractModule m_CameraInteractModule;

        protected override void Initialize()
        {
            m_PlayerInputModule.PlayerEntityInteractionKeyPressed +=
                PlayerInputModuleOnPlayerMouseEntityInteractionKeyPressed;
        }

        private void PlayerInputModuleOnPlayerMouseEntityInteractionKeyPressed()
        {
            m_CameraInteractModule.CameraForwardRaycast(false);
        }
    }
}