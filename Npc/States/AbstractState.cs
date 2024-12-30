using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractState
    {
        [SerializeField] protected string m_AnimationStateName;

        public virtual string AnimationStateName => m_AnimationStateName;
    }
}