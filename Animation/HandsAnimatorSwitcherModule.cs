using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class HandsAnimatorSwitcherModule : AbstractBehaviourModule
    {
        public event Action<RuntimeAnimatorController> AnimatorChanged = delegate { };

        [SerializeField] private Animator m_Animator;
        [SerializeField] private Transform m_HandsTransform;
        [SerializeField] private HandsWeaponAnimatorBindingDataObject weaponAnimatorBindingDataObject;
        [SerializeField] private RuntimeAnimatorController m_DefaultAnimatorController;

        public void ShowHands()
        {
            m_HandsTransform.gameObject.SetActive(true);
        }

        public void HideHands()
        {
            m_HandsTransform.gameObject.SetActive(false);
        }

        public void SetAnimatorForWeapon(WeaponItem weaponItem)
        {
            if (weaponItem == null)
            {
                HideHands();
                m_Animator.runtimeAnimatorController = m_DefaultAnimatorController;
                AnimatorChanged(m_DefaultAnimatorController);
                return;
            }

            var weaponAnimatorBindingData =
                weaponAnimatorBindingDataObject.GetAnimatorForWeapon(weaponItem.DataObject as WeaponDataObject);
            m_Animator.runtimeAnimatorController = weaponAnimatorBindingData.RuntimeAnimatorController;
            m_HandsTransform.localPosition = weaponAnimatorBindingData.HandsPosition;
            m_HandsTransform.localRotation = Quaternion.Euler(weaponAnimatorBindingData.HandsRotation);

            AnimatorChanged(weaponAnimatorBindingData.RuntimeAnimatorController);
        }

        public override void OnUpdate()
        {
            Debug.DrawRay(m_HandsTransform.transform.position, -m_HandsTransform.forward * 100f, Color.red);
        }
    }
}