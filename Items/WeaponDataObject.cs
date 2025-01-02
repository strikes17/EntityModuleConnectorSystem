using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "Data/New Weapon Data", fileName = "Weapon Data")]
    public class WeaponDataObject : AbstractPickableItemDataObject
    {
        [SerializeField] private WeaponType m_WeaponType;
        [SerializeField] private WeaponEntity m_WeaponEntity;
        [SerializeField] private Vector3 m_NpcHandsPosition;
        [SerializeField] private Vector3 m_NpcHandsRotation;
        [SerializeField] private Vector3 m_NpcHandsScale;
        [SerializeField] private WeaponStatsData m_BaseStatsData;

        public WeaponStatsData BaseStatsData => m_BaseStatsData;

        public Vector3 NpcHandsPosition => m_NpcHandsPosition;

        public Vector3 NpcHandsRotation => m_NpcHandsRotation;

        public Vector3 NpcHandsScale => m_NpcHandsScale;

        public WeaponEntity WeaponEntity => m_WeaponEntity;

        public WeaponType WeaponType => m_WeaponType;
    }
}