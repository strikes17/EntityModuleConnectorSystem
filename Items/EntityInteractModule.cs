using System;

namespace _Project.Scripts
{
    [Serializable]
    public class EntityInteractModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, AbstractEntity> InteractStarted = delegate { };

        public void StartInteract(AbstractEntity entityUser)
        {
            InteractStarted(m_AbstractEntity, entityUser);
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