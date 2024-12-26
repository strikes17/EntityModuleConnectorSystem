using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Camera;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcTaskDispenserConnector : BehaviourModuleConnector
    {
        [SelfInject] private AiNavMeshModule m_AiNavMeshModule;
        [SelfInject] private AiTaskResolverModule m_TaskResolverModule;
        [SelfInject] private NpcAiLogicModule m_NpcAiLogicModule;
        [Inject] private PointOfInterestContainer m_PointOfInterestContainer;

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

        protected override void Initialize()
        {
            m_NpcALifeState = NpcALifeState.Online;
            Moroutine.Run(Test());
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

        private void OnALifeStateChanged()
        {
            m_TaskResolverModule.SetALifeTasksState(m_NpcALifeState);
            ALifeStateChanged(m_AbstractEntity as NpcEntity, m_NpcALifeState);
        }

        private void CreateWalkToDestinationTask(Vector3 targetPoint)
        {
            AiSetDestinationOfflineTask setDestinationOfflineTask = new AiSetDestinationOfflineTask(m_AiNavMeshModule, targetPoint);
            AiSetDestinationOnlineTask setDestinationOnlineTask =
                new AiSetDestinationOnlineTask(m_AiNavMeshModule, targetPoint);
            AiTaskResolver setDestinationTaskResolver = new AiTaskResolver(setDestinationOfflineTask,
                setDestinationOnlineTask, AiTaskPriority.Default);

            Debug.Log(
                $"Npc {m_AbstractEntity.name} decided to go with alife: {NpcALifeState.ToString()}");
            m_TaskResolverModule.AddTask(setDestinationTaskResolver, NpcALifeState);
        }
    }
}