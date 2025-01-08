using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    public abstract class GuiAbstractBehaviourModule : AbstractBehaviourModule
    {
        protected GuiDefaultEntity m_GuiDefaultEntity;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_GuiDefaultEntity = m_AbstractEntity as GuiDefaultEntity;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
        }
        
        public virtual void OnBeginDrag(PointerEventData eventData)
        {
        }
        
        public virtual void OnDrag(PointerEventData eventData)
        {
        }
        
        public virtual void OnEndDrag(PointerEventData eventData)
        {
        }
    }
}