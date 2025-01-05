using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponHandsPositionData
    {
        [SerializeField] private Vector3 m_HandsPosition;
        [SerializeField] private Vector3 m_HandsRotation;
        [SerializeField] private Vector3 m_HandsScale;

        public Vector3 HandsPosition => m_HandsPosition;

        public Vector3 HandsRotation => m_HandsRotation;

        public Vector3 HandsScale => m_HandsScale;
    }
}