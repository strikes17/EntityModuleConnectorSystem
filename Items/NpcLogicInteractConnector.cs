using System;
using _Project.Scripts.Camera;

namespace _Project.Scripts
{
    /// <summary>
    /// Add this to npc entity, it makes interact module to react to npc decision of interacting with any entity
    /// </summary>
    [Serializable]
    public class NpcLogicInteractConnector : BehaviourModuleConnector
    {
        [SelfInject] private NpcInteractModule m_InteractModule;
        [SelfInject] private NpcAiLogicModule m_NpcAiLogicModule;

        protected override void Initialize()
        {
            m_NpcAiLogicModule.DecidedToInteractWithEntity += NpcAiLogicModuleOnDecidedToInteractWithEntity;
        }

        private void NpcAiLogicModuleOnDecidedToInteractWithEntity(AbstractEntity entity, NpcEntity user)
        {
            m_InteractModule.StartInteractWithEntity(entity, user);
        }
    }
}