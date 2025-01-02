using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class AiEliminateTaskNpcStateMachine
    {
        public event Action<AiEliminateTaskNpcState> StateChanged = delegate { };

        private List<AiEliminateTaskNpcState> m_AbstractStates = new();

        private AiEliminateTaskNpcState m_CurrentState;

        public AiEliminateTaskNpcStateMachine()
        {
            m_AbstractStates.Add(new AiEliminatePathCompleteState());
            m_AbstractStates.Add(new AiEliminatePathPartialState());
            m_AbstractStates.Add(new AiEliminatePathInvalidState());
        }

        public void SetState<T>() where T : AiEliminateTaskNpcState
        {
            var state = m_AbstractStates.FirstOrDefault(x => x.GetType() == typeof(T));
            if (state != null && m_CurrentState != state)
            {
                m_CurrentState = state;
                StateChanged(m_CurrentState);
                Debug.Log($"State changed to {m_CurrentState}");
            }
        }
    }
}