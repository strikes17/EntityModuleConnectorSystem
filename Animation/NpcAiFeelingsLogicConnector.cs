using System;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAiFeelingsLogicConnector : BehaviourModuleConnector
    {
        [SelfInject] private NpcAiVisionModule m_VisionModule;
        [SelfInject] private NpcAiLogicModule m_LogicModule;
        
        protected override void Initialize()
        {
            m_VisionModule.NoticedEntity += VisionModuleOnNoticedEntity;
        }

        private void VisionModuleOnNoticedEntity(AbstractEntity entity)
        {
            m_LogicModule.CheckEntity(entity);
        }
    }
}