using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class GuiTowerLevelTextConnector : BehaviourModuleConnector
    {
        [SerializeField] private AbstractEntity m_LevelValueModuleEntity;
        
        [SelfInject] private GuiTextModule m_GuiTextModule;
        
        private LevelValueModule m_LevelValueModule;

        protected override void Initialize()
        {
            m_LevelValueModule = m_LevelValueModuleEntity.GetValueModuleByType<LevelValueModule>();
            m_LevelValueModule.ValueChanged += LevelValueModuleOnValueChanged;
            m_GuiTextModule.Text = m_LevelValueModule.Value.ToString();
        }

        private void LevelValueModuleOnValueChanged(int level)
        {
            m_GuiTextModule.Text = level.ToString();
        }
    }
}