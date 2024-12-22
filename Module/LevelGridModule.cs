using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class LevelGridModule : AbstractBehaviourModule
    {
        public IEnumerable<Vector2Int> BlockedPositions => m_BlockedPositions;

        private List<Vector2Int> m_BlockedPositions = new();

        public void BlockPosition(Vector2Int position)
        {
            m_BlockedPositions.Add(position);
        }

        public void UnblockPosition(Vector2Int position)
        {
            m_BlockedPositions.Remove(position);
        }
    }
    
}