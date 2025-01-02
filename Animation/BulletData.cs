using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class BulletData : IPoolable<BulletData>
    {
        public float DelayBeforeDisable = 0.2f;
        
        public Vector3 Velocity;

        public GameObject BulletGameObject;

        public Vector3 StartPosition;

        public Action<BulletData> Enabled { get; set; }
        public Action<BulletData> Disabled { get; set; }
        public bool IsAvailable => !BulletGameObject.activeSelf;

        private bool m_IsCalledToDisable = false;
        private float m_UpdateTime = 0f;

        public BulletData CreateInstance()
        {
            return new BulletData()
            {
                BulletGameObject = Object.Instantiate(BulletGameObject),
                Velocity = Velocity,
                DelayBeforeDisable = DelayBeforeDisable,
                StartPosition = StartPosition
            };
        }

        public void Enable()
        {
            BulletGameObject.SetActive(true);
        }

        public void Disable()
        {
            // m_IsCalledToDisable = true;
            BulletGameObject.SetActive(false);
        }

        public void Update()
        {
            // if (m_IsCalledToDisable)
            // {
            //     m_UpdateTime += Time.deltaTime;
            //     if (m_UpdateTime >= 0.2f)
            //     {
            //         BulletGameObject.SetActive(false);
            //         m_IsCalledToDisable = false;
            //         m_UpdateTime = 0f;
            //     }
            // }
        }
    }
}