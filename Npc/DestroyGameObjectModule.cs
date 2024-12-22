using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    public class DestroyGameObjectModule : AbstractBehaviourModule
    {
        [SerializeField] private float m_Delay;

        private Moroutine m_Moroutine;

        public void Destroy()
        {
            if (m_Moroutine != null)
            {
                m_Moroutine.Stop();
            }

            m_Moroutine = Moroutine.Run(DestroyDelayCoroutine());
        }

        private IEnumerator DestroyDelayCoroutine()
        {
            yield return new WaitForSeconds(m_Delay);
            var gameObject = m_AbstractEntity.gameObject;
            gameObject.SetActive(false);
            Object.Destroy(gameObject);
        }
    }
}