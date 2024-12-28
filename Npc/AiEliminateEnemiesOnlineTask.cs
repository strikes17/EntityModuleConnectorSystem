using System;
using System.Collections.Generic;
using _Project.Scripts.Camera;

namespace _Project.Scripts
{
    [Serializable]
    public class AiEliminateEnemiesOnlineTask : AiOnlineTask
    {
        public override event Action<AiTask> TaskCompleted = delegate(AiTask task) { };

        public override event Action<AiTask> TaskFailed = delegate(AiTask task) { };

        private List<NpcEntity> m_EnemiesNpcEntities = new();

        public AiEliminateEnemiesOnlineTask()
        {
        }

        public override void StartResolve()
        {
        }

        public override void StopResolve()
        {
        }
    }
}