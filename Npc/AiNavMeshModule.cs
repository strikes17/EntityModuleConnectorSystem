using System;
using System.Collections;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts
{
    [Serializable]
    public class AiNavMeshModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity> ReachedDestination = delegate { };
        public event Action<AbstractEntity, Vector3> StartedMovingToDestination = delegate { };

        [SerializeField] private NavMeshAgent m_NavMeshAgent;

        private NavMeshPath m_NavMeshPath;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_NavMeshPath = new();
        }

        public bool IsPathValid(Vector3 target)
        {
            m_NavMeshPath = new NavMeshPath();
            bool hasPath = m_NavMeshAgent.CalculatePath(target, m_NavMeshPath);
            if (!hasPath || m_NavMeshAgent.pathStatus != NavMeshPathStatus.PathComplete)
            {
                Debug.Log($"Path is invalid - {m_NavMeshPath.status}");
                return false;
            }

            if (Vector3.Distance(m_NavMeshAgent.transform.position, target) < 1f)
            {
                Debug.Log($"Already at destination");
                return false;
            }

            return true;
        }

        private Moroutine m_Moroutine;

        public void SetDestination(Vector3 target)
        {
            if (m_Moroutine != null)
            {
                m_Moroutine.Stop();
            }

            m_Moroutine = Moroutine.Run(FollowPath(target));
        }

        public void ResetDestination()
        {
            m_NavMeshAgent.ResetPath();
        }

        private IEnumerator FollowPath(Vector3 target)
        {
            m_NavMeshAgent.SetPath(m_NavMeshPath);
            m_NavMeshAgent.updateRotation = true;

            yield return null;

            StartedMovingToDestination(m_AbstractEntity, target);

            while (m_NavMeshAgent.remainingDistance > 1f)
            {
                yield return null;
            }

            ReachedDestination(m_AbstractEntity);
        }
    }

    [Serializable]
    public abstract class AiTask
    {
        public abstract event Action<AiTask> TaskCompleted;
        public abstract void StartResolve();

        public abstract void StopResolve();

        [SerializeField, ReadOnly] protected bool m_IsTaskStarted;
    }

    [Serializable]
    public abstract class AiOfflineTask : AiTask
    {
    }

    [Serializable]
    public abstract class AiOnlineTask : AiTask
    {
    }

    [Serializable]
    public class AiSetDestinationOnlineTask : AiOnlineTask
    {
        private Vector3 m_TargetDestination;
        private AiNavMeshModule m_AiNavMeshModule;

        public AiSetDestinationOnlineTask(AiNavMeshModule navMeshModule, Vector3 targetDestination)
        {
            m_AiNavMeshModule = navMeshModule;
            m_TargetDestination = targetDestination;
        }

        public override event Action<AiTask> TaskCompleted = delegate { };

        public override void StartResolve()
        {
            if (m_IsTaskStarted) return;
            m_IsTaskStarted = true;
            m_AiNavMeshModule.ReachedDestination += AiNavMeshModuleOnReachedDestination;
            m_AiNavMeshModule.SetDestination(m_TargetDestination);
        }

        public override void StopResolve()
        {
            if (!m_IsTaskStarted) return;
            Debug.Log($"Stopping to {m_TargetDestination}");
            m_IsTaskStarted = false;
            m_AiNavMeshModule.ReachedDestination -= AiNavMeshModuleOnReachedDestination;
            m_AiNavMeshModule.ResetDestination();
        }

        private void AiNavMeshModuleOnReachedDestination(AbstractEntity obj)
        {
            TaskCompleted(this);
        }
    }

    [Serializable]
    public class AiSetDestinationOfflineTask : AiOfflineTask
    {
        public override event Action<AiTask> TaskCompleted;

        public override void StartResolve()
        {
        }

        public override void StopResolve()
        {
        }
    }

    [Serializable]
    public class AiEmptyOnlineTask : AiOnlineTask
    {
        public override event Action<AiTask> TaskCompleted;

        public override void StartResolve()
        {
        }

        public override void StopResolve()
        {
        }
    }

    [Serializable]
    public class AiEmptyOfflineTask : AiOfflineTask
    {
        public override event Action<AiTask> TaskCompleted;

        public override void StartResolve()
        {
        }

        public override void StopResolve()
        {
        }
    }

    public enum AiTaskPriority
    {
        NoTask = 0,
        Default = 1,
        Important = 2,
        MostImportant = 3,
        ExtremelyImportant = 10
    }

    [Serializable]
    public class AiTaskResolver
    {
        public NpcALifeState ALifeState;

        public event Action<AiTaskResolver, AiTask> TaskResolved = delegate { };

        public Type OfflineTaskType => m_OfflineTask.GetType();
        public Type OnlineTaskType => m_OnlineTask.GetType();

        public AiTaskPriority Priority => m_Priority;

        [SerializeField] private AiTaskPriority m_Priority;
        [SerializeField] private AiOfflineTask m_OfflineTask;
        [SerializeField] private AiOnlineTask m_OnlineTask;

        [SerializeField] private bool m_IsTaskResolved;

        public string Name => m_OnlineTask.GetType().ToString();

        public AiTaskResolver(AiOfflineTask aiOfflineTask, AiOnlineTask aiOnlineTask, AiTaskPriority aiTaskPriority)
        {
            m_Priority = aiTaskPriority;
            m_OnlineTask = aiOnlineTask;
            m_OfflineTask = aiOfflineTask;

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