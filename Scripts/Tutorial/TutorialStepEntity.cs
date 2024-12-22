using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Camera;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialStepEntity : AbstractEntity
    {
        public event Action<string> StepStarted = delegate { };

        public event Action<string> StepCompleted = delegate { };

        public string TutorialStepKey => m_TutorialStepKey;

        [SerializeField] protected string m_TutorialStepKey;

        [SerializeReference, Header("OnStart")]
        protected List<AbstractTutorialStepCondition> m_StartConditions;

        [SerializeReference] protected List<AbstractTutorialAction> m_StartActions;

        [SerializeReference, Header("OnEnd")] protected List<AbstractTutorialStepCondition> m_EndConditions;
        [SerializeReference] protected List<AbstractTutorialAction> m_EndActions;

        [SerializeField, ReadOnly] private int m_TotalStartConditionsCount;
        [SerializeField, ReadOnly] private int m_CompletedStartConditionsCount;

        [SerializeField, ReadOnly] private int m_TotalEndConditionsCount;
        [SerializeField, ReadOnly] private int m_CompletedEndConditionsCount;

        public IEnumerable<AbstractTutorialAction> StartActions => m_StartActions;

        public IEnumerable<AbstractTutorialStepCondition> StartConditions => m_StartConditions;

        public IEnumerable<AbstractTutorialAction> EndActions => m_EndActions;

        public IEnumerable<AbstractTutorialStepCondition> EndConditions => m_EndConditions;

        protected override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);

            m_TotalStartConditionsCount = m_StartConditions.Count;
            m_TotalEndConditionsCount = m_EndConditions.Count;

            foreach (var startCondition in m_StartConditions)
            {
                if (startCondition.IsResolved)
                {
                    startCondition.Initialize(this);
                }
                else
                {
                    startCondition.Resolved += OnConditionResolved;
                }

                startCondition.Completed += StartConditionOnCompleted;
            }
        }

        private void OnConditionResolved(IResolveTarget resolveTarget)
        {
            var condition = resolveTarget as AbstractTutorialStepCondition;
            condition.Initialize(this);
        }

        private void StartConditionOnCompleted(AbstractTutorialStepCondition stepCondition)
        {
            Moroutine.Run(StartStepEndOfFrame());
        }

        private IEnumerator StartStepEndOfFrame()
        {
            yield return new WaitForEndOfFrame();

            m_CompletedStartConditionsCount++;
            if (m_CompletedStartConditionsCount == m_TotalStartConditionsCount)
            {
                StepStarted(m_TutorialStepKey);
                foreach (var startAction in m_StartActions)
                {
                    if (startAction.IsResolved)
                    {
                        startAction.Execute();
                    }
                }

                foreach (var endCondition in m_EndConditions)
                {
                    if (endCondition.IsResolved)
                    {
                        endCondition.Initialize(this);
                    }
                    else
                    {
                        endCondition.Resolved += OnConditionResolved;
                    }
                    endCondition.Completed += EndConditionOnCompleted;
                }
            }
        }

        private void EndConditionOnCompleted(AbstractTutorialStepCondition obj)
        {
            m_CompletedEndConditionsCount++;
            if (m_CompletedEndConditionsCount == m_TotalEndConditionsCount)
            {
                foreach (var endAction in m_EndActions)
                {
                    if (endAction.IsResolved)
                    {
                        endAction.Execute();
                    }
                }

                StepCompleted(m_TutorialStepKey);
            }
        }
    }
}