using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAiWeaponConnector : BehaviourModuleConnector
    {
        [SelfInject] private InventoryModule m_InventoryModule;
        [SelfInject] private NpcUsableItemHolderModule m_UsableItemHolderModule;
        [SelfInject] private AnimatorStateMachineModule m_AnimatorStateMachineModule;

        private WeaponItem m_OldWeapon;
        private WeaponItem m_NewWeapon;

        protected override void Initialize()
        {
            m_InventoryModule.WeaponInHandsChanged += InventoryModuleOnWeaponInHandsChanged;
            m_InventoryModule.WeaponAdded += InventoryModuleOnWeaponAdded;
        }

        private void InventoryModuleOnWeaponAdded(WeaponItem weaponItem)
        {
            m_UsableItemHolderModule.AddWeaponToHolder(weaponItem);
        }

        private void InventoryModuleOnWeaponInHandsChanged(WeaponItem newWeapon, WeaponItem oldWeapon)
        {
            m_AnimatorStateMachineModule.StateFinished -= AnimatorStateMachineModuleOnStateFinished;
            if (newWeapon != null && oldWeapon == null)
            {
                m_UsableItemHolderModule.ChangeWeaponInHands(newWeapon, oldWeapon);
            }
            else
            {
                m_OldWeapon = oldWeapon;
                m_NewWeapon = newWeapon;
                m_AnimatorStateMachineModule.StateFinished += AnimatorStateMachineModuleOnStateFinished;
            }
        }

        private void AnimatorStateMachineModuleOnStateFinished(string stateName)
        {
            m_AnimatorStateMachineModule.StateFinished -= AnimatorStateMachineModuleOnStateFinished;
            m_UsableItemHolderModule.ChangeWeaponInHands(m_NewWeapon, m_OldWeapon);
            m_NewWeapon = null;
            m_OldWeapon = null;
        }
    }
}