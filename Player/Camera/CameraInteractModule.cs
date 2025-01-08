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
        [SerializeField] private float m_InteractDistance;
        [SerializeField] private UnityEngine.Camera m_Camera;
        [SerializeField] private EventSystem m_EventSystem;

        [SerializeField] private List<GraphicRaycaster> m_GraphicRaycasters;

        [SerializeField] private AbstractEntity m_User;

        public AbstractEntity User => m_User;

        private PointerEventData m_PointerEventData;

        private List<RaycastResult> m_RaycastResults;

        public event Action<AbstractEntity, RaycastHit> HitEntity = delegate { };

        public bool IsInteractionAvailable => m_InteractionBlockers.Count == 0;

        private Dictionary<int, byte> m_InteractionBlockers = new();

        private RaycastHit m_RaycastHit;

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

        public void CameraForwardRaycast(bool isUiBlocking)
        {
            var ray = new Ray(m_Camera.transform.position, m_Camera.transform.forward);
            if (Physics.Raycast(ray, out m_RaycastHit, m_InteractDistance))
            {
                AbstractEntity abstractEntity = m_RaycastHit.collider.GetComponent<AbstractEntity>();
                if (abstractEntity != null)
                {
                    if (isUiBlocking)
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
                    }

                    HitEntity(abstractEntity, m_RaycastHit);
                }
            }
        }
    }
}