using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAnimatorBlendTreeModule : AbstractBehaviourModule
    {
        [SerializeField] private Animator m_Animator;
        [SerializeField] private float m_TransitionSpeed = 1f;

        private float m_DirectionX;
        private float m_DirectionZ;
        private float m_Speed;

        private float m_TargetDirectionX;
        private float m_TargetDirectionZ;
        private float m_TargetSpeed;

        public float DirectionX
        {
            set => m_TargetDirectionX = value;
        }

        public float DirectionZ
        {
            set => m_TargetDirectionZ = value;
        }

        public float Speed
        {
            set => m_TargetSpeed = value;
        }

        public override void OnLateUpdate()
        {
            m_DirectionX = Mathf.MoveTowards(m_DirectionX, m_TargetDirectionX, m_TransitionSpeed);
            m_DirectionZ = Mathf.MoveTowards(m_DirectionZ, m_TargetDirectionZ, m_TransitionSpeed);
            m_Speed = Mathf.MoveTowards(m_Speed, m_TargetSpeed, m_TransitionSpeed);
            m_Animator.SetFloat("DirectionX", m_DirectionX);
            m_Animator.SetFloat("DirectionZ", m_DirectionZ);
            m_Animator.SetFloat("Speed", m_Speed);
        }
    }
}