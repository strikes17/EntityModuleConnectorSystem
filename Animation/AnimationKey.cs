using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AnimationKey
    {
        [SerializeField] protected string m_Key;

        public virtual string Key => m_Key;
    }
}