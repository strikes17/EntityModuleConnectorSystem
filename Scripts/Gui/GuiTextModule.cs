using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class GuiTextModule : GuiAbstractBehaviourModule
    {
        [SerializeField] private TMP_Text m_Text;

        public string Text
        {
            set => m_Text.text = value;
        }
    }
}