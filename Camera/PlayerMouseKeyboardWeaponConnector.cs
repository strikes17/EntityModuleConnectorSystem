using System;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerMouseKeyboardWeaponConnector : BehaviourModuleConnector
    {
        [SelfInject] private PlayerMouseKeyboardInputModule m_PlayerMouseKeyboardInputModule;
        [SelfInject] private InventoryModule m_InventoryModule;

        private WeaponModule m_WeaponModule;

        protected override void Initialize()
        {
            m_PlayerMouseKeyboardInputModule.PlayerEntityFirePressed +=
                MouseEntityInteractionKeyPressed;
            m_PlayerMouseKeyboardInputModule.PlayerEntityFireHold +=
                MouseEntityInteractionKeyPressed;
            m_InventoryModule.WeaponInHandsChanged += InventoryModuleOnWeaponInHandsChanged;
            if (m_InventoryModule.WeaponInHands != null)
            {
                m_WeaponModule =
                    m_InventoryModule.WeaponInHands.UsableItemEntity.GetBehaviorModuleByType<WeaponModule>();
            }

            m_PlayerMouseKeyboardInputModule.PrimaryWeaponSelected +=
                PlayerMouseKeyboardInputModuleOnPrimaryWeaponSelected;
            m_PlayerMouseKeyboardInputModule.SecondaryWeaponSelected +=
                PlayerMouseKeyboardInputModuleOnSecondaryWeaponSelected;
            m_PlayerMouseKeyboardInputModule.PistolWeaponSelected +=
                PlayerMouseKeyboardInputModuleOnPistolWeaponSelected;
        }

        private void PlayerMouseKeyboardInputModuleOnPistolWeaponSelected()
        {
            m_InventoryModule.SetPistolWeaponInHands();
        }

        private void PlayerMouseKeyboardInputModuleOnSecondaryWeaponSelected()
        {
            m_InventoryModule.SetSecondaryWeaponInHands();
        }

        private void PlayerMouseKeyboardInputModuleOnPrimaryWeaponSelected()
        {
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
            // Debug.Log($"Pressed fire! {m_WeaponModule != null}");
            if (m_WeaponModule != null)
            {
                m_WeaponModule.Fire(m_AbstractEntity);
            }
        }
    }
}