using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class TowerUpgradeStatsConnector : BehaviourModuleConnector
    {
        [InfoBox("LevelValueModule")]
        [SerializeField] private BaseTower m_BaseTower;
        [SelfInject] private LevelValueModule m_LevelValueModule;
        
        private List<AbstractValueModule> m_ValueModules = new();
        
        protected override void Initialize()
        {
            m_ValueModules = m_BaseTower.ValueModules.ToList();
            m_ValueModules.Remove(m_LevelValueModule);
            m_LevelValueModule.ValueChanged += LevelValueModuleOnValueChanged;
        }

        private void LevelValueModuleOnValueChanged(int level)
        {
            foreach (var abstractValueModule in m_ValueModules)
            {
                int balanceModuleValue =
                    m_BaseTower.BaseTowerDataObject.TowerBalanceDataObject.GetBalanceSingleValueForLevel(
                        abstractValueModule.GetType(), level);
                
                Debug.Log($"Updating value for tower {abstractValueModule.GetType()}: {balanceModuleValue}");
                
                abstractValueModule.Value = balanceModuleValue;
            }
        }
    }
}