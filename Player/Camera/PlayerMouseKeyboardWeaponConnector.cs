using System;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerMouseKeyboardWeaponConnector : BehaviourModuleConnector
    {
        [SelfInject] private PlayerInputModule m_PlayerInputModule;
        [SelfInject] private InventoryModule m_InventoryModule;
        [SelfInject] private WeaponAnimationLockerModule m_AnimationLockerModule;
        [Inject(typeof(GuiPdaEntity))] private GuiAbstractVisibilityModule m_GuiPdaVisibilityModule;

        private WeaponModule m_WeaponModule;

        private bool m_WeaponSwitchLocked = false;

        protected override void Initialize()
        {
            m_PlayerInputModule.PlayerEntityFirePressed +=
                MouseEntityInteractionKeyPressed;
            m_PlayerInputModule.PlayerEntityFireHold +=
                MouseEntityInteractionKeyPressed;
            m_InventoryModule.WeaponInHandsChanged += InventoryModuleOnWeaponInHandsChanged;
            if (m_InventoryModule.WeaponInHands != null)
            {
                m_WeaponModule =
                    m_InventoryModule.WeaponInHands.UsableItemEntity.GetBehaviorModuleByType<WeaponModule>();
            }

            m_PlayerInputModule.PrimaryWeaponSelected +=
                PlayerInputModuleOnPrimaryWeaponSelected;
            m_PlayerInputModule.SecondaryWeaponSelected +=
                PlayerInputModuleOnSecondaryWeaponSelected;
            m_PlayerInputModule.PistolWeaponSelected +=
                PlayerInputModuleOnPistolWeaponSelected;

            m_AnimationLockerModule.AnimationFinished += AnimationLockerModuleOnAnimationFinished;
        }

        private void AnimationLockerModuleOnAnimationFinished()
        {
            m_WeaponSwitchLocked = false;
        }

        private void PlayerInputModuleOnPistolWeaponSelected()
        {
            if (m_WeaponSwitchLocked)
            {
                return;
            }

            m_WeaponSwitchLocked = true;
            m_InventoryModule.SetPistolWeaponInHands();
        }

        private void PlayerInputModuleOnSecondaryWeaponSelected()
        {
            if (m_WeaponSwitchLocked)
            {
                return;
            }

            m_WeaponSwitchLocked = true;
            m_InventoryModule.SetSecondaryWeaponInHands();
        }

        private void PlayerInputModuleOnPrimaryWeaponSelected()
        {
            if (m_WeaponSwitchLocked)
            {
                return;
            }

            m_WeaponSwitchLocked = true;
            m_InventoryModule.SetPrimaryWeaponInHands();
        }

        private void InventoryModuleOnWeaponInHandsChanged(WeaponItem nextWeapon, WeaponItem oldWeapon)
        {
            if (nextWeapon != null)
            {
                m_WeaponModule = nextWeapon.UsableItemEntity.GetBehaviorModuleByType<WeaponModule>();
            }
            else
            {
                m_WeaponModule = null;
            }
        }

        private void MouseEntityInteractionKeyPressed()
        {
            if (m_WeaponModule != null && !m_WeaponSwitchLocked && !m_GuiPdaVisibilityModule.IsShown)
            {
                m_WeaponModule.Fire(m_AbstractEntity);
            }
        }
    }
}