using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [Serializable]
    public class HandsAnimatorModule : AbstractBehaviourModule
    {
        public event Action Updated = delegate { };

        [SerializeField] private Animator m_Animator;
        [SerializeField] private float m_TransitionSpeed = 1f;

        private float m_Speed;

        public float TargetSpeed
        {
            set => m_TargetSpeed = value;
        }

        private float m_TargetSpeed;

        public override void OnLateUpdate()
        {
            Updated();
            m_Speed = Mathf.MoveTowards(m_Speed, m_TargetSpeed, m_TransitionSpeed);
            m_Animator.SetFloat("Speed", m_Speed);
        }
    }
}