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
            m_AiNavMeshModule.StartedMovingToDestination += AiNavMeshModuleOnStartedMovingToDestination;
            m_AiNavMeshModule.ReachedDestination += AiNavMeshModuleOnReachedDestination;
        }

        private void AiNavMeshModuleOnReachedDestination(AbstractEntity obj)
        {
            m_StatesModule.SetState<IdleState>();
        }

        private void AiNavMeshModuleOnStartedMovingToDestination(AbstractEntity arg1, Vector3 arg2)
        {
            m_StatesModule.SetState<WalkState>();
        }
        
    }
}