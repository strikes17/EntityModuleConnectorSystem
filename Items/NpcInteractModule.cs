using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Add this to npc entity to allow him to interact with any entity in world
    /// </summary>
    [Serializable]
    public class NpcInteractModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, NpcEntity> InteractedWithEntity = delegate { };

        public void StartInteractWithEntity(AbstractEntity entityToInteract, NpcEntity user)
        {
            Debug.Log($"Npc {user.name} interacted with {entityToInteract.name}");
            InteractedWithEntity(entityToInteract, user);
        }
    }
}