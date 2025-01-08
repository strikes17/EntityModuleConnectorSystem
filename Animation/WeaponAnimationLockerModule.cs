using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponAnimationLockerModule : AbstractBehaviourModule
    {
        public event Action AnimationFinished = delegate { };

        [SerializeField] private Animator m_Animator;

        private bool m_IsUpdating;
        private Moroutine m_Moroutine;

        public void Start()
        {
            Debug.Log($"START");
            if (m_Moroutine != null)
            {
                m_Moroutine.Stop();
            }

            m_Moroutine = Moroutine.Run(Test());
        }

        private IEnumerator Test()
        {
            yield return new WaitWhile(() => !m_Animator.GetCurrentAnimatorStateInfo(1).IsName("Draw_Entry"));
            m_IsUpdating = true;
        }

        public override void OnUpdate()
        {
            if (!m_IsUpdating)
            {
                return;
            }

            var stateInfo = m_Animator.GetCurrentAnimatorStateInfo(1);
            // Debug.Log($"DRAW {stateInfo.normalizedTime}");
            if (stateInfo.normalizedTime >= 1f)
            {
                AnimationFinished();
                m_IsUpdating = false;
                // Debug.Log("FINISHED DRAW");
            }
        }
    }
}