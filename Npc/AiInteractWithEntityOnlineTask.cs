using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class AiInteractWithEntityOnlineTask : AiOnlineTask
    {
        private AiNavMeshModule m_AiNavMeshModule;
        private NpcAiLogicModule m_NpcAiLogicModule;
        private AbstractEntity m_TargetEntity;
        private SkinMeshAnimationModule m_SkinMeshAnimationModule;

        public AiInteractWithEntityOnlineTask(AiNavMeshModule navMeshModule, NpcAiLogicModule npcAiLogicModule,
            AbstractEntity target, SkinMeshAnimationModule skinMeshAnimationModule)
        {
            m_AiNavMeshModule = navMeshModule;
            m_NpcAiLogicModule = npcAiLogicModule;
            m_TargetEntity = target;
            m_SkinMeshAnimationModule = skinMeshAnimationModule;
        }

        public override event Action<AiTask> TaskCompleted = delegate { };
        public override event Action<AiTask> TaskFailed = delegate { };

        public override void StartResolve()
        {
            if (m_IsTaskStarted) return;

            m_IsTaskStarted = true;
            m_AiNavMeshModule.ReachedTargetEntity += OnReachedTargetEntity;
            m_AiNavMeshModule.StartFollowPathToEntity(m_TargetEntity, 1f, true);
        }

        public override void StopResolve()
        {
            if (!m_IsTaskStarted) return;

            m_IsTaskStarted = false;
            m_AiNavMeshModule.ResetDestination();
        }

        protected virtual void OnReachedTargetEntity(AbstractEntity follower, AbstractEntity target)
        {
            bool isSuccess = m_NpcAiLogicModule.TryInteractWithEntity(target);
            if (isSuccess)
            {
                TaskCompleted(this);
                // Debug.Log("Completed task of entity interact");
            }
            else
            {
                TaskFailed(this);
                // Debug.Log("Failed task of entity interact");
            }
        }
    }
}