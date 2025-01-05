using System;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    [Serializable]
    public class AiSetDestinationOnlineTask : AiOnlineTask
    {
        protected Transform m_TargetDestination;
        protected AiNavMeshModule m_AiNavMeshModule;
        protected SkinMeshAnimationModule m_SkinMeshAnimationModule;

        public AiSetDestinationOnlineTask(AiNavMeshModule navMeshModule, Transform targetDestination, SkinMeshAnimationModule skinMeshAnimationModule)
        {
            m_AiNavMeshModule = navMeshModule;
            m_TargetDestination = targetDestination;
            m_SkinMeshAnimationModule = skinMeshAnimationModule;
        }

        public override event Action<AiTask> TaskCompleted = delegate { };
        public override event Action<AiTask> TaskFailed = delegate { };

        public override void StartResolve()
        {
            if (m_IsTaskStarted) return;
            m_IsTaskStarted = true;
            m_AiNavMeshModule.ReachedTargetPoint += AiNavMeshModuleOnReachedTargetPoint;
            m_AiNavMeshModule.StartFollowPathToPoint(m_TargetDestination.position, true);
        }

        public override void StopResolve()
        {
            if (!m_IsTaskStarted) return;

            m_IsTaskStarted = false;
            m_AiNavMeshModule.ReachedTargetPoint -= AiNavMeshModuleOnReachedTargetPoint;
            m_AiNavMeshModule.ResetDestination();
        }

        protected virtual void AiNavMeshModuleOnReachedTargetPoint(AbstractEntity obj, Vector3 target)
        {
            TaskCompleted(this);
            // Debug.Log($"Completed task of destintion target point {target}");
        }
    }
}