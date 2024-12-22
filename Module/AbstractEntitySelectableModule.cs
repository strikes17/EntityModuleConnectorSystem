using System;
using UnityEngine;

namespace _Project.Scripts
{
    public abstract class AbstractEntitySelectableModule : RaycastTargetBehaviourModule
    {
        public event Action<AbstractEntity> Selected = delegate { };
        
        public event Action<AbstractEntity> Deselected = delegate { };

        protected virtual void OnSelected(AbstractEntity abstractEntity)
        {
            Selected(abstractEntity);
        }
        
        protected virtual void OnDeselected(AbstractEntity abstractEntity)
        {
            Deselected(abstractEntity);
        }
        
        public abstract void Select();
        
        public abstract void Deselect();
        
        
        public override void OnStart(RaycastHit raycastHit)
        {
            
        }

        public override void OnEnd(RaycastHit raycastHit)
        {
            
        }

        public override void OnHold(RaycastHit raycastHit)
        {
            
        }
    }
}