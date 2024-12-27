using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class CameraEntityInteractableConnector : BehaviourModuleConnector
    {
        [Inject] protected CameraInteractModule m_CameraInteractModule;
        [SelfInject] protected EntityInteractModule m_EntityInteractModule;

        protected override void Initialize()
        {
            m_CameraInteractModule.HitEntity += CameraInteractModuleOnHitEntity;
        }

        private void CameraInteractModuleOnHitEntity(AbstractEntity abstractEntity, RaycastHit hit)
        {
            m_EntityInteractModule.StartInteract(m_CameraInteractModule.User);
        }
    }

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

    /// <summary>
    /// Add this to npc entity to allow him to interact with any entity in world
    /// </summary>
    [Serializable]
    public class NpcInteractModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, NpcEntity> InteractedWithEntity = delegate { };

        public void StartInteractWithEntity(AbstractEntity entityToInteract, NpcEntity user)
        {
            InteractedWithEntity(entityToInteract, user);
        }
    }

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