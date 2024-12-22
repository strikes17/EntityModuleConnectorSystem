using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class PlayerBrushModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity> BrushUsed = delegate(AbstractEntity entity) {  };

        [SerializeField] private List<EntityBrushEditor> m_Brushes;
        
        public bool IsPaintingAvailable => m_PaintingBlockers.Count == 0;

        private Dictionary<int, byte> m_PaintingBlockers = new();

        private EntityBrushEditor m_SelectedBrush;

        public void TryBlockPainting(int blockerHashCode)
        {
            m_PaintingBlockers.TryAdd(blockerHashCode, 0);
        }

        public void TryUnblockPainting(int blockerHashCode)
        {
            m_PaintingBlockers.Remove(blockerHashCode);
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_SelectedBrush = m_Brushes.FirstOrDefault();
        }

        public void TryUseBrush(Vector3 position)
        {
            if (!IsPaintingAvailable) return;

            Vector3Int intPosition = Utility.IntVector3(position);
            var entity = m_SelectedBrush.Create(intPosition);
            BrushUsed(entity);
        }
    }
}