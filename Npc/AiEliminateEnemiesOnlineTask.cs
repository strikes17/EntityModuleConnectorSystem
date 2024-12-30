using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Camera;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class AiEliminateEnemiesOnlineTask : AiOnlineTask
    {
        public override event Action<AiTask> TaskCompleted = delegate(AiTask task) { };

        public override event Action<AiTask> TaskFailed = delegate(AiTask task) { };

        [SerializeField, ReadOnly] private List<NpcEntity> m_EliminationTargets = new();

        [SerializeField, ReadOnly] private NpcEntity m_ClosestEliminationTarget;

        private NpcAiLogicModule m_NpcAiLogicModule;
        private AiNavMeshModule m_AiNavMeshModule;
        private NpcAiVisionModule m_NpcAiVisionModule;

        [SerializeField, ReadOnly] private NpcEntity m_SelfEntity;

        [SerializeField, ReadOnly] private List<AbstractEntity> m_CurrentlySeeingEntities;

        public void AddTarget(NpcEntity npcEntity)
        {
            if (!m_EliminationTargets.Contains(npcEntity))
            {
                m_EliminationTargets.Add(npcEntity);
            }
        }

        public void RemoveTarget(NpcEntity npcEntity)
        {
            if (m_EliminationTargets.Contains(npcEntity))
            {
                m_EliminationTargets.Remove(npcEntity);
            }
        }

        public AiEliminateEnemiesOnlineTask(NpcEntity selfEntity, AiNavMeshModule navMeshModule,
            NpcAiLogicModule logicModule, NpcAiVisionModule npcAiVisionModule)
        {
            m_NpcAiLogicModule = logicModule;
            m_AiNavMeshModule = navMeshModule;
            m_NpcAiVisionModule = npcAiVisionModule;
            m_SelfEntity = selfEntity;
        }
        
        private IEnumerator LookForBestTarget()
        {
            while (m_EliminationTargets.Count > 0)
            {
                // Debug.Log($"m_EliminationTargets: {m_EliminationTargets.Count}");
                if (m_CurrentlySeeingEntities.Count > 0 && m_EliminationTargets.Count > 0)
                {
                    // Debug.Log($"m_CurrentlySeeingEntities: {m_CurrentlySeeingEntities.Count}");
                    var targets = m_CurrentlySeeingEntities.Where(x => m_EliminationTargets.Contains(x))
                        .OrderBy(x => Vector3.Distance(m_SelfEntity.transform.position, x.transform.position));
                    AbstractEntity closestVisibleTarget = targets.Any() ? targets.First() : null;
                    Debug.Log($"closestVisibleTarget: {closestVisibleTarget}");
                    if (closestVisibleTarget != null && m_ClosestEliminationTarget != closestVisibleTarget)
                    {
                        m_ClosestEliminationTarget = closestVisibleTarget as NpcEntity;
                        m_NpcAiLogicModule.TryIntimidateNpc(m_ClosestEliminationTarget);
                        Debug.Log($"Intimidating at {m_ClosestEliminationTarget.name}");
                    }
                }

                if (m_ClosestEliminationTarget != null)
                {
                    m_AiNavMeshModule.StartFollowPathToEntity(m_ClosestEliminationTarget);
                }

                yield return new WaitForSeconds(1f);
            }

            TaskCompleted(this);
        }

        public override void StartResolve()
        {
            m_CurrentlySeeingEntities = m_NpcAiVisionModule.CurrentlySeeingEntities;
            Moroutine.Run(LookForBestTarget());
        }

        public override void StopResolve()
        {
        }
    }
}