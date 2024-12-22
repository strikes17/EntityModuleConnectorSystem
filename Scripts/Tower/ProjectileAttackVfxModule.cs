using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class ProjectileAttackVfxModule : AbstractAttackVfxModule
    {
        [SerializeField] private PoolableParticleSystem m_ProjectilePrefab;
        [SerializeField] private Transform m_StartTransform;
        [SerializeField] private float m_ReachSpeed;
        [SerializeField] private float m_ReachMinDistance;

        private Pool<PoolableParticleSystem> m_Pool;

        [SerializeField] private List<PoolableParticleSystem> m_ActiveParticles = new();

        private Transform m_TargetTransform;

        private AbstractEntity m_AbstractEntityTarget;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_ReachMinDistance *= m_ReachMinDistance;
            m_Pool = new Pool<PoolableParticleSystem>(m_ProjectilePrefab);
            m_Pool.ElementEnabled += PoolOnElementEnabled;
            m_Pool.ElementDisabled += PoolOnElementDisabled;
        }

        private void PoolOnElementDisabled(PoolableParticleSystem poolableGameObject)
        {
            m_ActiveParticles.Remove(poolableGameObject);
        }

        private void PoolOnElementEnabled(PoolableParticleSystem poolableGameObject)
        {
            m_ActiveParticles.Add(poolableGameObject);
        }

        public override void OnUpdate()
        {
            if (m_AbstractEntityTarget == null)
            {
                return;
            }
            
            for (int i = 0;i < m_ActiveParticles.Count;i++)
            {
                var poolableParticleSystem = m_ActiveParticles[i];
                var transform = poolableParticleSystem.transform;

                var position = transform.position;
                var targetPosition = m_TargetTransform.position;

                var direction = (targetPosition - position).normalized;

                position += direction * (m_ReachSpeed * Time.deltaTime);

                transform.position = position;
                
                var rotation = Quaternion.LookRotation(direction);

                transform.rotation = rotation;


                var distance = (targetPosition - position).sqrMagnitude;
                if (distance <= m_ReachMinDistance)
                {
                    poolableParticleSystem.Disable();
                    OnReachedTarget(m_AbstractEntityTarget);
                }
            }
        }

        public override void Start(AbstractEntity abstractEntity, AbstractBone abstractBone)
        {
            m_TargetTransform = abstractBone.Transform;
            m_AbstractEntityTarget = abstractEntity;

            var particleSystem = m_Pool.Get();
            particleSystem.transform.parent = m_AbstractEntity.transform;
            particleSystem.transform.position = m_StartTransform.position;
            particleSystem.Enable();
        }
    }
}