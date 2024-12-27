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

        [SerializeField] private NpcPointOfInterestValue m_MinimumPointOfInterestValue;

        public NpcPointOfInterestValue MinimumPointOfInterestValue => m_MinimumPointOfInterestValue;
    }
}