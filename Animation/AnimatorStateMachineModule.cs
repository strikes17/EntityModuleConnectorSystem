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
        private string m_StateName;
        private float m_Time;

        private Moroutine m_Moroutine;

        public void SetAnimationState(string stateName, bool shouldHandleEvents = false)
        {
            Debug.Log($"Set state {stateName}");
            
            m_Animator.SetTrigger(stateName);

            if (shouldHandleEvents)
            {
                m_StateName = stateName;
                if (m_Moroutine != null)
                {
                    m_Moroutine.Stop();
                }

                m_Time = 0f;
                m_StateTriggered = false;
                m_Moroutine = Moroutine.Run(Test());
            }
        }

        private IEnumerator Test()
        {
            yield return new WaitWhile(() =>
            {
                var length = m_Animator.GetCurrentAnimatorClipInfo(1).Length;
                Debug.Log($"length: {length}");
                return length == 0;
            });
            m_StateTriggered = true;
        }

        public override void OnUpdate()
        {
            if (m_StateTriggered)
            {
                var length = m_Animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
                m_Time += Time.deltaTime;
                Debug.Log($"m_Time: {m_Time} / {length}");
                if (m_Time >= length)
                {
                    m_StateTriggered = false;
                    m_Time = 0f;
                    StateFinished(m_StateName);
                    Debug.Log($"Finished state: {m_StateName}");
                }
            }
        }
    }
}