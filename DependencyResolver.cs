using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Project.Scripts.Camera;
using Redcode.Moroutines;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class DependencyResolver : MonoBehaviour, IHaveInit
    {
        [SerializeField] private GameUpdateHandler m_GameUpdateHandler;
        [SerializeField] private List<AbstractEntity> m_RegisterUpdateListeners;

        [SerializeField, ReadOnly] private List<ResolveHandler<IResolveTarget>> m_ConnectorResolvers;
        [SerializeField, ReadOnly] private List<ResolveHandler<AbstractBehaviourModule>> m_BehaviourModulesResolvers;
        [SerializeField, ReadOnly] private List<ResolveHandler<AbstractValueModule>> m_ValueModulesResolvers;

        [SerializeReference, ReadOnly]
        private List<AbstractContainerModule<AbstractEntity>> m_EntityContainerModules = new();

        [SerializeReference, ReadOnly] private List<IResolveTarget> m_FailedConnectors = new();

        //Tutorial
        [SerializeField, ReadOnly] private List<ResolveHandler<IResolveTarget>> m_TutorialActionsResolvers;

        [SerializeField, ReadOnly] private List<ResolveHandler<IResolveTarget>> m_TutorialConditionsResolvers;

        [SerializeReference, ReadOnly] private List<IResolveTarget> m_FailedTutorialActions = new();
        [SerializeReference, ReadOnly] private List<IResolveTarget> m_FailedTutorialConditions = new();

        public void Init()
        {
            m_RegisterUpdateListeners.Clear();
            
            var entities = FindObjectsByType<AbstractEntity>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .ToList();
            
            m_RegisterUpdateListeners.AddRange(entities);
            entities.ForEach(abstractEntity =>
            {
                var containerModule =
                    abstractEntity.GetBehaviorModuleByType<AbstractContainerModule<AbstractEntity>>();
                if (containerModule != null)
                {
                    m_EntityContainerModules.Add(containerModule);
                }
                
                var entityContainerModule = abstractEntity.GetBehaviorModuleByType<EntityContainerModule>();
                if (entityContainerModule != null)
                {
                    m_EntityContainerModules.Add(entityContainerModule);
                }
                
                var entityConnectors = abstractEntity.Connectors;
                foreach (var connector in entityConnectors)
                {
                    ResolveHandler<IResolveTarget> handler =
                        new ResolveHandler<IResolveTarget>(abstractEntity, connector);
                    m_ConnectorResolvers.Add(handler);
                }

                var entityBehaviourModules = abstractEntity.BehaviourModules;
                foreach (var module in entityBehaviourModules)
                {
                    ResolveHandler<AbstractBehaviourModule> handler =
                        new ResolveHandler<AbstractBehaviourModule>(abstractEntity, module);
                    m_BehaviourModulesResolvers.Add(handler);
                }

                var entityValueModules = abstractEntity.ValueModules;
                foreach (var module in entityValueModules)
                {
                    ResolveHandler<AbstractValueModule> handler =
                        new ResolveHandler<AbstractValueModule>(abstractEntity, module);
                    m_ValueModulesResolvers.Add(handler);
                }
            });



            m_ConnectorResolvers.ForEach(handler =>
            {
                var connector = handler.Value;
                Resolve(connector, ref m_ConnectorResolvers, ref m_FailedConnectors);
                // ResolveConnector(connector);
            });

            foreach (var registerUpdateListener in m_RegisterUpdateListeners)
            {
                foreach (var behaviourModule in registerUpdateListener.BehaviourModules)
                {
                    behaviourModule.Register(m_GameUpdateHandler);
                }
            }

            m_EntityContainerModules.ForEach(containerModule =>
            {
                containerModule.ElementAdded += OnEntityAddedToContainer;
            });
            
            m_ConnectorResolvers.Clear();
        }

        private void OnEntityAddedToContainer(AbstractEntity abstractEntity)
        {
            var tutorialEntity = abstractEntity as TutorialStepEntity;
            if(tutorialEntity != null)
            {
                Debug.Log($"At tutorial step entity: {tutorialEntity.name}");
                foreach (var startAction in tutorialEntity.StartActions)
                {
                    ResolveHandler<IResolveTarget> handler =
                        new ResolveHandler<IResolveTarget>(tutorialEntity, startAction);
                    m_TutorialActionsResolvers.Add(handler);
                }

                foreach (var endAction in tutorialEntity.EndActions)
                {
                    ResolveHandler<IResolveTarget> handler =
                        new ResolveHandler<IResolveTarget>(tutorialEntity, endAction);
                    m_TutorialActionsResolvers.Add(handler);
                }

                foreach (var startCondition in tutorialEntity.StartConditions)
                {
                    ResolveHandler<IResolveTarget> handler =
                        new ResolveHandler<IResolveTarget>(tutorialEntity, startCondition);
                    m_TutorialConditionsResolvers.Add(handler);
                }

                foreach (var endCondition in tutorialEntity.EndConditions)
                {
                    ResolveHandler<IResolveTarget> handler =
                        new ResolveHandler<IResolveTarget>(tutorialEntity, endCondition);
                    m_TutorialConditionsResolvers.Add(handler);
                }

                m_TutorialActionsResolvers.ForEach(handler =>
                {
                    Resolve(handler.Value, ref m_TutorialActionsResolvers, ref m_FailedTutorialActions);
                });

                m_TutorialConditionsResolvers.ForEach(handler =>
                {
                    Resolve(handler.Value, ref m_TutorialConditionsResolvers, ref m_FailedTutorialConditions);
                });
            }

            var entityBehaviourModules = abstractEntity.BehaviourModules;
            foreach (var module in entityBehaviourModules)
            {
                ResolveHandler<AbstractBehaviourModule> handler =
                    new ResolveHandler<AbstractBehaviourModule>(abstractEntity, module);
                m_BehaviourModulesResolvers.Add(handler);
            }

            var entityValueModules = abstractEntity.ValueModules;
            foreach (var module in entityValueModules)
            {
                ResolveHandler<AbstractValueModule> handler =
                    new ResolveHandler<AbstractValueModule>(abstractEntity, module);
                m_ValueModulesResolvers.Add(handler);
            }

            var entityConnectors = abstractEntity.Connectors;
            foreach (var connector in entityConnectors)
            {
                ResolveHandler<IResolveTarget> handler =
                    new ResolveHandler<IResolveTarget>(abstractEntity, connector);
                m_ConnectorResolvers.Add(handler);
            }

            foreach (var connector in entityConnectors)
            {
                Resolve(connector, ref m_ConnectorResolvers, ref m_FailedConnectors);
                // ResolveConnector(connector);
            }

            List<AbstractEntity> childEntities = abstractEntity.ChildEntities.ToList();
            if (childEntities.Count > 0)
            {
                foreach (var childEntity in childEntities)
                {
                    OnEntityAddedToContainer(childEntity);
                }
            }

            if (m_FailedConnectors.Count > 0)
            {
                if (m_Moroutine != null)
                {
                    m_Moroutine.Stop();
                }

                Moroutine.Run(ResolveCoroutine());
            }
        }

        private Moroutine m_Moroutine;

        private IEnumerator ResolveCoroutine()
        {
            for (var i = 0; i < m_FailedConnectors.Count; i++)
            {
                var connector = m_FailedConnectors[i];
                // bool isResolved = ResolveConnector(connector);
                bool isResolved = Resolve(connector, ref m_ConnectorResolvers, ref m_FailedConnectors);
                if (isResolved)
                {
                    Debug.Log($"Resolved: {connector.GetType()}");
                    m_FailedConnectors.Remove(connector);
                }

                yield return null;
            }
        }

        private bool Resolve(IResolveTarget resolveTarget, ref List<ResolveHandler<IResolveTarget>> resolveHandlers,
            ref List<IResolveTarget> failedList)
        {
            bool isResolved = false;
            int resolvesCount = 0;
            int targetResolvesCount = 0;
            var type = resolveTarget.GetType();
            var fields = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute(typeof(InjectAttribute)) is InjectAttribute attribute)
                {
                    Type targetEntityType = attribute.Type;
                    targetResolvesCount++;
                    var fieldType = field.FieldType;
                    ResolveHandler<AbstractBehaviourModule> behaviourResolve = null;
                    if (targetEntityType != null)
                    {
                        behaviourResolve = m_BehaviourModulesResolvers.Find(handler =>
                            (handler.Value.GetType().IsSubclassOf(fieldType) || handler.Value.GetType() == fieldType) &&
                            (handler.AbstractEntity.GetType() == type));
                    }
                    else
                    {
                        behaviourResolve = m_BehaviourModulesResolvers.Find(handler =>
                            handler.Value.GetType().IsSubclassOf(fieldType) || handler.Value.GetType() == fieldType);
                    }

                    if (behaviourResolve != null)
                    {
                        var behaviourModule = behaviourResolve.Value;
                        if (behaviourModule != null)
                        {
                            field.SetValue(resolveTarget, behaviourModule);
                            // Debug.Log($"Resolved connector: {connector}, module: {fieldType}");
                            resolvesCount++;
                        }
                        else
                        {
                            Debug.LogError($"Failed to resolve: {resolveTarget}, behaviourModule is null");
                        }
                    }
                    else
                    {
                        var valueResolve = m_ValueModulesResolvers.Find(handler =>
                            handler.Value.GetType().IsSubclassOf(fieldType) || handler.Value.GetType() == fieldType);
                        if (valueResolve != null)
                        {
                            var valueModule = valueResolve.Value;
                            if (valueModule != null)
                            {
                                field.SetValue(resolveTarget, valueModule);
                                // Debug.Log($"Resolved connector: {connector}, module: {fieldType}");
                                resolvesCount++;
                            }
                            else
                            {
                                Debug.LogError($"Failed to resolve: {resolveTarget}, value module is null");
                            }
                        }
                        else
                        {
                            if (!failedList.Contains(resolveTarget))
                            {
                                failedList.Add(resolveTarget);
                            }

                            Debug.LogError($"Failed to resolve: {resolveTarget}, handler is null for: {fieldType}");
                        }
                    }
                }

                if (field.GetCustomAttribute(typeof(SelfInjectAttribute)) is SelfInjectAttribute selfAttribute)
                {
                    targetResolvesCount++;
                    var fieldType = field.FieldType;
                    ResolveHandler<IResolveTarget> targetResolveHandlers =
                        resolveHandlers.FirstOrDefault(handler => handler.Value == resolveTarget);
                    AbstractEntity connectorEntity = targetResolveHandlers?.AbstractEntity;
                    if (connectorEntity != null)
                    {
                        var resolveHandler = m_BehaviourModulesResolvers.FirstOrDefault(handler =>
                            handler.AbstractEntity == connectorEntity &&
                            (handler.Value.GetType().IsSubclassOf(fieldType) || handler.Value.GetType() == fieldType));
                        if (resolveHandler != null)
                        {
                            var behaviourModule = resolveHandler.Value;
                            if (behaviourModule != null)
                            {
                                field.SetValue(resolveTarget, behaviourModule);
                                // Debug.Log($"Resolved connector: {connector}, self module: {fieldType}");
                                resolvesCount++;
                            }
                            else
                            {
                                // Debug.LogError($"Failed to resolve: {connector}, self behaviourModule is null");
                            }
                        }
                        else
                        {
                            var valueResolve = m_ValueModulesResolvers.FirstOrDefault(handler =>
                                handler.AbstractEntity == connectorEntity &&
                                (handler.Value.GetType().IsSubclassOf(fieldType) ||
                                 handler.Value.GetType() == fieldType));
                            if (valueResolve != null)
                            {
                                var valueModule = valueResolve.Value;
                                if (valueModule != null)
                                {
                                    field.SetValue(resolveTarget, valueModule);
                                    // Debug.Log($"Resolved connector: {connector}, module: {fieldType}");
                                    resolvesCount++;
                                }
                                else
                                {
                                    Debug.LogError($"Failed to resolve: {resolveTarget}, value module is null");
                                }
                            }
                            else
                            {
                                if (!failedList.Contains(resolveTarget))
                                {
                                    failedList.Add(resolveTarget);
                                }

                                Debug.LogError(
                                    $"Failed to resolve: {resolveTarget}, self handler is null for: {fieldType}");
                            }
                        }
                    }
                }
            }

            isResolved = resolvesCount == targetResolvesCount;

            // Debug.Log($"resolved for {connector.GetType()}: {resolvesCount}/{targetResolvesCount}");
            resolveTarget.IsResolved = isResolved;

            return isResolved;
        }

        public int Order => 10;
    }
}