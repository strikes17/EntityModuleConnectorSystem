using System;
using System.Collections;
using System.Reflection;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractBehaviourModule : IUpdateListener, IRegisterUpdateListener, IFixedUpdateListener,
        ILateUpdateListener
    {
#if UNITY_EDITOR
        [Button("Copy type")]
        private void CopyTypeString()
        {
            Clipboard.Copy(GetType().ToString());
        }
#endif

        protected AbstractEntity m_AbstractEntity;

        public virtual void OnUpdate()
        {
        }

        public virtual void OnLateUpdate()
        {
        }

        public virtual void OnFixedUpdate()
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
            MethodInfo virtualMethod =
                typeof(AbstractBehaviourModule).GetMethod("OnUpdate", BindingFlags.Public | BindingFlags.Instance);
            if (Utility.IsOverridingVirtualMethod(GetType(), virtualMethod))
            {
                gameUpdateHandler.AddUpdateListener(this);
            }

            virtualMethod =
                typeof(AbstractBehaviourModule).GetMethod("OnLateUpdate", BindingFlags.Public | BindingFlags.Instance);
            if (Utility.IsOverridingVirtualMethod(GetType(), virtualMethod))
            {
                gameUpdateHandler.AddLateUpdateListener(this);
            }

            virtualMethod =
                typeof(AbstractBehaviourModule).GetMethod("OnFixedUpdate", BindingFlags.Public | BindingFlags.Instance);
            if (Utility.IsOverridingVirtualMethod(GetType(), virtualMethod))
            {
                gameUpdateHandler.AddFixedUpdateListener(this);
            }
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