using System;
using System.Collections;
using System.Linq;
using _Project.Scripts.Camera;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    [Serializable]
    public class AiEliminateEnemiesOnlineTask : AiOnlineTask
    {
        public override event Action<AiTask> TaskCompleted = delegate(AiTask task) { };

        public override event Action<AiTask> TaskFailed = delegate(AiTask task) { };

        [SerializeField, ReadOnly] private SerializableHashSet<AbstractEntity> m_EliminationTargets = new();

        [SerializeField, ReadOnly] private AbstractEntity m_ClosestEliminationTarget;

        private NpcAiLogicModule m_NpcAiLogicModule;
        private AiNavMeshModule m_AiNavMeshModule;
        private NpcAiVisionModule m_NpcAiVisionModule;
        private SkinMeshAnimationModule m_SkinMeshAnimationModule;

        private BonesModule m_EliminationTargetBonesModule;

        [SerializeField, ReadOnly] private AbstractEntity m_SelfEntity;

        [SerializeField, ReadOnly] private SerializableHashSet<AbstractEntity> m_CurrentlySeeingEntities;

        private AiEliminateTaskNpcStateMachine m_NpcStateMachine;

        private Vector3 m_LatestTargetPosition;

        public void AddTarget(AbstractEntity npcEntity)
        {
            m_CurrentlySeeingEntities.Add(npcEntity);
            m_EliminationTargets.Add(npcEntity);
        }

        public void RemoveTarget(AbstractEntity npcEntity)
        {
            m_CurrentlySeeingEntities.Remove(npcEntity);
            m_EliminationTargets.Remove(npcEntity);
        }

        public AiEliminateEnemiesOnlineTask(NpcEntity selfEntity, AiNavMeshModule navMeshModule,
            NpcAiLogicModule logicModule, NpcAiVisionModule npcAiVisionModule,
            SkinMeshAnimationModule skinMeshAnimationModule)
        {
            m_NpcAiLogicModule = logicModule;
            m_AiNavMeshModule = navMeshModule;
            m_NpcAiVisionModule = npcAiVisionModule;
            m_SelfEntity = selfEntity;
            m_CurrentlySeeingEntities = new();
            m_EliminationTargets = new();
            m_SkinMeshAnimationModule = skinMeshAnimationModule;
            m_NpcStateMachine = new();
            m_NpcStateMachine.SetState<AiEliminateNoneState>();
            m_NpcStateMachine.StateChanged += NpcStateMachineOnStateChanged;
            m_NpcAiVisionModule.NoticedEntity += NpcAiVisionModuleOnNoticedEntity;
            m_NpcAiVisionModule.EntityIsLost += NpcAiVisionModuleOnEntityIsLost;
        }

        private IEnumerator LookForBestTarget()
        {
            while (m_EliminationTargets.Count > 0)
            {
                // Debug.Log($"m_EliminationTargets: {m_EliminationTargets.Count}");
                if (m_CurrentlySeeingEntities.Count > 0)
                {
                    // Debug.Log($"m_CurrentlySeeingEntities: {m_CurrentlySeeingEntities.Count}");
                    var targets = m_CurrentlySeeingEntities.Set.Where(x => m_EliminationTargets.Contains(x))
                        .OrderBy(x => Vector3.Distance(m_SelfEntity.transform.position, x.transform.position));
                    AbstractEntity closestVisibleTarget = targets.Any() ? targets.First() : null;
                    // Debug.Log($"closestVisibleTarget: {closestVisibleTarget}");
                    if (closestVisibleTarget != null && m_ClosestEliminationTarget != closestVisibleTarget)
                    {
                        m_NpcStateMachine.SetState<AiEliminateNoneState>();
                        m_ClosestEliminationTarget = closestVisibleTarget;
                        m_EliminationTargetBonesModule =
                            m_ClosestEliminationTarget.GetBehaviorModuleByType<BonesModule>();
                        m_NpcAiLogicModule.TryIntimidateNpc(m_ClosestEliminationTarget as NpcEntity);
                    }
                }

                if (m_ClosestEliminationTarget != null)
                {
                    NavMeshPathStatus pathStatus =
                        m_AiNavMeshModule.GetPathStatus(m_ClosestEliminationTarget.transform.position);
                    if (pathStatus == NavMeshPathStatus.PathComplete)
                    {
                        m_NpcStateMachine.SetState<AiEliminatePathCompleteState>();
                    }
                    else if (pathStatus == NavMeshPathStatus.PathPartial)
                    {
                        m_NpcStateMachine.SetState<AiEliminatePathPartialState>();
                    }
                    else if (pathStatus == NavMeshPathStatus.PathInvalid)
                    {
                        m_NpcStateMachine.SetState<AiEliminatePathInvalidState>();
                    }
                }

                yield return new WaitForSeconds(0.25f);
            }

            TaskCompleted(this);
        }

        private void NpcStateMachineOnStateChanged(AiEliminateTaskNpcState taskNpcState)
        {
            if (taskNpcState is AiEliminatePathCompleteState)
            {
                UpdateLogicAtPathIsValid();
            }
            else if (taskNpcState is AiEliminatePathPartialState)
            {
                UpdateLogicAtPathIsPartial();
            }
            else if (taskNpcState is AiEliminatePathInvalidState)
            {
                UpdateLogicAtPathIsInvalid();
            }
        }

        private void SetTargetingScopeAtEnemy()
        {
            if (m_EliminationTargetBonesModule != null)
            {
                m_SkinMeshAnimationModule.RotationTarget =
                    m_EliminationTargetBonesModule.GetBone<HeadBone>().Transform;
            }
            else
            {
                m_SkinMeshAnimationModule.RotationTarget = m_ClosestEliminationTarget.transform;
            }
        }

        private void UpdateLogicAtPathIsValid()
        {
            m_AiNavMeshModule.StartFollowPathToEntity(m_ClosestEliminationTarget, 2f);
            SetTargetingScopeAtEnemy();
        }

        private void UpdateLogicAtPathIsPartial()
        {
            m_AiNavMeshModule.Stop();
            SetTargetingScopeAtEnemy();
        }

        private void UpdateLogicAtPathIsInvalid()
        {
            var point = new Vector3(-118.75f, 0.06f, 46.27f);
            m_AiNavMeshModule.StartFollowPathToPoint(point);
            SetTargetingScopeAtEnemy();
        }

        public override void StartResolve()
        {
            Moroutine.Run(LookForBestTarget());
        }

        private void NpcAiVisionModuleOnEntityIsLost(AbstractEntity obj)
        {
            m_CurrentlySeeingEntities.Remove(obj);
        }

        private void NpcAiVisionModuleOnNoticedEntity(AbstractEntity obj)
        {
            m_CurrentlySeeingEntities.Add(obj);
        }

        public override void StopResolve()
        {
        }
    }
}