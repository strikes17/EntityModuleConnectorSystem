﻿using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class PickableWeaponConnector : BehaviourModuleConnector
    {
        [Inject] private EntityGameUpdateHandlerRegisterModule m_HandlerRegisterModule;
        [Inject] private WeaponsContainer m_WeaponsContainer;
        
        [SelfInject] private EntityInteractModule m_EntityInteractModule;
        
        [SerializeField] private WeaponDataObject m_WeaponDataObject;

        protected override void Initialize()
        {
            m_EntityInteractModule.InteractStarted += EntityInteractModuleOnInteractStarted;
        }

        private void EntityInteractModuleOnInteractStarted(AbstractEntity entity, AbstractEntity user)
        {
            if (entity == m_AbstractEntity)
            {
                var inventoryModule = user.GetBehaviorModuleByType<InventoryModule>();
                if (inventoryModule != null)
                {
                    entity.gameObject.SetActive(false);
                    var weaponEntity = m_WeaponsContainer.SpawnWeapon(m_WeaponDataObject);
                    WeaponItem weaponItem = new WeaponItem(m_WeaponDataObject, weaponEntity);
                    inventoryModule.AddItem(weaponItem);
                    m_HandlerRegisterModule.Register(weaponItem.UsableItemEntity);
                }   
            }
        }
    }
}