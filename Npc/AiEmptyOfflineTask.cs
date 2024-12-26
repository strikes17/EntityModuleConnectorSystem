using System;

namespace _Project.Scripts
{
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
}