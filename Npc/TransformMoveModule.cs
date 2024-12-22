using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    public class TransformMoveModule : AbstractBehaviourModule
    {
        [SerializeField] private Transform m_ModelTransform;
        private Vector3 m_Target;
        private float m_Speed;
        private bool m_IsMoving;
        private Vector3 m_StartPosition;
        private Vector3 m_Position;
        private Transform m_Transform;
        private float m_Time;
        private float m_LerpValue;
        private Vector3 m_Direction;
        private Quaternion m_Rotation;

        public void MoveUntilReachTarget(Vector2 target, float speed)
        {
            m_Transform = m_AbstractEntity.transform;
            m_Target = new Vector3(target.x, 0f, target.y);
            m_Speed = speed;
            m_StartPosition = m_Position = m_Transform.position;
            m_Time = 100f / m_Speed;
            m_LerpValue = 0f;
            m_IsMoving = true;
            m_Direction = (m_Target - m_Position).normalized;
            m_Rotation = Quaternion.LookRotation(m_Direction);
        }

        public override void OnUpdate()
        {
            if (m_IsMoving)
            {
                if (m_LerpValue < 1f)
                {
                    m_LerpValue += Time.deltaTime / m_Time;
                    m_Position = Vector3.Lerp(m_StartPosition, m_Target, m_LerpValue);
                    m_ModelTransform.rotation = Quaternion.Lerp(m_ModelTransform.rotation, m_Rotation, m_LerpValue);
                    m_Transform.position = m_Position;
                }
                else
                {
                    m_IsMoving = false;
                }
            }
        }
    }
}