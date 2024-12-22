using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class StatesModule : AbstractBehaviourModule
    {
        public event Action<AbstractState> StateChanged = delegate { };

        [SerializeReference] private List<AbstractState> m_AbstractStates;

        [SerializeField, ReadOnly] private AbstractState m_CurrentState;

        public AbstractState CurrentState => m_CurrentState;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            AllowPostInitialization(2);
        }

        protected override void PostInitialize()
        {
            SetState<RunState>();
        }

        public void SetState<T>() where T : AbstractState
        {
            var state = m_AbstractStates.FirstOrDefault(x => x.GetType() == typeof(T));
            if (state != null)
            {
                m_CurrentState = state;
                StateChanged(m_CurrentState);
            }
        }
    }
}