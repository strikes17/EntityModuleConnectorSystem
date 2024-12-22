using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    public class PoolableParticleSystem : MonoBehaviour, IPoolable<PoolableParticleSystem>
    {
        [SerializeField] private ParticleSystem m_ParticleSystem;
        [SerializeField] private float m_ParticleStopDelay;

        public ParticleSystem ParticleSystem => m_ParticleSystem;

        public Action<PoolableParticleSystem> Enabled { get; set; }

        public Action<PoolableParticleSystem> Disabled { get; set; }

        public bool IsAvailable => !gameObject.activeSelf;

        public PoolableParticleSystem CreateInstance() => Instantiate(this);

        private Moroutine m_Moroutine;

        private WaitForSeconds m_WaitForSeconds;
        
        

        private void Start()
        {
            m_WaitForSeconds = new WaitForSeconds(m_ParticleStopDelay);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
            m_ParticleSystem.Play();
            Enabled(this);
        }

        public void Disable()
        {
            m_ParticleSystem.Stop();
            Disabled(this);
            
            if (m_Moroutine != null)
            {
                m_Moroutine.Stop();
            }

            m_Moroutine = Moroutine.Run(DelayedDisable());
        }

        private IEnumerator DelayedDisable()
        {
            yield return m_WaitForSeconds;
            gameObject.SetActive(false);
        }
    }
}