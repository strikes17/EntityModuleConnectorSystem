using System;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AiOfflineTask : AiTask
    {
        protected AiOnlineTask m_OnlineTask;

        public virtual void SetOnlineTaskInfo(AiOnlineTask aiOnlineTask)
        {
            m_OnlineTask = aiOnlineTask;
        }
    }
}