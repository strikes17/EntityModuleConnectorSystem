using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcStateConnector : BehaviourModuleConnector
    {
        [SelfInject] private StatesModule m_StatesModule;
        [SelfInject] private AiNavMeshModule m_AiNavMeshModule;
        
        protected override void Initialize()
        {
            m_AiNavMeshModule.StartedMovingToTargetPoint += AiNavMeshModuleOnStartedMovingToTargetPoint;
            m_AiNavMeshModule.ReachedTargetPoint += AiNavMeshModuleOnReachedTargetPoint;
            m_AiNavMeshModule.StartedMovingToTargetEntity += AiNavMeshModuleOnStartedMovingToTargetEntity;
            m_AiNavMeshModule.ReachedTargetEntity += AiNavMeshModuleOnReachedTargetEntity;
        }

        private void AiNavMeshModuleOnReachedTargetEntity(AbstractEntity arg1, AbstractEntity arg2)
        {
            m_StatesModule.SetState<IdleState>();
        }

        private void AiNavMeshModuleOnStartedMovingToTargetEntity(AbstractEntity arg1, AbstractEntity arg2)
        {
            m_StatesModule.SetState<WalkState>();
        }

        private void AiNavMeshModuleOnReachedTargetPoint(AbstractEntity obj, Vector3 target)
        {
            m_StatesModule.SetState<IdleState>();
        }

        private void AiNavMeshModuleOnStartedMovingToTargetPoint(AbstractEntity arg1, Vector3 arg2)
        {
            m_StatesModule.SetState<WalkState>();
        }
        
    }
}