using System;
using UnityEngine;

namespace _Project.Scripts
{
    public abstract class AbstractAttackVfxModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity> ReachedTarget = delegate { };

        protected virtual void OnReachedTarget(AbstractEntity abstractEntity)
        {
            ReachedTarget(abstractEntity);
        }

        public abstract void Start(AbstractEntity abstractEntity, AbstractBone abstractBone);
    }
}