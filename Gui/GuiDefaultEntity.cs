using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace _Project.Scripts
{
    public class GuiDefaultEntity : AbstractEntity, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler,
        IDragHandler, IEndDragHandler
    {
        [SerializeField, ReadOnly] private RectTransform m_RectTransform;

        public RectTransform RectTransform => m_RectTransform;

        private List<GuiAbstractBehaviourModule> m_PointerDownHandlers;
        private List<GuiAbstractBehaviourModule> m_PointerUpHandlers;
        private List<GuiAbstractBehaviourModule> m_BeginDragHandlers;
        private List<GuiAbstractBehaviourModule> m_DragHandlers;
        private List<GuiAbstractBehaviourModule> m_EndDragHandlers;

        protected override void OnValidate()
        {
            base.OnValidate();
            m_RectTransform = GetComponent<RectTransform>();
        }

        protected override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);

            m_PointerDownHandlers = new List<GuiAbstractBehaviourModule>();
            m_PointerUpHandlers = new List<GuiAbstractBehaviourModule>();
            m_BeginDragHandlers = new List<GuiAbstractBehaviourModule>();
            m_DragHandlers = new List<GuiAbstractBehaviourModule>();
            m_EndDragHandlers = new List<GuiAbstractBehaviourModule>();

            foreach (var abstractBehaviourModule in m_BehaviourModules)
            {
                if (abstractBehaviourModule is GuiAbstractBehaviourModule guiModule)
                {
                    MethodInfo virtualMethod =
                        typeof(GuiAbstractBehaviourModule).GetMethod("OnPointerDown",
                            BindingFlags.Public | BindingFlags.Instance);
                    if (Utility.IsOverridingVirtualMethod(guiModule.GetType(), virtualMethod))
                    {
                        m_PointerDownHandlers.Add(guiModule);
                        // Debug.Log($"Added PointerDown for {guiModule.GetType()}");
                    }

                    virtualMethod =
                        typeof(GuiAbstractBehaviourModule).GetMethod("OnPointerUp",
                            BindingFlags.Public | BindingFlags.Instance);
                    if (Utility.IsOverridingVirtualMethod(guiModule.GetType(), virtualMethod))
                    {
                        m_PointerUpHandlers.Add(guiModule);
                        // Debug.Log($"Added PointerUp for {guiModule.GetType()}");
                    }

                    virtualMethod =
                        typeof(GuiAbstractBehaviourModule).GetMethod("OnBeginDrag",
                            BindingFlags.Public | BindingFlags.Instance);
                    if (Utility.IsOverridingVirtualMethod(guiModule.GetType(), virtualMethod))
                    {
                        m_BeginDragHandlers.Add(guiModule);
                        // Debug.Log($"Added BeginDragHandler for {guiModule.GetType()}");
                    }

                    virtualMethod =
                        typeof(GuiAbstractBehaviourModule).GetMethod("OnDrag",
                            BindingFlags.Public | BindingFlags.Instance);
                    if (Utility.IsOverridingVirtualMethod(guiModule.GetType(), virtualMethod))
                    {
                        m_DragHandlers.Add(guiModule);
                        // Debug.Log($"Added DragHandler for {guiModule.GetType()}");
                    }

                    virtualMethod =
                        typeof(GuiAbstractBehaviourModule).GetMethod("OnEndDrag",
                            BindingFlags.Public | BindingFlags.Instance);
                    if (Utility.IsOverridingVirtualMethod(guiModule.GetType(), virtualMethod))
                    {
                        m_EndDragHandlers.Add(guiModule);
                        // Debug.Log($"Added EndDragHandler for {guiModule.GetType()}");
                    }
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (var guiModule in m_PointerDownHandlers)
            {
                guiModule.OnPointerDown(eventData);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            foreach (var guiModule in m_PointerUpHandlers)
            {
                guiModule.OnPointerUp(eventData);
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            foreach (var guiModule in m_BeginDragHandlers)
            {
                guiModule.OnBeginDrag(eventData);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            foreach (var guiModule in m_DragHandlers)
            {
                guiModule.OnDrag(eventData);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            foreach (var guiModule in m_EndDragHandlers)
            {
                guiModule.OnEndDrag(eventData);
            }
        }
    }
}