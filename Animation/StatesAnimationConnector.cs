using UnityEngine;

namespace _Project.Scripts
{
    public class StatesAnimationConnector : BehaviourModuleConnector
    {
        [SerializeField] private AbstractEntity m_AbstractEntity;
        [SerializeField] private AnimationDataObject m_AnimationDataObject;

        private AnimationModule m_AnimationModule;
        private StatesModule m_StatesModule;
        
        protected override void Initialize()
        {
            m_StatesModule = m_AbstractEntity.GetBehaviorModuleByType<StatesModule>();
            m_AnimationModule = m_AbstractEntity.GetBehaviorModuleByType<AnimationModule>();
            m_StatesModule.StateChanged += StatesModuleOnStateChanged;
        }

        private void StatesModuleOnStateChanged(AbstractState state)
        {
            if (state.GetType() == typeof(DeathState))
            {
                m_AnimationModule.PlayAnimation(m_AnimationDataObject.GetAnimationKey<AnimationDeathKey>());
            }
            else if (state.GetType() == typeof(RunState))
            {
                m_AnimationModule.PlayAnimation(m_AnimationDataObject.GetAnimationKey<AnimationRunKey>());
            }
        }
    }
}