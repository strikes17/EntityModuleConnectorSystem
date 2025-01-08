using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class AnimatorStateMachineModule : AbstractBehaviourModule
    {
        public event Action<string> StateFinished = delegate { };

        [SerializeField] private Animator m_Animator;

        private bool m_StateTriggered;
        private string m_EventStateName;

        private Moroutine m_Moroutine;

        public void SetAnimationStateForced(string stateName)
        {
            m_Animator.Play(stateName);
        }

        public void TriggerAnimationState(string stateName, bool shouldHandleEvents = false)
        {
            m_Animator.SetTrigger(stateName);

            if (shouldHandleEvents)
            {
                m_EventStateName = stateName;
                if (m_Moroutine != null)
                {
                    m_Moroutine.Stop();
                }
                m_StateTriggered = false;
                m_Moroutine = Moroutine.Run(Test());
            }
        }

        private IEnumerator Test()
        {
            yield return new WaitWhile(() => !m_Animator.GetCurrentAnimatorStateInfo(1).IsName(m_EventStateName));
            m_StateTriggered = true;
        }

        public override void OnUpdate()
        {
            if (m_StateTriggered)
            {
                var animatorStateInfo = m_Animator.GetCurrentAnimatorStateInfo(1);
                var normalizedTime = animatorStateInfo.normalizedTime;
                // Debug.Log($"normalizedTime: {normalizedTime} / {1f}");
                if (normalizedTime >= 1f)
                {
                    m_StateTriggered = false;
                    StateFinished(m_EventStateName);
                    Debug.Log($"Finished state: {m_EventStateName}");
                }   
            }
        }
    }
}