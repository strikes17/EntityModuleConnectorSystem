using _Project.Scripts.Camera;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class GridRaycastColliderCameraInteractConnector : BehaviourModuleConnector
    {
        [SelfInject, SerializeReference, ReadOnly] private GridRaycastColliderModule m_RaycastColliderModule;
        [Inject, SerializeReference, ReadOnly] private CameraInteractModule m_CameraInteractModule;
        
        protected override void Initialize()
        {
            m_CameraInteractModule.HitEntity += CameraInteractModuleOnHitEntity;
        }

        private void CameraInteractModuleOnHitEntity(AbstractEntity abstractEntity, RaycastHit hit)
        {
            m_RaycastColliderModule.OnEnd(hit);
        }
    }
}