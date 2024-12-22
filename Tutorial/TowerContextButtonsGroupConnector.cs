using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TowerContextButtonsGroupConnector : BehaviourModuleConnector
    {
        [Inject] private TowerContextButtonsGroupModule m_ButtonsGroupModule;
        [SelfInject] private ButtonGroupIdModule m_ButtonGroupIdModule;

        [SerializeField] private GuiDefaultEntity m_ButtonEntity;

        protected override void Initialize()
        {
            m_ButtonsGroupModule.AddButton(m_ButtonGroupIdModule.ButtonId, m_ButtonEntity);
            Debug.Log($"Added {m_ButtonGroupIdModule.ButtonId}");
        }
    }
}