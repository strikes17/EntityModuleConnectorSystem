using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcStateConnector : BehaviourModuleConnector
    {
        [SelfInject] private StatesModule m_StatesModule;
        [SelfInject] private AiNavMeshModule m_AiNavMeshModule;
        [SelfInject] private NpcAiLogicModule m_LogicModule;
        [SelfInject] private NpcAnimatorBlendTreeModule m_AnimatorBlendTreeModule;

        protected override void Initialize()
        {
            m_AiNavMeshModule.Moved += AiNavMeshModuleOnMoved;
            m_LogicModule.DecidedToIntimidateNpc += LogicModuleOnDecidedToIntimidateNpc;
            m_LogicModule.DecidedToPrepareWeapon += LogicModuleOnDecidedToPrepareWeapon;
            m_LogicModule.DecidedToHideWeapon += LogicModuleOnDecidedToHideWeapon;
            m_LogicModule.DecidedToStopIntimidating += LogicModuleOnDecidedToStopIntimidating;
        }

        private void AiNavMeshModuleOnMoved(AbstractEntity entity, float speed)
        {
            m_AnimatorBlendTreeModule.Speed = speed;
        }

        private void LogicModuleOnDecidedToStopIntimidating(NpcEntity arg1, NpcEntity arg2)
        {
            m_StatesModule.SetState<ArmedState>();
        }

        private void LogicModuleOnDecidedToHideWeapon(NpcEntity obj)
        {
            m_StatesModule.SetState<CalmState>();
        }

        private void LogicModuleOnDecidedToPrepareWeapon(NpcEntity obj)
        {
            m_StatesModule.SetState<ArmedState>();
        }

        private void LogicModuleOnDecidedToIntimidateNpc(NpcEntity arg1, NpcEntity arg2)
        {
            m_StatesModule.SetState<IntimidatingState>();
        }
    }
}