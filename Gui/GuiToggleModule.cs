using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class GuiToggleModule : GuiAbstractBehaviourModule
    {
        [SerializeField] private Toggle m_Toggle;

        public Toggle.ToggleEvent ValueChanged => m_Toggle.onValueChanged;
    }
}