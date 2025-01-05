using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, RaycastHit> BulletHit = delegate { };
        public event Action<AbstractEntity> WeaponFired = delegate { };

        [SerializeField] private WeaponDataObject m_WeaponDataObject;
        [SerializeField] private Transform m_WeaponFirePoint;
        [SerializeField] private GameObject m_BulletPrefab;
        [SerializeField] private GameObject m_HitPrefab;
        [SerializeField] private float m_StartBulletSpeed;
        [SerializeField] private WeaponStatsData m_WeaponStatsData;

        public int AmmoCount
        {
            get => m_AmmoCount; 
            set { m_AmmoCount = value; }
        }

        private int m_AmmoCount;

        private float m_FireCooldownUpdateTime;

        private Vector3 StartBulletVelocity
        {
            get
            {
                var spreadAngle = m_WeaponDataObject.BaseStatsData.SpreadAngle;
                var acc = m_WeaponDataObject.BaseStatsData.Accuracy;
                var coff = 1f - acc;
                var rnd = Random.Range(0, coff);
                var forward = m_WeaponFirePoint.forward;
                float randomAngle = spreadAngle * rnd;

                Vector3 randomAxis = Random.onUnitSphere;
                Vector3 randomDirection = forward.Rotate(randomAngle, randomAxis);

                return randomDirection * m_StartBulletSpeed;
            }
        }

        private Pool<BulletData> m_Pool;
        private BulletData m_MasterBullet;

        private bool m_IsFireOnCooldown;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_MasterBullet = new BulletData()
            {
                BulletGameObject = m_BulletPrefab,
                Velocity = StartBulletVelocity,
                DelayBeforeDisable = 1f,
                StartPosition = m_WeaponFirePoint.position
            };
            m_Pool = new Pool<BulletData>(m_MasterBullet);
            m_WeaponStatsData = m_WeaponDataObject.BaseStatsData.Clone() as WeaponStatsData;
            // Moroutine.Run(Test());
        }


        public void Fire(AbstractEntity weaponUser)
        {
            if (m_IsFireOnCooldown)
            {
                return;
            }

            for (int i = 0; i < 1; i++)
            {
                var bulletData = m_Pool.Get();
                bulletData.Enable();

                bulletData.Velocity = StartBulletVelocity;

                bulletData.StartPosition = m_WeaponFirePoint.position;
                bulletData.BulletGameObject.transform.position = m_WeaponFirePoint.position;
            }

            m_AmmoCount--;

            WeaponFired(weaponUser);

            m_FireCooldownUpdateTime = 0f;
        }

        private IEnumerator Test()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                Fire(m_AbstractEntity);
            }
        }

        public override void OnFixedUpdate()
        {
            IEnumerable<BulletData> bullets = m_Pool.GetActiveElements();
            foreach (var bulletData in bullets)
            {
                var bullet = bulletData.BulletGameObject;
                var velocity = CalculateNewVelocity(bulletData.Velocity, Physics.gravity, Time.fixedDeltaTime);
                var velocityNormalized = velocity.normalized;
                Ray ray = new Ray(bullet.transform.position, velocityNormalized);
                // Debug.DrawRay(bullet.transform.position, velocityNormalized * velocity.magnitude, Color.blue);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, velocity.magnitude))
                {
                    if (hitInfo.collider != null)
                    {
                        bulletData.DelayBeforeDisable =
                            (hitInfo.point - bulletData.StartPosition).magnitude / velocity.magnitude;
                        bulletData.Disable();
                        Object.Instantiate(m_HitPrefab, hitInfo.point, quaternion.identity);
                        BulletHit(m_AbstractEntity, hitInfo);
                    }
                }

                bullet.transform.position = CalculatePositionAtTime(bullet.transform.position, velocity);

                bulletData.Update();
            }
        }

        public override void OnUpdate()
        {
            if (m_FireCooldownUpdateTime < m_WeaponStatsData.FireCooldownTime)
            {
                m_FireCooldownUpdateTime += Time.deltaTime;
                m_IsFireOnCooldown = true;
            }
            else
            {
                m_IsFireOnCooldown = false;
            }
        }

        private Vector3 CalculatePositionAtTime(Vector3 position, Vector3 velocity)
        {
            float x = position.x + velocity.x;
            float y = position.y + velocity.y;
            float z = position.z + velocity.z;
            return new Vector3(x, y, z);
        }

        private Vector3 CalculateNewVelocity(Vector3 velocity, Vector3 gravity, float time)
        {
            float x = velocity.x;
            float y = velocity.y + (1f - m_WeaponDataObject.BaseStatsData.Flatness) * gravity.y;
            float z = velocity.z;
            return new Vector3(x, y, z) * time;
        }
    }
}