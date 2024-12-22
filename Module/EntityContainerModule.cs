using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Contains only runtime spawned entities like towers, npcs etc, not statically placed on scene like context menu entity
    /// </summary>
    [Serializable]
    public class EntityContainerModule : AbstractContainerModule<AbstractEntity>
    {
        [SerializeField] private List<AbstractEntity> m_DebugEntities;

        public AbstractEntity GetEntityByBehaviourModule<T>() where T : AbstractBehaviourModule
        {
            foreach (var abstractEntity in ContainerCollection)
            {
                var module = abstractEntity.GetBehaviorModuleByType<T>();
                if (module != null)
                {
                    return abstractEntity;
                }
            }

            return null;
        }

        [Button]
        private void DebugInfo()
        {
            foreach (var abstractEntity in m_DebugEntities)
            {
                AddElement(abstractEntity);
            }

            foreach (var abstractEntity in ContainerCollection)
            {
                Debug.Log($"{abstractEntity.name}");
            }
        }
    }
}