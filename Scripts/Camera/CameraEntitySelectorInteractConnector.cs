using UnityEngine;

namespace _Project.Scripts.Camera
{
    public class CameraEntitySelectorInteractConnector : BehaviourModuleConnector
    {
        [SelfInject] private CameraEntitySelectorModule m_EntitySelectorModule;
        [SelfInject] private CameraInteractModule m_CameraInteractModule;

        protected override void Initialize()
        {
            m_CameraInteractModule.EntityClickedUp += CameraInteractModuleOnEntityClickedUp;
        }

        private void CameraInteractModuleOnEntityClickedUp(AbstractEntity abstractEntity, RaycastHit hit)
        {
            m_EntitySelectorModule.TrySelectTargetEntity(abstractEntity, hit);
        }
    }
}