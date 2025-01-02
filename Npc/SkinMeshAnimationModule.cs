using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class SkinMeshAnimationModule : AbstractBehaviourModule
    {
        public event Action<Vector3> ForwardVectorUpdated = delegate { };

        [SerializeField] private float m_RotateSpeed = 1f;

        [SerializeField] private Transform m_SpineBoneTransform;
        [SerializeField] private Transform m_MainBodyTransform;

        [SerializeField] private Vector3 m_BodyTransformFixedRotation;
        [SerializeField] private Vector3 m_SpineBoneFixedRotation;

        [SerializeField] private AnimatorIKBehaviour m_AnimatorIKBehaviour;

        public Vector3 BodyBoneForwardDirection =>
            m_ForwardVector;

        private Vector3 m_ForwardVector;

        public Transform RotationTarget
        {
            get => m_RotationTarget;
            set => m_RotationTarget = value;
        }

        private Transform m_RotationTarget;

        public override void OnLateUpdate()
        {
            if (m_RotationTarget == null)
            {
                return;
            }

            RotateAroundY(m_MainBodyTransform, m_RotationTarget.position, m_BodyTransformFixedRotation);

            m_ForwardVector = Quaternion.AngleAxis(-m_BodyTransformFixedRotation.y, Vector3.up) *
                              -m_SpineBoneTransform.forward;

            ForwardVectorUpdated(m_ForwardVector);
        }

        private void LookAtTargetTransform(Transform bone, Vector3 position, Vector3 fixedRotation)
        {
            Vector3 direction = position - bone.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);

            Quaternion targetRotation = lookRotation * Quaternion.Euler(fixedRotation);

            Vector3 eulerAngles = targetRotation.eulerAngles;

            Quaternion limitedRotation = Quaternion.Euler(eulerAngles);

            bone.rotation = limitedRotation;
        }

        private void RotateAroundY(Transform bone, Vector3 position, Vector3 fixedRotation)
        {
            Vector3 direction = position - bone.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
            Vector3 eulerAngles = lookRotation.eulerAngles;
            eulerAngles.y += fixedRotation.y;
            eulerAngles.x = bone.eulerAngles.x + fixedRotation.x;
            eulerAngles.z = bone.eulerAngles.x + fixedRotation.z;

            bone.rotation = Quaternion.RotateTowards(bone.rotation, Quaternion.Euler(eulerAngles), m_RotateSpeed);

            direction.y = 0;
            var fwd = bone.forward;
            fwd.y = 0;
            var angle = Vector3.Angle(direction, fwd);
            if (angle - m_BodyTransformFixedRotation.y <= 15f)
            {
                LookAtTargetTransform(m_SpineBoneTransform, m_RotationTarget.position, m_SpineBoneFixedRotation);
            }
        }
    }
}