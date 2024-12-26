using System;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAiVisionModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity> NoticedEntity = delegate { };
    }
}