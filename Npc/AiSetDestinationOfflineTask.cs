using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    [Serializable]
    public class AiSetDestinationOfflineTask : AiOfflineTask
    {
        public override event Action<AiTask> TaskCompleted;

        private WaitForSeconds m_WaitForSeconds;

        private Moroutine m_ProgressMoroutine;

        private float m_SimulatedProgress;
        
        private Vector3 m_TargetDestination;
        private AiNavMeshModule m_AiNavMeshModule;
        
        public AiSetDestinationOfflineTask(AiNavMeshModule navMeshModule, Vector3 targetDestination)
        {
            m_AiNavMeshModule = navMeshModule;
            m_TargetDestination = targetDestination;
        }

        public override void StartResolve()
        {
            if (m_ProgressMoroutine != null)
            {
                m_ProgressMoroutine.Stop();
            }

            m_ProgressMoroutine = Moroutine.Run(Simulate());
        }

        public override void StopResolve()
        {
            if (m_ProgressMoroutine != null)
            {
                m_ProgressMoroutine.Stop();
            }
        }

        private IEnumerator Simulate()
        {
            float interval = 1f;
            m_WaitForSeconds = new WaitForSeconds(interval);
            var path = m_AiNavMeshModule.FindPath(m_TargetDestination);
            if (path != null)
            {
                
            }
            while (true)
            {
                yield return m_WaitForSeconds;
            }
        }
    }
}