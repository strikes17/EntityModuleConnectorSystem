using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class HandsWeaponAnimatorSwitcherConnector : BehaviourModuleConnector
    {
        [SelfInject] private InventoryModule m_InventoryModule;
        [SelfInject] private HandsAnimatorSwitcherModule m_AnimatorSwitcherModule;
        [SelfInject] private AnimatorStateMachineModule m_AnimatorStateMachineModule;

        private WeaponItem m_OldWeapon;
        private WeaponItem m_NewWeapon;

        protected override void Initialize()
        {
            m_InventoryModule.WeaponInHandsChanged += InventoryModuleOnWeaponInHandsChanged;
        }

        private void InventoryModuleOnWeaponInHandsChanged(WeaponItem newWeapon, WeaponItem oldWeapon)
        {
            m_AnimatorStateMachineModule.StateFinished -= AnimatorStateMachineModuleOnStateFinished;
            if (newWeapon != null && oldWeapon == null)
            {
                m_AnimatorSwitcherModule.ShowHands();
                m_AnimatorSwitcherModule.SetAnimatorForWeapon(newWeapon);
            }
            else
            {
                m_OldWeapon = oldWeapon;
                m_NewWeapon = newWeapon;
                m_AnimatorSwitcherModule.ShowHands();
                m_AnimatorStateMachineModule.StateFinished += AnimatorStateMachineModuleOnStateFinished;
            }
        }

        private void AnimatorStateMachineModuleOnStateFinished(string stateName)
        {
            m_AnimatorStateMachineModule.StateFinished -= AnimatorStateMachineModuleOnStateFinished;
            m_AnimatorSwitcherModule.SetAnimatorForWeapon(m_NewWeapon);
            m_NewWeapon = null;
            m_OldWeapon = null;
        }
    }
}