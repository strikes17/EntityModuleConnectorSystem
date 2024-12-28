using System;
using _Project.Scripts.Camera;

namespace _Project.Scripts
{
    /// <summary>
    /// Add this to weapon,ammo,item etc. to add npc interaction with it
    /// </summary>
    [Serializable]
    public class NpcEntityInteractableConnector : BehaviourModuleConnector
    {
        [Inject] private NpcAiContainer m_NpcAiContainer;
        [SelfInject] private EntityInteractModule m_EntityInteractModule;

        protected override void Initialize()
        {
            foreach (var abstractEntity in m_NpcAiContainer.ContainerCollection)
            {
                NpcAiContainerOnElementAdded(abstractEntity);
            }

            m_NpcAiContainer.ElementAdded += NpcAiContainerOnElementAdded;
        }

        private void NpcAiContainerOnElementAdded(AbstractEntity obj)
        {
            var npcInteractModule = obj.GetBehaviorModuleByType<NpcInteractModule>();
            if (npcInteractModule != null)
            {
                npcInteractModule.InteractedWithEntity += NpcInteractModuleOnInteractedWithEntity;
            }
        }

        private void NpcInteractModuleOnInteractedWithEntity(AbstractEntity interactedEntity, NpcEntity npcEntity)
        {
            if (interactedEntity == m_AbstractEntity)
            {
                m_EntityInteractModule.StartInteract(npcEntity);
            }
        }
    }
}