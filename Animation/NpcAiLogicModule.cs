using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAiLogicModule : AbstractBehaviourModule
    {
        public event Action<Vector3> DecidedToReachPoint = delegate { };

        public event Action<AbstractEntity, NpcEntity> DecidedToInteractWithEntity = delegate { };

        public event Action<AbstractEntity, NpcEntity> DecidedToEliminateEntity = delegate { };

        /// <summary>
        /// 1-st is the target, 2nd-param is NPC who is making decision
        /// </summary>
        public event Action<NpcEntity, NpcEntity> DecidedToIntimidateNpc = delegate { };

        public event Action<NpcEntity> DecidedToReload = delegate { };
        
        public event Action<NpcEntity> DecidedToPrepareWeapon = delegate { };

        public event Action<NpcEntity> DecidedToHideWeapon= delegate { };
        
        public event Action<NpcEntity, NpcEntity> DecidedToStopIntimidating = delegate { };
        
        [SerializeField] private NpcPointOfInterestValue m_MinimumPointOfInterestValue;

        public NpcPointOfInterestValue MinimumPointOfInterestValue => m_MinimumPointOfInterestValue;
        
        public void TryToHideWeapon(NpcEntity npcEntity)
        {
            DecidedToHideWeapon(m_AbstractEntity as NpcEntity);
        }

        public void TryStopIntimidateNpc(NpcEntity npcEntity)
        {
            DecidedToStopIntimidating(npcEntity, m_AbstractEntity as NpcEntity);
        }
        
        public void TryToPrepareWeapon(NpcEntity npcEntity)
        {
            DecidedToPrepareWeapon(m_AbstractEntity as NpcEntity);
        }

        public void TryIntimidateNpc(NpcEntity npcEntity)
        {
            DecidedToIntimidateNpc(npcEntity, m_AbstractEntity as NpcEntity);
        }

        public bool TrySetEliminationTarget(AbstractEntity abstractEntity)
        {
            if (abstractEntity != null)
            {
                DecidedToEliminateEntity(abstractEntity, m_AbstractEntity as NpcEntity);
                return true;
            }

            return false;
        }

        public bool TryInteractWithEntity(AbstractEntity abstractEntity)
        {
            var interactModule = abstractEntity.GetBehaviorModuleByType<EntityInteractModule>();
            if (interactModule != null)
            {
                if (interactModule.IsInteractable)
                {
                    DecidedToInteractWithEntity(abstractEntity, m_AbstractEntity as NpcEntity);
                    return true;
                }
            }

            return false;
        }
    }
}