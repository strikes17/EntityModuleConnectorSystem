using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "Data/New Weapon Data", fileName = "Weapon Data")]
    public class WeaponDataObject : AbstractPickableItemDataObject
    {
        [SerializeField] private WeaponType m_WeaponType;
        [SerializeField] private WeaponEntity m_WeaponEntity;

        public WeaponEntity WeaponEntity => m_WeaponEntity;

        public WeaponType WeaponType => m_WeaponType;
    }
}