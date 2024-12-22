using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialStepsModule : AbstractBehaviourModule
    {
        public event Action<string> StepCompleted = delegate { };

        [Serializable]
        public class TutorialStepState
        {
            [SerializeField] private bool m_IsCompleted;
            [SerializeField] private bool m_IsStarted;

            public bool IsCompleted
            {
                get => m_IsCompleted;
                set => m_IsCompleted = value;
            }

            public bool IsStarted
            {
                get => m_IsStarted;
                set => m_IsStarted = value;
            }
        }

        private Dictionary<string, TutorialStepState> m_TutorialStepStates;

        [SerializeField, ReadOnly] private List<string> m_CompletedStepsKeys = new();

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_TutorialStepStates = new();
        }

        public bool IsStepStarted(string stepKey)
        {
            if (m_TutorialStepStates.TryGetValue(stepKey, out TutorialStepState stepState))
            {
                return stepState.IsStarted;
            }

            return false;
        }
        
        public bool IsStepCompleted(string stepKey)
        {
            if (m_TutorialStepStates.TryGetValue(stepKey, out TutorialStepState stepState))
            {
                return stepState.IsCompleted;
            }

            return false;
        }

        public void AddStep(string stepKey)
        {
            m_TutorialStepStates.TryAdd(stepKey, new TutorialStepState());
        }

        private bool m_Lock;

        private IEnumerator TutorialLock()
        {
            m_Lock = true;
            yield return null;
            m_Lock = false;
        }

        public void SetStepCompleted(string stepKey)
        {
            // if (m_Lock)
            //     return;
            if (m_TutorialStepStates.TryGetValue(stepKey, out TutorialStepState stepState))
            {
                stepState.IsCompleted = true;
                StepCompleted(stepKey);
                m_CompletedStepsKeys.Add(stepKey);
                // Moroutine.Run(TutorialLock());
                Debug.Log($"Completed step {stepKey}");
            }
        }

        public void SetStepStarted(string stepKey)
        {
            if (m_TutorialStepStates.TryGetValue(stepKey, out TutorialStepState stepState))
            {
                stepState.IsStarted = true;
                Debug.Log($"Start step {stepKey}");
            }
        }

        public void ClearAllSteps()
        {
            m_TutorialStepStates.Clear();
        }

        public void ResetStepStates()
        {
            foreach (var tutorialStepState in m_TutorialStepStates.Values)
            {
                tutorialStepState.IsCompleted = false;
                tutorialStepState.IsStarted = false;
            }
        }
    }
}