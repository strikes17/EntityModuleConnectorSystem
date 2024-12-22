using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialFadeScreen : AbstractTutorialAction
    {
        [Inject] private GuiFadeBackingButtonModule m_FadeBackingButtonModule;
        [SerializeField] private bool m_IsShowing;

        private Image m_Image;

        public void Show()
        {
            m_Image = m_FadeBackingButtonModule.Image;
            m_Image.enabled = true;
        }

        public void Hide()
        {
            m_Image = m_FadeBackingButtonModule.Image;
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