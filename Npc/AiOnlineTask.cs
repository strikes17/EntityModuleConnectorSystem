using System;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AiOnlineTask : AiTask
    {
        protected AiOfflineTask m_AiOfflineTask;

        public virtual void SetOfflineTaskInfo(AiOfflineTask aiOfflineTask)
        {
            m_AiOfflineTask = aiOfflineTask;
        }
    }
}