﻿using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialClickButtonCondition : AbstractTutorialStepCondition
    {
        [SerializeField] private Button m_Button;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            if (m_Button != null)
            {
                m_Button.onClick.AddListener(ButtonOnClicked);
            }
        }

        private void ButtonOnClicked()
        {
            m_Button.onClick.RemoveListener(ButtonOnClicked);
            OnCompleted();
        }
    }
}