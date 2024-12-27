using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAiVisionModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity> NoticedEntity = delegate { };
        public event Action<AbstractEntity> EntityLostFromSight = delegate { };

        [SerializeField] private float m_Angle;
        [SerializeField] private float m_MaxVisionDistance;
        [SerializeField] private float m_EntityDetectRadius;
        [SerializeField] private Transform m_VisionTransform;
        [SerializeField] private Transform m_SphereOverlapOriginTransform;

        private List<AbstractEntity> m_CurrentlyNoticedEntities = new();

        public override void OnUpdate()
        {
            var colliders = Physics.OverlapSphere(m_SphereOverlapOriginTransform.position, m_EntityDetectRadius);
            foreach (var collider in colliders)
            {
                var noticedEntity = collider.GetComponent<AbstractEntity>();
                if (noticedEntity != null)
                {
                    if (m_CurrentlyNoticedEntities.Contains(noticedEntity))
                        continue;

                    if (Physics.Raycast(m_VisionTransform.position, noticedEntity.transform.position,
                            m_MaxVisionDistance))
                    {
                        var direction = (noticedEntity.transform.position - m_VisionTransform.position).normalized;
                        var forward = m_VisionTransform.forward;
                        var angle = Vector3.Angle(direction, forward);
                        if (angle <= m_Angle)
                        {
                            m_CurrentlyNoticedEntities.Add(noticedEntity);
                            NoticedEntity(noticedEntity);
                        }
                    }
                }
            }

            for (var i = 0; i < m_CurrentlyNoticedEntities.Count; i++)
            {
                var currentlyNoticedEntity = m_CurrentlyNoticedEntities[i];
                if (Vector3.Distance(m_VisionTransform.position, currentlyNoticedEntity.transform.position) >
                    m_MaxVisionDistance)
                {
                    m_CurrentlyNoticedEntities.Remove(currentlyNoticedEntity);
                    EntityLostFromSight(currentlyNoticedEntity);
                }
            }
        }
    }
}