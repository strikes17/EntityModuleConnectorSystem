using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class AttackSingleTargetSelectorModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity> TargetSelected = delegate { };
        public event Action<AbstractEntity> TargetLost = delegate { };

        [SerializeField, ReadOnly] private AbstractEntity m_SelectedEntity;
        private List<AbstractEntity> m_PossibleTargets = new();

        private readonly float m_TimeForUpdate = 0.1f;

        private float m_UpdateTime;
        private float m_AttackRange;

        public void SetAttackRange(float range)
        {
            m_AttackRange = range * range;
        }

        public void SetPossibleTargets(List<AbstractEntity> possibleTargets)
        {
            m_PossibleTargets = possibleTargets;
            OrderPossibleTargetsByDistance();
        }

        public void AddPossibleTarget(AbstractEntity abstractEntity)
        {
            m_PossibleTargets.Add(abstractEntity);
            OrderPossibleTargetsByDistance();
        }

        public void RemovePossibleTarget(AbstractEntity abstractEntity)
        {
            m_PossibleTargets.Remove(abstractEntity);
        }

        public override void OnUpdate()
        {
            m_UpdateTime += Time.deltaTime;
            if (m_UpdateTime >= m_TimeForUpdate)
            {
                m_UpdateTime = 0f;
                TrySelectTargetForAttack();
            }
        }

        public void TrySelectTargetForAttack()
        {
            var selectedEntity = m_PossibleTargets.FirstOrDefault(x =>
            {
                var distance = (x.transform.position - m_AbstractEntity.transform.position).sqrMagnitude;
                return distance < m_AttackRange;
            });

            if (selectedEntity == m_SelectedEntity)
            {
                return;
            }

            if (selectedEntity != null)
            {
                m_SelectedEntity = selectedEntity;
                TargetSelected(selectedEntity);
            }
            else
            {
                if (selectedEntity == null && m_SelectedEntity != null)
                {
                    OrderPossibleTargetsByDistance();
                    TargetLost(m_SelectedEntity);
                }
            }
        }

        private void OrderPossibleTargetsByDistance()
        {
            m_PossibleTargets = m_PossibleTargets.OrderBy(x =>
            {
                var distance = (x.transform.position - m_AbstractEntity.transform.position).sqrMagnitude;
                if (distance < m_AttackRange)
                    return distance;
                return float.MaxValue;
            }).ToList();
        }
    }
}