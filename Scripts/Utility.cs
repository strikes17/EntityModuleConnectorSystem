using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = System.Random;

namespace _Project.Scripts
{
    public static class Utility
    {
        public static Vector3Int IntVector3(Vector3 vector3)
        {
            return new Vector3Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y),
                Mathf.RoundToInt(vector3.z));
        }

        public static List<AbstractEntity> FindEntitiesWithModule<T>() where T : AbstractBehaviourModule
        {
            List<AbstractEntity> targets = new();
            var entities =
                Object.FindObjectsByType<AbstractEntity>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var abstractEntity in entities)
            {
                var module = abstractEntity.GetBehaviorModuleByType<T>();
                if (module != null)
                {
                    targets.Add(abstractEntity);
                }
            }

            return targets;
        }

        public static AbstractEntity FindEntityWithModule<T>() where T : AbstractBehaviourModule
        {
            var entities =
                Object.FindObjectsByType<AbstractEntity>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var abstractEntity in entities)
            {
                var module = abstractEntity.GetBehaviorModuleByType<T>();
                if (module != null)
                {
                    return abstractEntity;
                }
            }

            Debug.LogError($"Failed to find module of type {typeof(T)}");
            return null;
        }

        public static GameObject CreatePrimitive(Vector2Int position)
        {
            var primitive = GameObject.CreatePrimitive(PrimitiveType.Cube);
            primitive.transform.position = new Vector3(position.x, 0f, position.y);
            primitive.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            return primitive;
        }
    }
}