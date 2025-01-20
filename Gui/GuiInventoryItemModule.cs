using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiInventoryItemModule : GuiAbstractBehaviourModule
    {
        public event Action<AbstractUsableItem> MiniatureBeganDrag = delegate { };
        public event Action<AbstractUsableItem> MiniatureEndDrag = delegate { };

        [SerializeField] private Vector2Int m_SizeInGrid;

        public Vector2Int SizeInGrid => m_SizeInGrid;

        public AbstractUsableItem Item;

        private List<GuiInventoryGridFillModule> m_GridFillModules;
        private List<InventoryGridValidationModule> m_GridValidationModules;

        public void SetAllowedGrids(List<GuiInventoryGridFillModule> gridFillModules,
            List<InventoryGridValidationModule> gridValidationModules)
        {
            m_GridFillModules = gridFillModules;
            m_GridValidationModules = gridValidationModules;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            MiniatureBeganDrag(Item);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            var position = eventData.position;
            m_GuiDefaultEntity.RectTransform.position = position;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            Vector2 rectTransformPosition = m_GuiDefaultEntity.RectTransform.position;
            float cellSize = 100f;
            for (var gridIndex = 0; gridIndex < m_GridFillModules.Count; gridIndex++)
            {
                var gridFillModule = m_GridFillModules[gridIndex];
                var gridValidationModule = m_GridValidationModules[gridIndex];

                Vector2 pivotPosition = gridFillModule.PivotPosition;

                Vector2 difference = (pivotPosition - rectTransformPosition);
                difference.x /= cellSize;
                difference.y /= cellSize;

                int cellX = Mathf.Abs(Mathf.FloorToInt(difference.x));
                int cellY = Mathf.Abs(Mathf.FloorToInt(difference.y));

                Dictionary<Vector2Int, AbstractUsableItem> collidedCells =
                    gridValidationModule.IsItemCanFitOrSwap(m_SizeInGrid, new Vector2Int(cellX, cellY));
                if (collidedCells.Count == 0)
                {
                    Debug.Log($"Ok");
                }
                else
                {
                }
            }
        }
    }
}