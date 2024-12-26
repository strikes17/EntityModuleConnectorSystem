using UnityEngine;

namespace _Project.Scripts
{
    public class StatesAnimationConnector : BehaviourModuleConnector
    {
        [SerializeField] private AnimationDataObject m_AnimationDataObject;

        [SelfInject] private AnimationModule m_AnimationModule;
        [SelfInject] private StatesModule m_StatesModule;
        
        protected override void Initialize()
        {
            m_StatesModule.StateChanged += StatesModuleOnStateChanged;
        }

        private void StatesModuleOnStateChanged(AbstractState state)
        {
            Debug.Log($"On state changed to {state.GetType()}");
            if (state.GetType() == typeof(DeathState))
            {
                m_AnimationModule.PlayAnimation(m_AnimationDataObject.GetAnimationKey<AnimationDeathKey>());
            }
            else if (state.GetType() == typeof(RunState))
            {
                m_AnimationModule.PlayAnimation(m_AnimationDataObject.GetAnimationKey<AnimationRunKey>());
            }
            else if (state.GetType() == typeof(WalkState))
            {
                m_AnimationModule.PlayAnimation(m_AnimationDataObject.GetAnimationKey<AnimationWalkKey>());
            }
            else if (state.GetType() == typeof(IdleState))
            {
                m_AnimationModule.PlayAnimation(m_AnimationDataObject.GetAnimationKey<AnimationIdleKey>());
            }
        }
    }
}