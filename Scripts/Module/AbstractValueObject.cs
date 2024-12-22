using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractValueObject<T> : ICloneable
    {
        public event Action<T> ValueChanged = delegate { };

        [SerializeField] protected T m_Value;

        protected virtual void OnValueChanged(T value) => ValueChanged(m_Value);

        public virtual T Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                OnValueChanged(m_Value);
            }
        }

        public object Clone() => MemberwiseClone();
    }
}