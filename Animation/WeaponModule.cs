using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, Vector3, WeaponEntity> WeaponFired = delegate { };

        [SerializeField] private WeaponDataObject m_WeaponDataObject;
        [SerializeField] private Transform m_WeaponFirePoint;
        [SerializeField] private GameObject m_BulletPrefab;
        [SerializeField] private GameObject m_HitPrefab;
        [SerializeField] private float m_StartBulletSpeed;

        private Vector3 StartBulletVelocity => m_WeaponFirePoint.forward * m_StartBulletSpeed;

        private Pool<BulletData> m_Pool;
        private BulletData m_MasterBullet;

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
            Moroutine.Run(Test());
        }


        public void Fire(AbstractEntity weaponUser, AbstractEntity targetEntity)
        {
            var bulletData = m_Pool.Get();
            bulletData.Enable();

            bulletData.Velocity = StartBulletVelocity;

            bulletData.StartPosition = m_WeaponFirePoint.position;
            bulletData.BulletGameObject.transform.position = m_WeaponFirePoint.position;

            WeaponFired(weaponUser, Vector3.zero, m_AbstractEntity as WeaponEntity);
        }

        private IEnumerator Test()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                Fire(m_AbstractEntity, m_AbstractEntity);
            }
        }

        public override void OnUpdate()
        {
            IEnumerable<BulletData> bullets = m_Pool.GetActiveElements();
            foreach (var bulletData in bullets)
            {
                var bullet = bulletData.BulletGameObject;
                var velocity = bulletData.Velocity;
                Ray ray = new Ray(bullet.transform.position, bulletData.Velocity.normalized);
                Debug.DrawRay(bullet.transform.position, bulletData.Velocity.normalized, Color.blue);
                if (Physics.Raycast(ray, out RaycastHit hitInfo, bulletData.Velocity.magnitude * Time.deltaTime))
                {
                    if (hitInfo.collider != null)
                    {
                        bulletData.DelayBeforeDisable =
                            (hitInfo.point - bulletData.StartPosition).magnitude / velocity.magnitude;
                        bulletData.Disable();
                        var hitInstance = Object.Instantiate(m_HitPrefab, hitInfo.point, Quaternion.identity);
                    }
                }

                bullet.transform.position = CalculatePositionAtTime(bullet.transform.position, velocity,
                    Physics.gravity, Time.deltaTime);

                bulletData.Update();
            }
        }

        private Vector3 CalculatePositionAtTime(Vector3 position, Vector3 velocity, Vector3 gravity, float time)
        {
            float x = position.x + velocity.x * time;
            float y = position.y + velocity.y * time + 0.5f * gravity.y * time * time;
            float z = position.z + velocity.z * time;
            return new Vector3(x, y, z);
        }
    }
}