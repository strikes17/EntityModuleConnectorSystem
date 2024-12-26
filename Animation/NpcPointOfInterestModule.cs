using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcPointOfInterestModule : AbstractBehaviourModule
    {
        [SerializeField] private bool m_IsStatic;
        [SerializeField] private Transform m_Transform;

        public Vector3 Position => m_Transform.position;
        
        public bool IsStatic => m_IsStatic;
    }
}