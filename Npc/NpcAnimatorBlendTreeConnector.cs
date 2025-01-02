using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAnimatorBlendTreeConnector : BehaviourModuleConnector
    {
        [SelfInject] private SkinMeshAnimationModule m_SkinMeshAnimationModule;
        [SelfInject] private NpcAnimatorBlendTreeModule m_AnimatorBlendTreeModule;
        [SelfInject] private AiNavMeshModule m_AiNavMeshModule;
        [SerializeField] private float m_Coeff = 1.5f;

        protected override void Initialize()
        {
            m_SkinMeshAnimationModule.ForwardVectorUpdated += SkinMeshAnimationModuleOnForwardVectorUpdated;
        }

        private void SkinMeshAnimationModuleOnForwardVectorUpdated(Vector3 boneForward)
        {
            var dir1 = m_AiNavMeshModule.Velocity.normalized;
            var dir2 = boneForward.normalized;
            var poss = m_AbstractEntity.transform.position;
            poss.y += 1.5f;
            Debug.DrawRay(poss, dir1 * 2, Color.green);
            Debug.DrawRay(poss, dir2 * 2, Color.blue);
            dir1.y = 0f;
            dir2.y = 0f;
            float angle = (Vector3.SignedAngle(dir1, dir2, Vector3.up) + 90f) * Mathf.Deg2Rad;
            float dirX = Mathf.Cos(angle);
            float dirZ = Mathf.Sin(angle);
            dirX = Mathf.RoundToInt(dirX);
            dirZ = Mathf.RoundToInt(dirZ);
            // Debug.Log($"Cos: {dirX} Sin: {dirZ}");
            m_AnimatorBlendTreeModule.DirectionX = dirX * m_Coeff;
            m_AnimatorBlendTreeModule.DirectionZ = dirZ * m_Coeff;
        }
    }
}