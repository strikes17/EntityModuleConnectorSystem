using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    
    [CreateAssetMenu(menuName = "Data/New Weapon Data", fileName = "Weapon Data")]
    public class WeaponDataObject : AbstractPickableItemDataObject
    {
        [SerializeField] private WeaponType m_WeaponType;
        [SerializeField] private WeaponEntity m_WeaponEntity;

        [SerializeField] private WeaponStatsData m_BaseStatsData;

        [SerializeReference] private List<WeaponHandsPositionData> m_HandsPositionDatas;

        public WeaponStatsData BaseStatsData => m_BaseStatsData;
        
        public T GetPositionData<T>() where T : WeaponHandsPositionData
        {
            var positionData = m_HandsPositionDatas.FirstOrDefault(x => x is T);
            return positionData as T;
        }

        public WeaponEntity WeaponEntity => m_WeaponEntity;

        public WeaponType WeaponType => m_WeaponType;

    }
}