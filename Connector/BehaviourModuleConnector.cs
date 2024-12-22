using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class BehaviourModuleConnector : IResolveTarget
    {
        public event Action<IResolveTarget> Resolved = delegate { };

        protected abstract void Initialize();

        protected AbstractEntity m_AbstractEntity;

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
                // Debug.Log($"{GetType()} m_IsResolved: {m_IsResolved}");
            }
        }

        [SerializeField, ReadOnly]
        private bool m_IsResolved;

        public void Init(AbstractEntity abstractEntity)
        {
            m_AbstractEntity = abstractEntity;
            Initialize();
        }
    }
}