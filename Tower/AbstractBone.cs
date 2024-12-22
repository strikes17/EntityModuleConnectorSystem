using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractBone
    {
        [SerializeField] protected Transform m_Transform;

        public Transform Transform => m_Transform;
    }
}