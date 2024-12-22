using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractTutorialStepCondition : IResolveTarget
    {
        public event Action<AbstractTutorialStepCondition> Completed = delegate { };

        protected virtual void OnCompleted()
        {
            Debug.Log($"Completed: {GetType()}");
            Completed(this);
        }

        public virtual void Initialize(AbstractEntity abstractEntity)
        {
            
        }

        public event Action<IResolveTarget> Resolved = delegate { };

        public virtual bool IsResolved
        {
            get => m_IsResolved;
            set
            {
                m_IsResolved = value;
                if (m_IsResolved)
                {
                    Resolved(this);
                }
                Debug.Log($"{GetType()} m_IsResolved: {m_IsResolved}");
            }
        }

        [SerializeField, ReadOnly] private bool m_IsResolved;
    }
}