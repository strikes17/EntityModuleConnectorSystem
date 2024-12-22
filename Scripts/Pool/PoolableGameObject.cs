using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class PoolableGameObject : MonoBehaviour, IPoolable<PoolableGameObject>
    {
        public Action<PoolableGameObject> Enabled { get; set; }
        
        public Action<PoolableGameObject> Disabled { get; set; }

        public bool IsAvailable => !gameObject.activeSelf;

        public PoolableGameObject CreateInstance() => Instantiate(this);

        public void Enable() => gameObject.SetActive(true);

        public void Disable() => gameObject.SetActive(false);
    }
}