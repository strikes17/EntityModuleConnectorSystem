using UnityEngine;

namespace _Project.Scripts.Camera
{
    public class CameraInteractMoveConnector : BehaviourModuleConnector
    {
        [SelfInject] private CameraInteractModule m_CameraInteractModule;
        [SelfInject] private CameraMoveModule m_CameraMoveModule;

        protected override void Initialize()
        {
            m_CameraMoveModule.MoveStateUpdated += CameraMoveModuleOnMoveStateUpdated;
        }

        private void CameraMoveModuleOnMoveStateUpdated(bool canPlace)
        {
            if (canPlace)
            {
                m_CameraInteractModule.TryUnblockInteraction(GetHashCode());
            }
            else
            {
                m_CameraInteractModule.TryBlockInteraction(GetHashCode());
            }
        }
    }
}