using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class AiHostileModule : AbstractBehaviourModule
    {
        public event Action TargetedFinalSpot = delegate { };

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            AllowPostInitialization(3);
        }

        protected override void PostInitialize()
        {
            TargetedFinalSpot();
        }
    }
}