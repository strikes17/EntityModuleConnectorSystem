using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Camera;
using Redcode.Moroutines;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcGlobalTaskDispenserConnector : BehaviourModuleConnector
    {
        [SelfInject] private AiNavMeshModule m_AiNavMeshModule;
        [SelfInject] private AiTaskResolverModule m_TaskResolverModule;
        [SelfInject] private NpcAiLogicModule m_NpcAiLogicModule;
        [Inject] private PointOfInterestContainer m_PointOfInterestContainer;
        [SelfInject] private NpcALifeModule m_NpcALifeModule;

        protected override void Initialize()
        {
            m_NpcALifeModule.ALifeStateChanged += NpcALifeModuleOnALifeStateChanged;
            Moroutine.Run(Test());
        }

        private void NpcALifeModuleOnALifeStateChanged(NpcEntity npcEntity, NpcALifeState npcALifeState)
        {
            m_TaskResolverModule.SetALifeTasksState(npcALifeState);
        }

        private IEnumerator Test()
        {
            yield return new WaitForSeconds(1f);
            while (true)
            {
                if (!m_TaskResolverModule.HasAnyTask)
                {
                    Debug.Log($"Polling ai action for npc {m_AbstractEntity.name}");
            
                    if (!m_TaskResolverModule.HasTaskOfType<AiSetDestinationOnlineTask>())
                    {
                        List<NpcPointOfInterestModule> pointOfInterestModules = m_PointOfInterestContainer
                            .ContainerCollection.Select(x =>
                                x.GetBehaviorModuleByType<NpcPointOfInterestModule>()).ToList();
                        int rnd = Random.Range(0, pointOfInterestModules.Count);
                        var targetPoint = pointOfInterestModules[rnd].Position;
                        if (m_AiNavMeshModule.IsPathValid(targetPoint))
                        {
                            CreateWalkToDestinationTask(targetPoint);
                        }
                    }
                }
            
                yield return new WaitForSeconds(2f);
            }
        }

        private void CreateWalkToDestinationTask(Vector3 targetPoint)
        {
            AiSetDestinationOfflineTask setDestinationOfflineTask = new AiSetDestinationOfflineTask(m_AiNavMeshModule, targetPoint);
            AiSetDestinationOnlineTask setDestinationOnlineTask =
                new AiSetDestinationOnlineTask(m_AiNavMeshModule, targetPoint);
            AiTaskResolver setDestinationTaskResolver = new AiTaskResolver(setDestinationOfflineTask,
                setDestinationOnlineTask, AiTaskPriority.Default);

            Debug.Log(
                $"Npc {m_AbstractEntity.name} decided to go with alife: {m_NpcALifeModule.NpcALifeState.ToString()}");
            m_TaskResolverModule.AddTask(setDestinationTaskResolver, m_NpcALifeModule.NpcALifeState);
        }
    }
}