using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class AiTaskResolverModule : AbstractBehaviourModule
    {
        public event Action<AiTaskResolver> ActiveTaskChanged = delegate { };

        [SerializeField] private List<AiTaskResolver> m_TaskResolvers = new();
        [SerializeField] private AiTaskResolver m_ActiveTaskResolver;

        private AiTaskResolver m_EmptyTaskResolver;
        
        public bool HasAnyTask => m_TaskResolvers.Count > 1;
        
        public bool HasTaskOfType<T>() where T : AiTask
        {
            foreach (var aiTaskResolver in m_TaskResolvers)
            {
                if (aiTaskResolver.OfflineTaskType == typeof(T) || aiTaskResolver.OnlineTaskType == typeof(T))
                {
                    return true;
                }
            }

            return false;
        }
        
        public T GetTaskOfType<T>() where T : AiTask
        {
            foreach (var aiTaskResolver in m_TaskResolvers)
            {
                if (aiTaskResolver.OfflineTaskType == typeof(T) || aiTaskResolver.OnlineTaskType == typeof(T))
                {
                    return aiTaskResolver.OnlineTask as T;
                }
            }

            return null;
        }

        private AiTaskResolver ActiveTaskResolver
        {
            set
            {
                if (value == null)
                {
                    m_ActiveTaskResolver = m_EmptyTaskResolver;
                    ActiveTaskChanged(m_ActiveTaskResolver);
                }
                else if (m_ActiveTaskResolver != value)
                {
                    m_ActiveTaskResolver = value;
                    ActiveTaskChanged(m_ActiveTaskResolver);
                }
            }
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_EmptyTaskResolver =
                new AiTaskResolver(new AiEmptyOfflineTask(), new AiEmptyOnlineTask(), AiTaskPriority.NoTask);
            m_TaskResolvers.Add(m_EmptyTaskResolver);
            AllowPostInitialization(2);
        }

        protected override void PostInitialize()
        {
            base.PostInitialize();
            ActiveTaskResolver = m_EmptyTaskResolver;
        }

        public void AddTask(AiTaskResolver aiTaskResolver, NpcALifeState aLifeState)
        {
            int maxPriority = (int)m_TaskResolvers.Max(x => x.Priority);

            m_TaskResolvers.Add(aiTaskResolver);
            aiTaskResolver.TaskResolved += AiTaskResolverOnTaskResolved;
            aiTaskResolver.TaskFailed += AiTaskResolverOnTaskFailed;
            aiTaskResolver.ALifeState = aLifeState;
            
            if ((int)aiTaskResolver.Priority > maxPriority)
            {
                if (m_ActiveTaskResolver != null)
                {
                    m_ActiveTaskResolver.StopResolveTask();
                }
                Debug.Log($"AiTask {aiTaskResolver.OnlineTaskType} priority is higher than {maxPriority}, setting" +
                          $"{aLifeState.ToString()}");
                ActiveTaskResolver = aiTaskResolver;
                m_ActiveTaskResolver.StartResolveTask();
            }
        }

        private void AiTaskResolverOnTaskFailed(AiTaskResolver resolver, AiTask aiTask)
        {
            resolver.StopResolveTask();
            
            m_TaskResolvers.Remove(resolver);
            AiTaskResolver nextResolver = m_TaskResolvers.Aggregate((x, y) => x.Priority > y.Priority ? x : y);
            ActiveTaskResolver = nextResolver;

            m_ActiveTaskResolver.StartResolveTask();
        }

        /// <summary>
        /// Method sets ALife state of NPC to all his tasks at once
        /// </summary>
        /// <param name="aLifeState"></param>
        public void SetALifeTasksState(NpcALifeState aLifeState)
        {
            foreach (var aiTaskResolver in m_TaskResolvers)
            {
                aiTaskResolver.ALifeState = aLifeState;
            }

            m_ActiveTaskResolver.StartResolveTask();
        }

        /// <summary>
        /// Method called when active task is resolved
        /// Switches to the next task with highest priority
        /// </summary>
        /// <param name="resolver"></param>
        /// <param name="aiTask"></param>
        private void AiTaskResolverOnTaskResolved(AiTaskResolver resolver, AiTask aiTask)
        {
            resolver.StopResolveTask();
            
            m_TaskResolvers.Remove(resolver);
            AiTaskResolver nextResolver = m_TaskResolvers.Aggregate((x, y) => x.Priority > y.Priority ? x : y);
            ActiveTaskResolver = nextResolver;

            m_ActiveTaskResolver.StartResolveTask();
        }
    }
}