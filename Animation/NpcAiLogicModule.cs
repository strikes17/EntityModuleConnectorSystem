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

        public event Action<AbstractEntity, NpcEntity> DecidedToEliminateEntity = delegate {  };

        [SerializeField] private NpcPointOfInterestValue m_MinimumPointOfInterestValue;

        public NpcPointOfInterestValue MinimumPointOfInterestValue => m_MinimumPointOfInterestValue;

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