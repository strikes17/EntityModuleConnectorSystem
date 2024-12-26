using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAiLogicModule : AbstractBehaviourModule
    {
        public event Action<Vector3> DecidedToReachPoint = delegate { };

        public void CheckEntity(AbstractEntity entity)
        {
            
        }
    }
}