using System;
using KinematicCharacterController.Walkthrough.BasicMovement;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class HandsAnimatorConnector : BehaviourModuleConnector
    {
        [SelfInject] private StatesModule m_StatesModule;
        [SelfInject] private HandsAnimatorModule m_HandsAnimatorModule;
        [SelfInject] private AnimatorStateMachineModule m_AnimatorStateMachineModule;
        [SelfInject] private InventoryModule m_InventoryModule;
        [SelfInject] private PlayerControllerModule m_PlayerControllerModule;

        private WeaponModule m_WeaponModule;

        protected override void Initialize()
        {
            m_InventoryModule.WeaponInHandsChanged += InventoryModuleOnWeaponInHandsChanged;
            m_HandsAnimatorModule.Updated += HandsAnimatorModuleOnUpdated;
        }

        private void HandsAnimatorModuleOnUpdated()
        {
            m_HandsAnimatorModule.TargetSpeed = m_PlayerControllerModule.Character.Motor.BaseVelocity.magnitude;
        }

        private void InventoryModuleOnWeaponInHandsChanged(WeaponItem nextWeapon, WeaponItem oldWeapon)
        {
            if (oldWeapon != null)
            {
                var oldWeaponModule = oldWeapon.UsableItemEntity.GetBehaviorModuleByType<WeaponModule>();
                oldWeaponModule.WeaponFired -= WeaponModuleOnWeaponFired;
                if (nextWeapon != null)
                {
                    m_AnimatorStateMachineModule.SetAnimationState("ChangeWeapon", true);
                }
                else
                {
                    m_AnimatorStateMachineModule.SetAnimationState("Holster", true);
                }
            }

            if (nextWeapon != null)
            {
                m_WeaponModule = nextWeapon.UsableItemEntity.GetBehaviorModuleByType<WeaponModule>();
                m_WeaponModule.WeaponFired += WeaponModuleOnWeaponFired;

                m_StatesModule.SetState<HandsStandState>();
                if (oldWeapon == null)
                {
                    m_AnimatorStateMachineModule.SetAnimationState("Draw");
                }
            }
            else
            {
                m_AnimatorStateMachineModule.SetAnimationState("Holster", true);
            }
        }

        private void WeaponModuleOnWeaponFired(AbstractEntity obj)
        {
            m_AnimatorStateMachineModule.SetAnimationState("Fired");
        }
    }
}