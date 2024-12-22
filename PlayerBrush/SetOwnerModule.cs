using UnityEngine;

namespace _Project.Scripts
{
    public class SetOwnerModule : AbstractBehaviourModule
    {
        [SerializeField] private int m_OwnerId;
        
        public void SetOwnerForEntity(AbstractEntity entity)
        {
            var ownerModule = entity.GetValueModuleByType<OwnerModule>();
            if (ownerModule != null)
            {
                ownerModule.Value = m_OwnerId;
            }
        }
    }
}