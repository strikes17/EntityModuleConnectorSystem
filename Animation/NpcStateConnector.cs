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

        private bool m_IsIntimidating;
        private bool m_IsWeaponPrepared;

        protected override void Initialize()
        {
            m_AiNavMeshModule.StartedMovingToTargetPoint += AiNavMeshModuleOnStartedMovingToTargetPoint;
            m_AiNavMeshModule.ReachedTargetPoint += AiNavMeshModuleOnReachedTargetPoint;
            m_AiNavMeshModule.StartedMovingToTargetEntity += AiNavMeshModuleOnStartedMovingToTargetEntity;
            m_AiNavMeshModule.ReachedTargetEntity += AiNavMeshModuleOnReachedTargetEntity;
            
            m_LogicModule.DecidedToIntimidateNpc += LogicModuleOnDecidedToIntimidateNpc;
            m_LogicModule.DecidedToPrepareWeapon += LogicModuleOnDecidedToPrepareWeapon;
            m_LogicModule.DecidedToHideWeapon += LogicModuleOnDecidedToHideWeapon;
            m_LogicModule.DecidedToStopIntimidating += LogicModuleOnDecidedToStopIntimidating;
        }

        private void LogicModuleOnDecidedToStopIntimidating(NpcEntity arg1, NpcEntity arg2)
        {
            m_IsWeaponPrepared = false;
        }

        private void LogicModuleOnDecidedToHideWeapon(NpcEntity obj)
        {
            m_IsIntimidating = false;
        }

        private void LogicModuleOnDecidedToPrepareWeapon(NpcEntity obj)
        {
            m_IsWeaponPrepared = true;
        }

        private void LogicModuleOnDecidedToIntimidateNpc(NpcEntity arg1, NpcEntity arg2)
        {
            m_IsIntimidating = true;
        }

        private void AiNavMeshModuleOnReachedTargetEntity(AbstractEntity arg1, AbstractEntity arg2)
        {
            if (m_IsIntimidating)
            {
                m_StatesModule.SetState<IdleArmedIntimidatingState>();
            }
            else if (m_IsWeaponPrepared)
            {
                m_StatesModule.SetState<IdleArmedState>();
            }
            else
            {
                m_StatesModule.SetState<IdleState>();
            }
        }

        private void AiNavMeshModuleOnStartedMovingToTargetEntity(AbstractEntity arg1, AbstractEntity arg2)
        {
            if (m_IsIntimidating)
            {
                m_StatesModule.SetState<WalkArmedIntimidatingState>();
            }
            else if (m_IsWeaponPrepared)
            {
                m_StatesModule.SetState<WalkArmedState>();
            }
            else
            {
                m_StatesModule.SetState<WalkState>();
            }
        }

        private void AiNavMeshModuleOnReachedTargetPoint(AbstractEntity obj, Vector3 target)
        {
            if (m_IsIntimidating)
            {
                m_StatesModule.SetState<IdleArmedIntimidatingState>();
            }
            else if (m_IsWeaponPrepared)
            {
                m_StatesModule.SetState<IdleArmedState>();
            }
            else
            {
                m_StatesModule.SetState<IdleState>();
            }
        }

        private void AiNavMeshModuleOnStartedMovingToTargetPoint(AbstractEntity arg1, Vector3 arg2)
        {
            if (m_IsIntimidating)
            {
                m_StatesModule.SetState<WalkArmedIntimidatingState>();
            }
            else if (m_IsWeaponPrepared)
            {
                m_StatesModule.SetState<WalkArmedState>();
            }
            else
            {
                m_StatesModule.SetState<WalkState>();
            }
        }
        
    }
}