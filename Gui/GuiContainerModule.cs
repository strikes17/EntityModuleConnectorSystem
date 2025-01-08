using System;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiContainerModule : EntityContainerModule
    {
        public GuiDefaultEntity SpawnGuiEntity(GuiDefaultEntity entityPrefab)
        {
            var instance = Object.Instantiate(entityPrefab);
            AddElement(instance);
            return instance;
        }
    }
}