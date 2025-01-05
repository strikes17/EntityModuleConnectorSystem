using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponDamageDealConnector : BehaviourModuleConnector
    {
        [SelfInject] private WeaponModule m_WeaponModule;

        protected override void Initialize()
        {
            m_WeaponModule.BulletHit += WeaponModuleOnBulletHit;
        }

        private void WeaponModuleOnBulletHit(AbstractEntity damageSource, RaycastHit hitTarget)
        {
            Debug.Log($"Hit {hitTarget.collider.name}");
            var targetEntity = hitTarget.collider.GetComponent<AbstractEntity>();
            if (targetEntity != null)
            {
                var damageRecieveModule = targetEntity.GetBehaviorModuleByType<DamageRecieveModule>();
                if (damageRecieveModule != null)
                {
                    damageRecieveModule.DealDamage(damageSource, 10f);
                }
            }
        }
    }
}