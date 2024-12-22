using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public abstract class AbstractEntity : MonoBehaviour
    {
        public event Action<GameObject> Destroyed = delegate { };

        public IEnumerable<AbstractBehaviourModule> BehaviourModules => m_BehaviourModules;

        public IEnumerable<AbstractValueModule> ValueModules => m_ValueModules;

        [SerializeReference] protected List<AbstractValueModule> m_ValueModules = new();
        [SerializeReference] protected List<AbstractBehaviourModule> m_BehaviourModules = new();
        [SerializeReference] protected List<BehaviourModuleConnector> m_Connectors = new();

        private event Action<AbstractValueModule> ValueModuleAdded = delegate { };

        public IEnumerable<AbstractEntity> ChildEntities => m_ChildEntities;

        [SerializeField, ReadOnly] private List<AbstractEntity> m_ChildEntities;

        protected virtual void OnValueModuleAdded(AbstractValueModule abstractValueModule) =>
            ValueModuleAdded(abstractValueModule);

        public IEnumerable<BehaviourModuleConnector> Connectors => m_Connectors;

        public List<T> GetBehaviorModulesCollectionByType<T>() where T : AbstractBehaviourModule
        {
            return m_BehaviourModules.Where(x =>
                x.GetType().IsSubclassOf(typeof(T)) ||
                x.GetType() == typeof(T)).Select(x => x as T).ToList();
        }

        public T GetBehaviorModuleByType<T>() where T : AbstractBehaviourModule
        {
            return m_BehaviourModules.FirstOrDefault(x =>
                x.GetType().IsSubclassOf(typeof(T)) ||
                x.GetType() == typeof(T)) as T;
        }

        public AbstractBehaviourModule GetBehaviorModuleByType(Type behaviourModuleType)
        {
            return m_BehaviourModules.FirstOrDefault(x =>
                x.GetType().IsSubclassOf(behaviourModuleType) ||
                x.GetType() == behaviourModuleType);
        }

        public T GetValueModuleByType<T>() where T : AbstractValueModule
        {
            var valueModule = m_ValueModules.FirstOrDefault(x =>
                x.GetType().IsSubclassOf(typeof(T)) ||
                x.GetType() == typeof(T)) as T;
            return valueModule;
        }


        protected virtual void Initialize(AbstractEntity abstractEntity)
        {
            m_ChildEntities = transform.GetComponentsInChildren<AbstractEntity>(true).ToList();
            m_ChildEntities.Remove(this);
            StartCoroutine(InitializeCoroutine());
        }

        private IEnumerator InitializeCoroutine()
        {
            m_BehaviourModules.ForEach(x => x.Initialize(this));
            yield return null;
            m_Connectors.ForEach(connector =>
            {
                if (connector.IsResolved)
                {
                    connector.Init(this);
                }
                else
                {
                    connector.Resolved += ConnectorOnResolved;
                    // Debug.LogError($"Connector: {connector.GetType()} of {name} is not resolved!");
                }
            });
        }

        private void ConnectorOnResolved(IResolveTarget resolveTarget)
        {
            var connector = resolveTarget as BehaviourModuleConnector;
            connector.Init(this);
            connector.Resolved -= ConnectorOnResolved;
        }

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            Initialize(this);
        }

        private void OnDestroy()
        {
            Destroyed(gameObject);
        }
    }
}