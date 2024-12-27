using System;
using _Project.Scripts.Camera;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcFractionModule : AbstractBehaviourModule
    {
        [SerializeField] private NpcFraction m_NpcFraction;

        public NpcFraction NpcFraction => m_NpcFraction;

        public bool IsEnemyFraction(NpcFraction npcFraction)
        {
            return npcFraction != m_NpcFraction;
        }
    }

    [Serializable]
    public class NpcALifeModule : AbstractBehaviourModule
    {
        public event Action<NpcEntity, NpcALifeState> ALifeStateChanged = delegate { };

        [SerializeField, ReadOnly] private NpcALifeState m_NpcALifeState;

        public NpcALifeState NpcALifeState
        {
            get => m_NpcALifeState;
            set
            {
                if (m_NpcALifeState != value)
                {
                    m_NpcALifeState = value;
                    OnALifeStateChanged();
                    Debug.Log($"Npc {m_AbstractEntity.name} is now {m_NpcALifeState.ToString()}");
                }
            }
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_NpcALifeState = NpcALifeState.Online;
        }

        private void OnALifeStateChanged()
        {
            ALifeStateChanged(m_AbstractEntity as NpcEntity, m_NpcALifeState);
        }
    }
}