using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public abstract class AbstractContainerModule<T> : AbstractBehaviourModule
    {
        public event Action<T> ElementAdded = delegate(T obj) { };
        public event Action<T> ElementRemoved = delegate(T obj) { };

        private Dictionary<int, T> m_ContainerList = new();

        public IEnumerable<T> ContainerCollection => m_ContainerList.Values;

        public virtual void AddElement(T element)
        {
            m_ContainerList.TryAdd(element.GetHashCode(), element);
            ElementAdded(element);
        }

        public virtual void RemoveElement(T element)
        {
            m_ContainerList.Remove(element.GetHashCode());
            ElementRemoved(element);
        }
    }
}