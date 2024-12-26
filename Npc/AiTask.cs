using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AiTask
    {
        public abstract event Action<AiTask> TaskCompleted;
        public abstract void StartResolve();

        public abstract void StopResolve();

        [SerializeField, ReadOnly] protected bool m_IsTaskStarted;
    }
}