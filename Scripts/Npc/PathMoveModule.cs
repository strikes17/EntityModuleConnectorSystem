using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Redcode.Moroutines;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class PathMoveModule : AbstractBehaviourModule
    {
        public event Action<Vector2> StartedFollowingNextNode = delegate { };
        public event Action<Vector2> ArrivedAtFinalNode = delegate { };
        public event Action<Vector2> StartedFollowingPath = delegate { };

        private List<Vector2> m_CurrentPath;
        private Vector2 m_FinalNode;
        private Vector2 m_StartingNode;
        private Moroutine m_MovePathCoroutine;
        private Moroutine m_MovePathNodeCoroutine;
        private int m_CurrentPathIndex;

        public void StartFollowingAlongPath(List<Vector2> path)
        {
            m_CurrentPath = path;
            m_StartingNode = m_CurrentPath.First();
            m_FinalNode = m_CurrentPath.Last();
            m_CurrentPathIndex = 1;

            if (m_StartingNode != m_FinalNode)
            {
                if (m_MovePathCoroutine != null)
                {
                    m_MovePathCoroutine.Stop();
                }

                m_MovePathCoroutine = Moroutine.Run(TryFollowPathCoroutine());
            }
        }

        private IEnumerator TryFollowPathCoroutine()
        {
            StartedFollowingPath(m_CurrentPath[m_CurrentPathIndex]);
            
            Transform transform = m_AbstractEntity.transform;
            
            float distance;
            do
            {
                var position = new Vector2(transform.position.x,
                    transform.position.z);
                distance = (position - m_FinalNode).magnitude;

                m_MovePathNodeCoroutine = Moroutine.Run(TryFollowNextPathNode());
                yield return m_MovePathNodeCoroutine.WaitForComplete();
            } while (distance > 0.01f);

            ArrivedAtFinalNode(m_FinalNode);
        }

        private IEnumerator TryFollowNextPathNode()
        {
            if (m_CurrentPathIndex + 1 >= m_CurrentPath.Count)
            {
                yield break;
            }
            Vector2 targetPoint = m_CurrentPath[m_CurrentPathIndex];

            Transform transform = m_AbstractEntity.transform;

            float distance;
            do
            {
                var position = new Vector2(transform.position.x,
                    transform.position.z);
                distance = (position - targetPoint).magnitude;

                yield return null;
            } while (distance > 0.01f);

            m_CurrentPathIndex++;
            StartedFollowingNextNode(m_CurrentPath[m_CurrentPathIndex]);
        }
        
        protected override void OnDestroyed(GameObject gameObject)
        {
            if (m_MovePathCoroutine != null)
            {
                m_MovePathCoroutine.Stop();
            }
            
            if (m_MovePathNodeCoroutine != null)
            {
                m_MovePathNodeCoroutine.Stop();
            }
        }
    }
}