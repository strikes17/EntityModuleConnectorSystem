using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiInventoryGridFillModule : GuiAbstractBehaviourModule
    {
        [SerializeField] private Transform m_ContentRoot;
        [SerializeField] private Transform m_ItemsRoot;
        
        [SerializeField] private GameObject m_CellIndexerPrefab;

        private Dictionary<Vector2Int, Transform> m_GridCellsInstances;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_GridCellsInstances = new();
        }

        public void SetItem(Vector2Int position, AbstractUsableItem item)
        {
            if (m_GridCellsInstances.TryGetValue(position, out Transform gridCellTransform))
            {
                var itemEntity = item.InventoryItemEntity;
                itemEntity.gameObject.SetActive(true);

                var transform = itemEntity.transform;

                transform.SetParent(m_ItemsRoot);
                var rectTransform = transform.GetComponent<RectTransform>();
                var vector2 = gridCellTransform.GetComponent<RectTransform>().anchoredPosition;
                rectTransform.anchoredPosition = vector2;
                rectTransform.localScale = Vector3.one;
            }
        }

        public void AddCell(Vector2Int position)
        {
            var instance = Object.Instantiate(m_CellIndexerPrefab, m_ContentRoot);
            m_GridCellsInstances.Add(position, instance.transform);
        }
    }
}