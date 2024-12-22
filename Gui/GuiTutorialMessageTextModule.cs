using System;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiTutorialMessageTextModule : AbstractBehaviourModule
    {
        [SerializeField] private TMP_Text m_TMPText;

        public string Message
        {
            get => m_TMPText.text;
            set => m_TMPText.text = value;
        }
    }
}