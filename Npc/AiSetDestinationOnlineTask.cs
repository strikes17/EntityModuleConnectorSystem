using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    [Serializable]
    public class AiSetDestinationOnlineTask : AiOnlineTask
    {
        private Vector3 m_TargetDestination;
        private AiNavMeshModule m_AiNavMeshModule;

        public AiSetDestinationOnlineTask(AiNavMeshModule navMeshModule, Vector3 targetDestination)
        {
            m_AiNavMeshModule = navMeshModule;
            m_TargetDestination = targetDestination;
        }

        public override event Action<AiTask> TaskCompleted = delegate { };

        public override void StartResolve()
        {
            if (m_IsTaskStarted) return;

            m_IsTaskStarted = true;
            m_AiNavMeshModule.ReachedDestination += AiNavMeshModuleOnReachedDestination;
            m_AiNavMeshModule.SetDestination(m_TargetDestination);
        }

        public override void StopResolve()
        {
            if (!m_IsTaskStarted) return;

            m_IsTaskStarted = false;
            m_AiNavMeshModule.ReachedDestination -= AiNavMeshModuleOnReachedDestination;
            m_AiNavMeshModule.ResetDestination();
        }

        private void AiNavMeshModuleOnReachedDestination(AbstractEntity obj)
        {
            TaskCompleted(this);
        }
    }
}