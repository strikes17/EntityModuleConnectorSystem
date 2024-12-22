using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialFadeScreen : AbstractTutorialAction
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private bool m_IsShowing;

        public void Show()
        {
            m_Image.enabled = true;
        }

        public void Hide()
        {
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