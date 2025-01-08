﻿using System;
using UnityEngine;
using AbstractState = _Project.Scripts.AbstractState;

namespace _Project.Scripts
{
    [Serializable]
    public class StatesAnimationConnector : BehaviourModuleConnector
    {
        [SerializeField] private AnimationDataObject m_AnimationDataObject;

        [SelfInject] private AnimatorStateMachineModule m_AnimatorStateMachineModule;
        [SelfInject] private StatesModule m_StatesModule;
        
        protected override void Initialize()
        {
            m_StatesModule.StateChanged += StatesModuleOnStateChanged;
        }

        private void StatesModuleOnStateChanged(AbstractState state)
        {
            // Debug.Log($"On state changed to {state.GetType()} {Time.deltaTime}");
            m_AnimatorStateMachineModule.TriggerAnimationState(m_AnimationDataObject.GetAnimationStateName(state.GetType()));
        }
    }
}