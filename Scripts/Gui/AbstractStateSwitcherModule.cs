using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public abstract class AbstractStateSwitcherModule : GuiAbstractBehaviourModule
    {
        public int State
        {
            get => m_State;
            set
            {
                m_State = Mathf.Clamp(value, 0, MaxState);
                OnStateChanged();
            }
        }

        public abstract int MaxState { get; }

        private int m_State;

        [Button]
        private void SetDebugState(int state, AbstractEntity abstractEntity)
        {
            Initialize(abstractEntity);
            State = state;
        }

        protected abstract void OnStateChanged();
    }
}