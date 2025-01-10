using UnityEngine;

namespace _Project.Scripts
{
    public class GuiInventoryItemEntity : GuiDefaultEntity
    {
        [SerializeField] private Vector2Int m_SizeInGrid;

        public Vector2Int SizeInGrid => m_SizeInGrid;
    }
}