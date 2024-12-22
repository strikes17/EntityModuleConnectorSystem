using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialPointerAction : AbstractTutorialAction
    {
        [SerializeField] private bool m_IsShowing;
        [SerializeField] private RectTransform m_RectTransform;
        [SerializeField] private RectTransform m_Pointer;

        public void Show()
        {
            m_Pointer.anchoredPosition = m_RectTransform.anchoredPosition;
            m_Pointer.gameObject.SetActive(true);
        }
        
        public void Hide()
        {
            m_Pointer.gameObject.SetActive(false);
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