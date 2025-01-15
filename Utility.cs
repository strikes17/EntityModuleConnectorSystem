using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public static void SetLayerRecursively(GameObject gameObject, int layer)
        {
            if (gameObject == null) return;

            gameObject.layer = layer; // Change the layer of this object
            // Recursively call the method for each child
            foreach (Transform child in gameObject.transform)
            {
                SetLayerRecursively(child.gameObject, layer);
            }
        }

        // public static bool IsInstanceOverridingVirtualMethod<T>(Type derivedType, T instance, string methodName)
        // {
        //     MethodInfo virtualMethod =
        //         typeof(GuiAbstractBehaviourModule).GetMethod(methodName,
        //             BindingFlags.Public | BindingFlags.Instance);
        //     if (IsOverridingVirtualMethod(instance.GetType(), virtualMethod))
        //     {
        //         return true;
        //     }
        //
        //     return false;
        // }

        public static bool IsOverridingVirtualMethod(Type derivedType, MethodInfo virtualMethod)
        {
            // Get method from derived type with the same name and signature as the virtual method
            MethodInfo derivedMethod = derivedType.GetMethod(
                virtualMethod.Name,
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                virtualMethod.GetParameters().Select(p => p.ParameterType).ToArray(),
                null
            );

            if (derivedMethod == null) return false; // If the method is not present, then it is not overridden
            if (derivedMethod.DeclaringType == virtualMethod.DeclaringType)
                return false;

            return true; // This method is not overriding base virtual method.
        }
    }
}