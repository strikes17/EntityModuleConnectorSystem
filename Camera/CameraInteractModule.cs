using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class CameraInteractModule : AbstractBehaviourModule
    {
        [SerializeField] private CameraView m_CameraView;
        [SerializeField] private EventSystem m_EventSystem;

        [SerializeField] private List<GraphicRaycaster> m_GraphicRaycasters;

        private PointerEventData m_PointerEventData;

        private List<RaycastResult> m_RaycastResults;

        public event Action<AbstractEntity, RaycastHit> EntityClickedDown = delegate { };

        public event Action<AbstractEntity, RaycastHit> EntityClickedUp = delegate { };
        
        public bool IsInteractionAvailable => m_InteractionBlockers.Count == 0;

        private Dictionary<int, byte> m_InteractionBlockers = new();

        public override int Order => 10;

        public void TryBlockInteraction(int blockerHashCode)
        {
            m_InteractionBlockers.TryAdd(blockerHashCode, 0);
        }

        public void TryUnblockInteraction(int blockerHashCode)
        {
            m_InteractionBlockers.Remove(blockerHashCode);
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_PointerEventData = new PointerEventData(null);
            m_RaycastResults = new List<RaycastResult>();
        }

        public override void OnUpdate()
        {
            if (!IsInteractionAvailable)
                return;
            RaycastHit hit;
            var input = Input.mousePosition;
            var ray = m_CameraView.Camera.ScreenPointToRay(input);
            InputType inputType = InputType.None;
            if (Input.GetMouseButton(0))
            {
                inputType = InputType.Hold;
            }

            if (Input.GetMouseButtonDown(0))
            {
                inputType = InputType.OneClick;
            }

            if (Input.GetMouseButtonUp(0))
            {
                inputType = InputType.End;
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                AbstractEntity abstractEntity = hit.collider.GetComponent<AbstractEntity>();
                if (abstractEntity != null)
                {
                    if (inputType != InputType.None)
                    {
                        RaycastTargetBehaviourModule raycastTargetBehaviour =
                            abstractEntity.GetBehaviorModuleByType<RaycastTargetBehaviourModule>();
                        if (raycastTargetBehaviour != null)
                        {
                            if (inputType == InputType.OneClick)
                            {
                                EntityClickedDown(abstractEntity, hit);
                                // raycastTargetBehaviour.OnStart(hit);
                            }
                            else if (inputType == InputType.Hold)
                            {
                                // raycastTargetBehaviour.OnHold(hit);
                            }
                            else if (inputType == InputType.End)
                            {
                                m_PointerEventData.position = Input.mousePosition;
                                foreach (var graphicRaycaster in m_GraphicRaycasters)
                                {
                                    m_RaycastResults.Clear();
                                    graphicRaycaster.Raycast(m_PointerEventData, m_RaycastResults);
                                    if (m_RaycastResults.Count > 0)
                                    {
                                        return;
                                    }
                                }
                                EntityClickedUp(abstractEntity, hit);
                            }
                        }
                    }
                }
            }
        }
    }
}