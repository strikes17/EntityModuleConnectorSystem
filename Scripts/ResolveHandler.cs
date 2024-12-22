using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ResolveHandler<T>
    {
        [SerializeReference] private T m_Value;
        [SerializeField] private AbstractEntity m_AbstractEntity;

        public T Value => m_Value;

        public AbstractEntity AbstractEntity => m_AbstractEntity;

        public ResolveHandler(AbstractEntity abstractEntity,
            T value)
        {
            m_Value = value;
            m_AbstractEntity = abstractEntity;
        }
    }
}