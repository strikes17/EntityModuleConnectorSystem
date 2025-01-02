using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponStatsData
    {
        [SerializeField] private float m_Flatness;
        [SerializeField] private float m_Accuracy;
        [SerializeField] private float m_Reliability;
        [SerializeField] private float m_Damage;

        public float Flatness => m_Flatness;

        public float Accuracy => m_Accuracy;

        public float Reliability => m_Reliability;

        public float Damage => m_Damage;
    }
}