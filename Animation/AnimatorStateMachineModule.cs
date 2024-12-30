using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class AnimatorStateMachineModule : AbstractBehaviourModule
    {
        [SerializeField] private Animator m_Animator;

        public void SetAnimationState(string stateName)
        {
            m_Animator.SetTrigger(stateName);
        }
    }
}