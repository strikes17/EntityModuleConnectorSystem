using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAiVisionModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity> NoticedEntity = delegate { };
        public event Action<AbstractEntity> EntityIsLost = delegate { };

        [SerializeField] private float m_Angle;
        [SerializeField] private float m_MaxVisionDistance;
        [SerializeField] private float m_EntityDetectRadius;
        [SerializeField] private Transform m_VisionTransform;
        [SerializeField] private Transform m_SphereOverlapOriginTransform;
        [SerializeField] private Collider m_Collider;

        [SerializeField, ReadOnly] private List<AbstractEntity> m_CurrentlySeeingEntities = new();

        private int m_EntityLayerMask;

        private bool m_IsUpdating = false;

        private RaycastHit m_RaycastHit;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_EntityLayerMask = 1 << LayerMask.NameToLayer("Entity");
            AllowPostInitialization(5);
        }

        protected override void PostInitialize()
        {
            base.PostInitialize();
            m_IsUpdating = true;
        }

        public override void OnUpdate()
        {
            if (!m_IsUpdating) return;
            var colliders = Physics.OverlapSphere(m_SphereOverlapOriginTransform.position, m_EntityDetectRadius,
                m_EntityLayerMask);
            foreach (var collider in colliders)
            {
                var noticedEntity = collider.GetComponent<AbstractEntity>();
                var direction = (noticedEntity.transform.position - m_VisionTransform.position)
                    .normalized;
                Ray ray = new Ray(m_VisionTransform.position, direction);
                if (noticedEntity != null)
                {
                    // Debug.DrawRay(ray.origin, ray.direction * m_MaxVisionDistance, Color.red, 4f);
                    var forward = m_VisionTransform.forward;
                    var angle = Vector3.Angle(direction, forward);
                    m_Collider.enabled = false;
                    if (Physics.Raycast(ray.origin, ray.direction, out m_RaycastHit, m_MaxVisionDistance))
                    {
                        if (m_RaycastHit.collider.gameObject != m_AbstractEntity.gameObject
                            && m_RaycastHit
                                .collider.GetComponent<AbstractEntity>() != null && angle <= m_Angle)
                        {
                            if (!m_CurrentlySeeingEntities.Contains(noticedEntity))
                            {
                                m_CurrentlySeeingEntities.Add(noticedEntity);
                                NoticedEntity(noticedEntity);
                            }
                        }
                    }

                    m_Collider.enabled = true;
                }
            }

            for (var i = 0; i < m_CurrentlySeeingEntities.Count; i++)
            {
                var noticedEntity = m_CurrentlySeeingEntities[i];
                var direction = (noticedEntity.transform.position - m_VisionTransform.position)
                    .normalized;
                Ray ray = new Ray(m_VisionTransform.position, direction);
                var forward = m_VisionTransform.forward;
                var angle = Vector3.Angle(direction, forward);
                m_Collider.enabled = false;
                if (!Physics.Raycast(ray.origin, ray.direction, out m_RaycastHit, m_MaxVisionDistance) || angle > m_Angle)
                {
                    m_CurrentlySeeingEntities.Remove(noticedEntity);
                    EntityIsLost(noticedEntity);
                }
                m_Collider.enabled = true;
            }
        }
    }
}