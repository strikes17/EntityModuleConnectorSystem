using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class NavigationBlockerEntityContainerModule : AbstractContainerModule<AbstractEntity>
    {
        [Button]
        private void DebugInfo()
        {
            foreach (var abstractEntity in ContainerCollection)
            {
                Debug.Log($"{abstractEntity.name}");
            }
        }
    }
}