using System;
using System.Collections;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractBehaviourModule : IUpdateListener, IRegisterUpdateListener
    {
        [Button("Copy type")]
        private void CopyTypeString()
        {
            Clipboard.Copy(GetType().ToString());
        }

        protected AbstractEntity m_AbstractEntity;

        public virtual void OnUpdate()
        {
        }
        
        public virtual void OnLateUpdate()
        {
        }

        public virtual int Order => 0;

        public virtual void Initialize(AbstractEntity abstractEntity)
        {
            m_AbstractEntity = abstractEntity;
            m_AbstractEntity.Destroyed += OnDestroyed;
        }

        protected virtual void OnDestroyed(GameObject gameObject)
        {
        }

        protected void AllowPostInitialization(int order) => Moroutine.Run(PostInitializeCoroutine(order));

        private IEnumerator PostInitializeCoroutine(int order)
        {
            for (int i = 0; i < order; i++)
            {
                yield return null;
            }

            PostInitialize();
        }

        protected virtual void PostInitialize()
        {
        }

        public void Register(GameUpdateHandler gameUpdateHandler)
        {
            gameUpdateHandler.AddListener(this);
        }

        public void Unregister(GameUpdateHandler gameUpdateHandler)
        {
            gameUpdateHandler.RemoveListener(this);
        }

        public virtual string GetInfo()
        {
            return m_AbstractEntity.name;
        }
    }
}