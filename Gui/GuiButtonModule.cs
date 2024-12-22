using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class GuiButtonModule : GuiAbstractBehaviourModule
    {
        public event Action Clicked = delegate { };

        [SerializeField] private Button m_Button;

        protected virtual void OnClick()
        {
            Clicked();
        }

        public void Lock()
        {
            m_Button.interactable = false;
        }
        
        public void Unlock()
        {
            m_Button.interactable = true;
        }

        public override void Initialize(AbstractEntity abstractEntity)
        {
            m_AbstractEntity = abstractEntity;
            m_Button.onClick.AddListener(OnClick);
        }
    }
}