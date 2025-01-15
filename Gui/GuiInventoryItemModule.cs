using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiInventoryItemModule : GuiAbstractBehaviourModule
    {
        [SerializeField] private Vector2Int m_SizeInGrid;

        public Vector2Int SizeInGrid => m_SizeInGrid;

        public AbstractEntity OwnerEntity;

        public AbstractUsableItem Item;

        private List<>
        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            var gridValidationModule = OwnerEntity.GetBehaviorModuleByType<InventoryGridValidationModule>();
            gridValidationModule
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            var position = eventData.position;
            m_GuiDefaultEntity.transform.position = position;
        }
    }
}