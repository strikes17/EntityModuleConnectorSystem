using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class EntityInteractModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, AbstractEntity> InteractStarted = delegate { };
        
        [SerializeField] private NpcPointOfInterestValue m_PointOfInterestValue;

        public bool IsInteractable => m_IsInteractable;

        private bool m_IsInteractable;
        
        public NpcPointOfInterestValue PointOfInterestValue => m_PointOfInterestValue;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_IsInteractable = true;
        }

        public void StartInteract(AbstractEntity entityUser)
        {
            InteractStarted(m_AbstractEntity, entityUser);
            m_IsInteractable = false;
        }
    }

    [Serializable]
    public abstract class AbstractUsableItem
    {
    }

    public class WeaponItem : AbstractUsableItem
    {
        
    }
    
    public class AmmoItem : AbstractUsableItem
    {
        
    }
}