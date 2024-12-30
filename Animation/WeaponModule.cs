using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, Vector3, WeaponEntity> WeaponFired = delegate { };

        [SerializeField] private WeaponDataObject m_WeaponDataObject;
        [SerializeField] private Transform m_WeaponFirePoint;

        public void Fire(AbstractEntity weaponUser, AbstractEntity targetEntity)
        {
            var direction = (targetEntity.transform.position - m_WeaponFirePoint.position).normalized;
            Ray ray = new Ray(m_WeaponFirePoint.transform.position, direction);
            if (Physics.Raycast(ray, Mathf.Infinity))
            {

            }
            WeaponFired(weaponUser, direction, m_AbstractEntity as WeaponEntity);
        }
    }
}