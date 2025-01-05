using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class HandsWeaponAnimatorBindingData
    {
        [SerializeField] private RuntimeAnimatorController m_RuntimeAnimatorController;
        [SerializeField] private Vector3 m_HandsPosition;
        [SerializeField] private Vector3 m_HandsRotation;

        public RuntimeAnimatorController RuntimeAnimatorController => m_RuntimeAnimatorController;

        public Vector3 HandsPosition => m_HandsPosition;

        public Vector3 HandsRotation => m_HandsRotation;
    }
}