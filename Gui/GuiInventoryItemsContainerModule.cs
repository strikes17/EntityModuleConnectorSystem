using System;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiInventoryItemsContainerModule : EntityContainerModule
    {
        public GuiInventoryItemEntity SpawnInventoryItemEntity(GuiInventoryItemEntity entityPrefab)
        {
            var instance = Object.Instantiate(entityPrefab);
            AddElement(instance);
            return instance;
        }
    }
}