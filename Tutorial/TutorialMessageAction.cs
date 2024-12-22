using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialMessageAction : AbstractTutorialAction
    {
        public enum CharacterPopupPosition
        {
            Top, Center, Bottom
        }

        [SerializeField] private bool m_IsShowing;
        [SerializeField] private string m_Message;
        [SerializeField] private CharacterPopupPosition m_CharacterPopupPosition;

        [Inject] private GuiTutorialCharacterModule m_TutorialCharacterModule;
        [Inject] private GuiTutorialMessageTextModule m_TutorialMessageTextModule;

        private RectTransform m_RectTransform;

        public void Show()
        {
            m_RectTransform = m_TutorialCharacterModule.RectTransform;
            m_TutorialMessageTextModule.Message = m_Message;
            m_TutorialCharacterModule.GameObject.SetActive(true);
            Debug.Log($"Screen height: {Screen.height}");
            int yPos = 0;
            var pos = m_RectTransform.anchoredPosition;
            if (m_CharacterPopupPosition == CharacterPopupPosition.Top)
            {
                yPos = Screen.height / 3;
            }
            else if (m_CharacterPopupPosition == CharacterPopupPosition.Center)
            {
                yPos = 0;
            }
            else
            {
                yPos = -Screen.height / 3;
            }

            pos.y = yPos;
            m_RectTransform.anchoredPosition = pos;
        }

        public void Hide()
        {
            m_TutorialCharacterModule.GameObject.SetActive(false);
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