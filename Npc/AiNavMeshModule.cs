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
        public event Action<AbstractEntity> ReachedDestination = delegate { };
        public event Action<AbstractEntity, Vector3> StartedMovingToDestination = delegate { };

        [SerializeField] private NavMeshAgent m_NavMeshAgent;

        private NavMeshPath m_NavMeshPath;
        public Vector3 AgentPosition => m_AbstractEntity.transform.position;

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

        private Moroutine m_Moroutine;

        public NavMeshPath FindPath(Vector3 target)
        {
            if (IsPathValid(target))
            {
                return m_NavMeshPath;
            }

            return null;
        }

        public void SetDestination(Vector3 target)
        {
            if (m_Moroutine != null)
            {
                m_Moroutine.Stop();
            }

            m_Moroutine = Moroutine.Run(FollowPath(target));
        }

        public void ResetDestination()
        {
            m_NavMeshAgent.ResetPath();
        }

        private IEnumerator FollowPath(Vector3 target)
        {
            m_NavMeshAgent.SetPath(m_NavMeshPath);
            m_NavMeshAgent.updateRotation = true;

            yield return null;

            StartedMovingToDestination(m_AbstractEntity, target);

            while (m_NavMeshAgent.remainingDistance > 1f)
            {
                Vector3 positionOld = AgentPosition;
                yield return new WaitForEndOfFrame();
                Vector3 delta = AgentPosition - positionOld;
                yield return null;
            }

            ReachedDestination(m_AbstractEntity);
        }
    }
}