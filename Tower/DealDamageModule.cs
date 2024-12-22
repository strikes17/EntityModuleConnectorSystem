using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class DealDamageModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity> DamageApplied = delegate { };

        private float m_ReloadTime;
        private float m_TimeToReload;

        private AbstractEntity m_DamageTargetEntity;

        public void SetDamageTarget(AbstractEntity abstractEntity, float reloadTime)
        {
            m_DamageTargetEntity = abstractEntity;
            m_TimeToReload = reloadTime;
        }

        public void RemoveDamageTarget()
        {
            m_DamageTargetEntity = null;
        }

        public override void OnUpdate()
        {
            m_ReloadTime += Time.deltaTime;
            if (m_ReloadTime > m_TimeToReload)
            {
                if (m_DamageTargetEntity != null)
                {
                    DamageApplied(m_DamageTargetEntity);
                    m_ReloadTime = 0f;
                }
            }
        }
    }
}