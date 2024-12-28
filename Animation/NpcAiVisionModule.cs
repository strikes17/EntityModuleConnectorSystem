using System;
using System.Collections.Generic;
using System.Linq;
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

        private bool m_IsUpdating = false;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            AllowPostInitialization(5);
        }

        protected override void PostInitialize()
        {
            base.PostInitialize();
            m_IsUpdating = true;
        }

        public override void OnUpdate()
        {
            if(!m_IsUpdating)return;
            var colliders = Physics.OverlapSphere(m_SphereOverlapOriginTransform.position, m_EntityDetectRadius);
            foreach (var collider in colliders)
            {
                var noticedEntity = collider.GetComponent<AbstractEntity>();
                if (noticedEntity != null)
                {
                    if (m_CurrentlyNoticedEntities.Contains(noticedEntity))
                    {
                        continue;
                    }

                    var direction = (noticedEntity.transform.position - m_VisionTransform.position)
                        .normalized;
                    Ray ray = new Ray(m_VisionTransform.position, direction);
                    Debug.DrawRay(ray.origin, ray.direction * m_MaxVisionDistance, Color.red, 4f);
                    var raycastHits = Physics.RaycastAll(ray, m_MaxVisionDistance).ToList();
                    if (raycastHits.Count > 0)
                    {
                        foreach (var raycastHit in raycastHits)
                        {
                            if (raycastHit.collider.gameObject != m_AbstractEntity.gameObject
                                && raycastHit
                                    .collider.GetComponent<AbstractEntity>() != null)
                            {
                                var forward = m_VisionTransform.forward;
                                var angle = Vector3.Angle(direction, forward);
                                if (angle <= m_Angle)
                                {
                                    Debug.Log($"A!Notice entity : {noticedEntity.name}");
                                    m_CurrentlyNoticedEntities.Add(noticedEntity);
                                    NoticedEntity(noticedEntity);
                                }
                            }
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