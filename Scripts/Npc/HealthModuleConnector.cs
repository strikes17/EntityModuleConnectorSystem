using System.Collections.Generic;
using _Project.Scripts.Camera;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class HealthModuleConnector : BehaviourModuleConnector
    {
        [SerializeField] private AbstractEntity m_AbstractEntity;

        [SelfInject, SerializeReference, ReadOnly] private DestroyGameObjectModule m_DestroyGameObjectModule;
        [SelfInject, SerializeReference, ReadOnly] private HealthModule m_HealthModule;
        [Inject, SerializeReference, ReadOnly] private EntityContainerModule m_EntityContainerModule;
        [SelfInject, SerializeReference, ReadOnly] private StatesModule m_StatesModule;
        
        protected override void Initialize()
        {
            m_HealthModule.ValueChanged += HealthModuleOnValueChanged;
        }

        private void HealthModuleOnValueChanged(int value)
        {
            if (value <= 0 && m_StatesModule.CurrentState.GetType() != typeof(DeathState))
            {
                Debug.Log($"Dead: {m_AbstractEntity.name}");
                m_StatesModule.SetState<DeathState>();
                m_EntityContainerModule.RemoveElement(m_AbstractEntity);
                m_DestroyGameObjectModule.Destroy();
            }
        }
    }
}