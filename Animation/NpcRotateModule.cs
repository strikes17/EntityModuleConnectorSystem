using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcRotateModule : AbstractBehaviourModule
    {
        [SerializeField] private Transform m_Transform;
        
        public void SetLookAt(Vector3 target)
        {
            var direction = -(target - m_Transform.position).normalized;
            var rotation = Quaternion.LookRotation(direction, Vector3.up);
            rotation.eulerAngles = new Vector3(0f, rotation.eulerAngles.y, 0f);
            m_Transform.rotation = rotation;
        }
    }
}