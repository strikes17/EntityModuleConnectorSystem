using UnityEngine;

namespace _Project.Scripts
{
    public abstract class RaycastTargetBehaviourModule : AbstractBehaviourModule
    {
        [SerializeField] private Collider m_Collider;
        
        public override void Initialize(AbstractEntity abstractEntity)
        {
            m_Collider ??= abstractEntity.GetComponent<Collider>();
            m_AbstractEntity = abstractEntity;
        }

        public abstract void OnStart(RaycastHit raycastHit);

        public abstract void OnHold(RaycastHit raycastHit);

        public abstract void OnEnd(RaycastHit raycastHit);
    }
}