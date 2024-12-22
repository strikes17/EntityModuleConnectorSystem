using UnityEngine;

namespace _Project.Scripts
{
    public class GridIntegerPositionModule : AbstractBehaviourModule
    {
        public Vector2Int GridPosition => new Vector2Int(Mathf.RoundToInt(m_AbstractEntity.transform.position.x),
            Mathf.RoundToInt(m_AbstractEntity.transform.position.z));
    }
}