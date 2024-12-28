using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    [Serializable]
    public class AiNavMeshModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, Vector3> ReachedTargetPoint = delegate { };
        public event Action<AbstractEntity, Vector3> StartedMovingToTargetPoint = delegate { };
        
        public event Action<AbstractEntity, AbstractEntity> ReachedTargetEntity= delegate { };
        public event Action<AbstractEntity, AbstractEntity> StartedMovingToTargetEntity = delegate { };

        [SerializeField] private NavMeshAgent m_NavMeshAgent;

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
            m_NavMeshPath = new NavMeshPath();
            bool hasPath = m_NavMeshAgent.CalculatePath(target, m_NavMeshPath);
            if (!hasPath || m_NavMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
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

        private Moroutine m_FollowPointMoroutine;
        private Moroutine m_FollowEntityMoroutine;

        public NavMeshPath FindPath(Vector3 target)
        {
            if (IsPathValid(target))
            {
                return m_NavMeshPath;
            }

            return null;
        }
        
        public void StartFollowPathToEntity(AbstractEntity target)
        {
            if (m_FollowEntityMoroutine != null)
            {
                m_FollowEntityMoroutine.Stop();
            }

            m_FollowEntityMoroutine = Moroutine.Run(FollowPathToEntity(target));
        }

        public void StartFollowPathToPoint(Vector3 target)
        {
            if (m_FollowPointMoroutine != null)
            {
                m_FollowPointMoroutine.Stop();
            }

            m_FollowPointMoroutine = Moroutine.Run(FollowPathToPoint(target));
        }

        public void ResetDestination()
        {
            m_NavMeshAgent.ResetPath();
        }
        
        private IEnumerator FollowPathToEntity(AbstractEntity target)
        {
            m_NavMeshAgent.SetDestination(target.transform.position);
            m_NavMeshAgent.updateRotation = true;

            yield return null;

            StartedMovingToTargetEntity(m_AbstractEntity, target);

            while (m_NavMeshAgent.remainingDistance > 1f)
            {

                m_NavMeshAgent.SetDestination(target.transform.position);
                yield return null;
            }

            bool isgood = m_NavMeshAgent.CalculatePath(target.transform.position, m_NavMeshPath);
            Debug.Log($"is good: {isgood}");
            Debug.Log($"remLEFT: {m_NavMeshAgent.remainingDistance}");

            ReachedTargetEntity(m_AbstractEntity, target);
        }

        private IEnumerator FollowPathToPoint(Vector3 target)
        {
            m_NavMeshAgent.SetPath(m_NavMeshPath);
            m_NavMeshAgent.updateRotation = true;

            yield return null;

            StartedMovingToTargetPoint(m_AbstractEntity, target);

            while (m_NavMeshAgent.remainingDistance > 1f)
            {
                yield return null;
            }

            ReachedTargetPoint(m_AbstractEntity, target);
        }
    }
}