using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialFadeBackingMask : AbstractTutorialAction
    {
        [SerializeField] private bool m_IsShowing;
        [SerializeField] private Image m_Image;
        [SerializeField] private Material m_MaskMaterial;
        [SerializeField] private UnityEngine.Camera m_Camera;
        [SerializeField] private float m_Radius;
        [SerializeField] private bool m_BlocksRaycast;
        [SerializeField] private Transform m_Transform;

        private static readonly int Radius = Shader.PropertyToID("_Radius");
        private static readonly int TargetPosition = Shader.PropertyToID("_TargetPosition");

        public void Show()
        {
            m_Image.material = m_MaskMaterial;

            var transformPosition = m_Transform.position;
            var screenPos = m_Camera.WorldToScreenPoint(transformPosition);
            var uvScreenPos = new Vector2(screenPos.x / Screen.width, screenPos.y / Screen.height);
            m_Image.material.SetVector(TargetPosition, uvScreenPos);
            m_Image.material.SetFloat(Radius, m_Radius);
            m_Image.raycastTarget = m_BlocksRaycast;
            m_Image.enabled = true;
        }

        public void Hide()
        {
            m_Image.material = null;
            m_Image.enabled = false;
        }

        public override void Execute()
        {
            if (m_IsShowing)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}