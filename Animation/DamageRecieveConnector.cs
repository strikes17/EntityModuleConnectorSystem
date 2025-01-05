using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class DamageRecieveConnector : BehaviourModuleConnector
    {
        [SelfInject] private HealthModule m_HealthModule;
        [SelfInject] private DamageRecieveModule m_DamageRecieveModule;
        
        protected override void Initialize()
        {
            m_DamageRecieveModule.DamageRecieved += DamageRecieveModuleOnDamageRecieved;
        }

        private void DamageRecieveModuleOnDamageRecieved(AbstractEntity damageSource, float damageValue)
        {
            m_HealthModule.Value -= Mathf.RoundToInt(damageValue);
        }
    }
}