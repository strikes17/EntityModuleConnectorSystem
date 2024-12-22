using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialMessageAction : AbstractTutorialAction
    {
        [SerializeField] private bool m_IsShowing;
        [SerializeField] private string m_Message;
        [SerializeField] private Transform m_CharacterPopupTransform;
        [SerializeField] private TMP_Text m_MessageText;

        public void Show()
        {
            m_MessageText.text = m_Message;
            m_CharacterPopupTransform.gameObject.SetActive(true);
        }

        public void Hide()
        {
            m_CharacterPopupTransform.gameObject.SetActive(false);
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