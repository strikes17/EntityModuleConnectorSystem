using System;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialStepConnector : BehaviourModuleConnector
    {
        [Inject] private TutorialStepsModule m_TutorialStepsModule;

        private TutorialStepEntity m_TutorialStepEntity;

        protected override void Initialize()
        {
            m_TutorialStepEntity = m_AbstractEntity as TutorialStepEntity;
            m_TutorialStepsModule.AddStep(m_TutorialStepEntity.TutorialStepKey);
            m_TutorialStepEntity.StepStarted += TutorialStepEntityOnStepStarted;
            m_TutorialStepEntity.StepCompleted += TutorialStepEntityOnStepCompleted;
        }

        private void TutorialStepEntityOnStepCompleted(string stepKey)
        {
            m_TutorialStepsModule.SetStepCompleted(stepKey);
        }

        private void TutorialStepEntityOnStepStarted(string stepKey)
        {
            m_TutorialStepsModule.SetStepStarted(stepKey);
        }
    }
}