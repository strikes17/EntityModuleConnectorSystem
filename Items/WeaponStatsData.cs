using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponStatsData : ICloneable
    {
        [SerializeField, Range(0f, 1f)] private float m_Flatness;
        [SerializeField, Range(0f, 1f)] private float m_Accuracy;
        [SerializeField, Range(0f, 1f)] private float m_Comfort;
        
        [SerializeField] private float m_Damage;
        [SerializeField] private float m_ReloadTimeInSeconds;
        [SerializeField] private float m_FireCooldownTime;
        [SerializeField] private float m_Drawback;
        [SerializeField] private float m_SpreadAngle;
        
        [SerializeField] private int m_FireResourceValue;
        [SerializeField] private int m_MagazineCapacity;

        public float SpreadAngle => m_SpreadAngle;

        public float ReloadTimeInSeconds => m_ReloadTimeInSeconds;

        public float FireCooldownTime => m_FireCooldownTime;

        public float Drawback => m_Drawback;

        public int MagazineCapacity => m_MagazineCapacity;

        public float Comfort => m_Comfort;

        public float Flatness => m_Flatness;

        public float Accuracy => m_Accuracy;

        public int FireResourceValue => m_FireResourceValue;

        public float Damage => m_Damage;

        public object Clone()
        {
            return MemberwiseClone();
        }
    }
}