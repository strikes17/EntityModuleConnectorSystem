using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class AiTaskResolver
    {
        public NpcALifeState ALifeState;

        public event Action<AiTaskResolver, AiTask> TaskResolved = delegate { };

        public Type OfflineTaskType => m_OfflineTask.GetType();
        public Type OnlineTaskType => m_OnlineTask.GetType();

        public AiTaskPriority Priority => m_Priority;

        public string Name => m_OnlineTask.GetType().ToString();

        [SerializeField] private AiTaskPriority m_Priority;

        [SerializeField] private AiOfflineTask m_OfflineTask;
        [SerializeField] private AiOnlineTask m_OnlineTask;

        [SerializeField] private bool m_IsTaskResolved;

        public AiTaskResolver(AiOfflineTask aiOfflineTask, AiOnlineTask aiOnlineTask, AiTaskPriority aiTaskPriority)
        {
            m_Priority = aiTaskPriority;
            m_OnlineTask = aiOnlineTask;
            m_OfflineTask = aiOfflineTask;

            m_OnlineTask.SetOfflineTaskInfo(m_OfflineTask);
            m_OfflineTask.SetOnlineTaskInfo(m_OnlineTask);

            m_OnlineTask.TaskCompleted += OnTaskCompleted;
            m_OfflineTask.TaskCompleted += OnTaskCompleted;
        }

        private void OnTaskCompleted(AiTask task)
        {
            if (!m_IsTaskResolved)
            {
                m_IsTaskResolved = true;
                TaskResolved(this, task);
            }
        }

        public void StopResolveTask()
        {
            if (ALifeState == NpcALifeState.Offline)
            {
                m_OfflineTask.StopResolve();
            }
            else if (ALifeState == NpcALifeState.Online)
            {
                m_OnlineTask.StopResolve();
            }
        }

        public void StartResolveTask()
        {
            if (ALifeState == NpcALifeState.Offline)
            {
                m_OnlineTask.StopResolve();
                m_OfflineTask.StartResolve();
            }
            else if (ALifeState == NpcALifeState.Online)
            {
                m_OfflineTask.StopResolve();
                m_OnlineTask.StartResolve();
            }
        }
    }
}