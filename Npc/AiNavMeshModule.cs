using System;
using System.Collections;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    [Serializable]
    public class AiNavMeshModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, float> Moved = delegate { };
        
        public event Action<AbstractEntity, Vector3> ReachedTargetPoint = delegate { };

        public event Action<AbstractEntity, AbstractEntity> ReachedTargetEntity = delegate { };

        [SerializeField] private NavMeshAgent m_NavMeshAgent;

        [SerializeField, ReadOnly] private Vector3 m_LatestInterruptedPosition;

        public Vector3 Velocity => m_NavMeshAgent.velocity;

        private NavMeshPath m_NavMeshPath;

        public float TotalDistanceToDestination
        {
            get
            {
                if (m_NavMeshPath.status != NavMeshPathStatus.PathComplete)
                {
                    return 0f;
                }

                var corners = m_NavMeshPath.corners;
                float totalDistance = 0f;
                for (var i = 0; i < corners.Length - 1; i++)
                {
                    var corner = corners[i];
                    var nextCorner = corners[i + 1];
                    var distance = Vector3.Distance(corner, nextCorner);
                    totalDistance += distance;
                }

                return totalDistance;
            }
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_NavMeshPath = new();
        }

        public bool IsPathValid(Vector3 target)
        {
            m_NavMeshAgent.CalculatePath(target, m_NavMeshPath);
            if (m_NavMeshPath.status != NavMeshPathStatus.PathComplete)
            {
                Debug.Log($"Path is invalid - {m_NavMeshPath.status}");
                return false;
            }

            if (Vector3.Distance(m_NavMeshAgent.transform.position, target) < 1f)
            {
                Debug.Log($"Already at destination");
                return false;
            }

            return true;
        }
        
        public NavMeshPathStatus GetPathStatus(Vector3 target)
        {
            m_NavMeshAgent.CalculatePath(target, m_NavMeshPath);
            return m_NavMeshPath.status;
        }

        private Moroutine m_FollowPointMoroutine;
        private Moroutine m_FollowEntityMoroutine;

        public override void OnUpdate()
        {
            if (m_NavMeshAgent == null)
            {
                return;
            }
            Moved(m_AbstractEntity, m_NavMeshAgent.velocity.magnitude);
        }

        public NavMeshPath FindPath(Vector3 target)
        {
            if (IsPathValid(target))
            {
                return m_NavMeshPath;
            }

            return null;
        }

        public (Vector3, bool) FindClosestEdge(Vector3 targetPoint)
        {
            if (NavMesh.SamplePosition(targetPoint, out var navMeshHit, 10f, NavMesh.AllAreas))
            {
                return (navMeshHit.position, true);
            }

            return (Vector3.zero, false);
        }

        public void StartFollowPathToEntity(AbstractEntity target, float distanceToAccomplish = 1f, bool useAutoRotation = false)
        {
            m_NavMeshAgent.updateRotation = useAutoRotation;
            if (m_FollowEntityMoroutine != null)
            {
                m_FollowEntityMoroutine.Stop();
            }
            if (m_FollowPointMoroutine != null)
            {
                m_FollowPointMoroutine.Stop();
            }
            m_FollowEntityMoroutine = Moroutine.Run(FollowPathToEntity(target, distanceToAccomplish));
        }

        public void StartFollowPathToPoint(Vector3 target, bool useAutoRotation = false)
        {
            m_NavMeshAgent.updateRotation = useAutoRotation;
            if (m_FollowPointMoroutine != null)
            {
                m_FollowPointMoroutine.Stop();
            }
            if (m_FollowEntityMoroutine != null)
            {
                m_FollowEntityMoroutine.Stop();
            }
            m_FollowPointMoroutine = Moroutine.Run(FollowPathToPoint(target));
        }

        public void ResetDestination()
        {
            Stop();
            m_NavMeshAgent.ResetPath();
        }

        public void Stop()
        {
            if (m_FollowEntityMoroutine != null)
            {
                m_FollowEntityMoroutine.Stop();
            }

            if (m_FollowPointMoroutine != null)
            {
                m_FollowPointMoroutine.Stop();
            }
        }

        private IEnumerator FollowPathToEntity(AbstractEntity target, float distanceToAccomplish = 1f)
        {
            if (!m_AbstractEntity.gameObject.activeSelf)
                yield break;
            m_NavMeshAgent.isStopped = false;
            m_NavMeshAgent.SetDestination(target.transform.position);

            yield return null;

            while (m_AbstractEntity.gameObject.activeSelf &&
                   Vector3.Distance(m_AbstractEntity.transform.position, target.transform.position) > distanceToAccomplish)
            {
                m_NavMeshAgent.SetDestination(target.transform.position);
                // Debug.Log($"Moving to entity :{target.name}");
                yield return null;
            }

            m_NavMeshAgent.isStopped = true;
            ReachedTargetEntity(m_AbstractEntity, target);
        }

        private IEnumerator FollowPathToPoint(Vector3 target)
        {
            if (!m_AbstractEntity.gameObject.activeSelf)
                yield break;
            m_NavMeshAgent.CalculatePath(target, m_NavMeshPath);
            m_NavMeshAgent.SetPath(m_NavMeshPath);

            yield return null;

            while (m_AbstractEntity.gameObject.activeSelf &&
                   Vector3.Distance(m_AbstractEntity.transform.position, target) > 1f)
            {
                // m_NavMeshAgent.SetPath(m_NavMeshPath);
                Debug.Log($"Moving to point :{target}");
                yield return null;
            }

            m_NavMeshAgent.isStopped = true;
            ReachedTargetPoint(m_AbstractEntity, target);
        }
    }
}