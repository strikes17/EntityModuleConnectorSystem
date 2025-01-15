﻿using System;
using _Project.Scripts.Camera;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAiLogicTaskDispenserConnector : BehaviourModuleConnector
    {
        [SelfInject] private NpcAiVisionModule m_VisionModule;
        [SelfInject] private NpcAiLogicModule m_LogicModule;
        [SelfInject] private AiTaskResolverModule m_TaskResolverModule;
        [SelfInject] private AiNavMeshModule m_AiNavMeshModule;
        [SelfInject] private NpcALifeModule m_NpcALifeModule;
        [SelfInject] private NpcFractionModule m_FractionModule;
        [SelfInject] private SkinMeshAnimationModule m_SkinMeshAnimationModule;

        protected override void Initialize()
        {
            m_VisionModule.NoticedEntity += VisionModuleOnNoticedEntity;
            m_VisionModule.EntityIsLost += VisionModuleOnEntityIsLost;
        }

        private void VisionModuleOnEntityIsLost(AbstractEntity entity)
        {
            if (entity.GetType() == typeof(NpcEntity))
            {
                var task = m_TaskResolverModule.GetTaskOfType<AiEliminateEnemiesOnlineTask>();
                if (task != null)
                {
                    // task.RemoveTarget(entity as NpcEntity);
                }
            }
        }

        private void VisionModuleOnNoticedEntity(AbstractEntity entity)
        {
            var entityInteractModule = entity.GetBehaviorModuleByType<EntityInteractModule>();
            if (entityInteractModule != null)
            {
                if (entityInteractModule.IsInteractable &&
                    entityInteractModule.PointOfInterestValue != NpcPointOfInterestValue.None)
                {
                    int rnd = Random.Range(0 + 5 * (int)entityInteractModule.PointOfInterestValue, 100);
                    if (rnd >= 50)
                    {
                        if (entityInteractModule.PointOfInterestValue >= m_LogicModule.MinimumPointOfInterestValue)
                        {
                            SetInteractEntityTask(m_AbstractEntity, AiTaskPriority.Important, entity);
                        }
                    }
                }
            }
            else
            {
                if (entity is NpcEntity)
                {
                    Debug.Log($"Noticed npc: {entity.name}");
                    var npcFractionModule = entity.GetBehaviorModuleByType<NpcFractionModule>();
                    if (m_FractionModule.IsEnemyFraction(npcFractionModule.NpcFraction))
                    {
                        SetEliminateEntityTask(m_AbstractEntity, AiTaskPriority.MostImportant, entity);
                    }
                }
            }
        }

        private void SetEliminateEntityTask(AbstractEntity entity, AiTaskPriority aiTaskPriority,
            AbstractEntity targetEntity)
        {
            var task = m_TaskResolverModule.GetTaskOfType<AiEliminateEnemiesOnlineTask>();
            if (task != null)
            {
                task.AddTarget(targetEntity as NpcEntity);
            }
            else
            {
                AiEliminateEnemiesOnlineTask eliminateEnemiesOnlineTask =
                    new AiEliminateEnemiesOnlineTask(entity as NpcEntity, m_AiNavMeshModule, m_LogicModule,
                        m_VisionModule, m_SkinMeshAnimationModule);
                AiSetDestinationOfflineTask checkEntityOfflineTask =
                    new AiSetDestinationOfflineTask(m_AiNavMeshModule, targetEntity.transform.position);

                eliminateEnemiesOnlineTask.AddTarget(targetEntity);

                AiTaskResolver aiTaskResolver =
                    new AiTaskResolver(checkEntityOfflineTask, eliminateEnemiesOnlineTask, aiTaskPriority);
                Debug.Log($"added task {aiTaskResolver.OnlineTaskType}, {m_NpcALifeModule.NpcALifeState.ToString()}");
                m_TaskResolverModule.AddTask(aiTaskResolver, m_NpcALifeModule.NpcALifeState);
            }
        }

        private void SetInteractEntityTask(AbstractEntity entity, AiTaskPriority aiTaskPriority,
            AbstractEntity targetEntity)
        {
            if (m_AiNavMeshModule.IsPathValid(targetEntity.transform.position))
            {
                AiInteractWithEntityOnlineTask checkEntityOnlineTask =
                    new AiInteractWithEntityOnlineTask(m_AiNavMeshModule, m_LogicModule, targetEntity, m_SkinMeshAnimationModule);
                AiSetDestinationOfflineTask checkEntityOfflineTask =
                    new AiSetDestinationOfflineTask(m_AiNavMeshModule, targetEntity.transform.position);
                AiTaskResolver aiTaskResolver =
                    new AiTaskResolver(checkEntityOfflineTask, checkEntityOnlineTask, aiTaskPriority);
                Debug.Log($"added task {aiTaskResolver.OnlineTaskType}, {m_NpcALifeModule.NpcALifeState.ToString()}");
                m_TaskResolverModule.AddTask(aiTaskResolver, m_NpcALifeModule.NpcALifeState);
            }
        }
    }
}