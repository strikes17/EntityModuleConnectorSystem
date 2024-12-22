using UnityEngine;

namespace _Project.Scripts
{
    public class AnimationModule : AbstractBehaviourModule
    {
        [SerializeField] private Animator m_Animator;

        public void PlayAnimation(string animationName)
        {
            m_Animator.Play(animationName);
        }
    }
}