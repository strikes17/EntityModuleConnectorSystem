using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialCompleteStepCondition : AbstractTutorialStepCondition
    {
        [SerializeField] private string m_StepKey;
        [Inject] private TutorialStepsModule m_TutorialStepsModule;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_TutorialStepsModule.StepCompleted += TutorialStepsModuleOnStepCompleted;
        }

        private void TutorialStepsModuleOnStepCompleted(string stepKey)
        {
            if (stepKey == m_StepKey)
            {
                OnCompleted();
            }
        }
    }
}