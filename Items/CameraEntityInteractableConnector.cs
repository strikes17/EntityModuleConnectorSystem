using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class CameraEntityInteractableConnector : BehaviourModuleConnector
    {
        [Inject] protected CameraInteractModule m_CameraInteractModule;
        [SelfInject] protected EntityInteractModule m_EntityInteractModule;

        protected override void Initialize()
        {
            m_CameraInteractModule.HitEntity += CameraInteractModuleOnHitEntity;
        }

        private void CameraInteractModuleOnHitEntity(AbstractEntity abstractEntity, RaycastHit hit)
        {
            if (m_AbstractEntity == abstractEntity)
            {
                m_EntityInteractModule.StartInteract(m_CameraInteractModule.User);
            }
        }
    }
}