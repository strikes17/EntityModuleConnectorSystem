using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiInventoryItemModule : GuiAbstractBehaviourModule
    {
        public event Action<AbstractUsableItem> MiniatureBeganDrag = delegate{  };
        public event Action<AbstractUsableItem> MiniatureEndDrag = delegate{  };

        [SerializeField] private Vector2Int m_SizeInGrid;

        public Vector2Int SizeInGrid => m_SizeInGrid;

        public AbstractUsableItem Item;

        private List<GuiInventoryGridFillModule> m_GridFillModules;

        public void SetAllowedGrids(List<GuiInventoryGridFillModule> gridFillModules)
        {
            m_GridFillModules = gridFillModules;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            MiniatureBeganDrag(Item);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            MiniatureEndDrag(Item);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            var position = eventData.position;
            Vector2 rectTransformPosition = m_GuiDefaultEntity.RectTransform.position;
            m_GuiDefaultEntity.RectTransform.position = position;
            float cellSize = 100f;
            foreach (var gridFillModule in m_GridFillModules)
            {
                Vector2 pivotPosition = gridFillModule.PivotPosition;

                int cellX = Mathf.FloorToInt(pivotPosition.x - rectTransformPosition.x);
                int cellY = Mathf.FloorToInt(pivotPosition.y - rectTransformPosition.y);

                for (int y = 0; y < m_SizeInGrid.y; y++)
                {
                    int targetCellY = cellY + y;
                    for (int x = 0; x < m_SizeInGrid.x; x++)
                    {
                        int targetCellX = cellX + x;
                        gridFillModule.
                    }
                }

            }
        }
    }
}