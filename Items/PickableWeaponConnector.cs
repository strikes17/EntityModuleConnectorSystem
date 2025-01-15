using System;
using _Project.Scripts.Camera;
using UnityEngine;
using Object = System.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class PickableWeaponConnector : BehaviourModuleConnector
    {
        [Inject] private EntityGameUpdateHandlerRegisterModule m_HandlerRegisterModule;
        [Inject] private WeaponsContainer m_WeaponsContainer;
        [Inject] private GuiContainerModule m_GuiContainerModule;

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
                    bool canAddItem = inventoryModule.CanAddItem(m_WeaponDataObject);

                    if (canAddItem)
                    {
                        entity.gameObject.SetActive(false);
                        WeaponItem weaponItem = null;
                        var weaponEntity = m_WeaponsContainer.SpawnWeapon(m_WeaponDataObject);

                        if (user is PlayerEntity)
                        {
                            var inventoryItemEntity =
                                m_GuiContainerModule.SpawnGuiEntity(m_WeaponDataObject.InventoryItemEntity) as
                                    GuiInventoryItemEntity;
                            inventoryItemEntity.gameObject.SetActive(false);
                            weaponItem = new WeaponItem(m_WeaponDataObject, weaponEntity, inventoryItemEntity, m_HandlerRegisterModule);

                            var itemModule = inventoryItemEntity.GetBehaviorModuleByType<GuiInventoryItemModule>();
                            itemModule.OwnerEntity = user;

                            //hz
                            var assignModule =
                                inventoryItemEntity.GetBehaviorModuleByType<InventoryItemGridAssignModule>();
                            assignModule.AssignNewGrid(GridFillInventoryType.PlayerMain);
                            assignModule.AssignNewGrid(GridFillInventoryType.NpcSell);
                            assignModule.AssignNewGrid(GridFillInventoryType.TraderSell);
                            assignModule.AssignNewGrid(GridFillInventoryType.PlayerStash);
                        }
                        else
                        {
                            weaponItem = new WeaponItem(m_WeaponDataObject, weaponEntity, null, m_HandlerRegisterModule);
                        }



                        inventoryModule.AddItem(weaponItem);

                        m_HandlerRegisterModule.Register(weaponItem.UsableItemEntity);
                    }
                }
            }
        }
    }
}