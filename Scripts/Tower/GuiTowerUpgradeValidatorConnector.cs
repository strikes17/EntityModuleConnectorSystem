using System.Collections.Generic;
using _Project.Scripts.Camera;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class GuiTowerUpgradeValidatorConnector : BehaviourModuleConnector
    {
        [InfoBox(
            "GuiButtonModule, CameraEntitySelectorModule, TowerContextMenuModule," +
            " SoftCurrencyValueModule,GuiTextModule, StateSwitcherCompoundModule")]
        [SerializeField]
        private bool m_Yes;

        [SerializeField] protected List<AbstractEntity> m_Entities; 
        [Inject(typeof(PlayerEntity))] private SoftCurrencyValueModule m_SoftCurrencyValueModule;

        [SelfInject] private GuiButtonModule m_UpgradeButtonModule;
        [Inject] private CameraEntitySelectorModule m_EntitySelectorModule;
        [Inject] private TowerContextMenuModule m_TowerContextMenuModule;
        
        private GuiTextModule m_UpgradePriceTextModule;
        private StateSwitcherCompoundModule m_SwitcherCompoundModule;

        private TowerUpgradeModule m_TowerUpgradeModule;
        
        protected override void Initialize()
        {
            foreach (var abstractEntity in m_Entities)
            {
                var module4 = abstractEntity.GetBehaviorModuleByType<StateSwitcherCompoundModule>();
                if (module4 != null)
                {
                    m_SwitcherCompoundModule = module4;
                }

                var module5 = abstractEntity.GetBehaviorModuleByType<GuiTextModule>();
                if (module5 != null)
                {
                    m_UpgradePriceTextModule = module5;
                }
            }
            m_UpgradeButtonModule.Clicked += UpgradeButtonClicked;
            m_TowerContextMenuModule.Opened += TowerContextMenuModuleOnOpened;
        }

        private void TowerContextMenuModuleOnOpened()
        {
            var selectedTower = m_EntitySelectorModule.CurrentSelectedEntity as BaseTower;
            if (selectedTower != null)
            {
                int currentTowerLevel = selectedTower.GetValueModuleByType<LevelValueModule>().Value;
                var maxLevel =
                    selectedTower.BaseTowerDataObject.TowerBalanceDataObject.GetBalanceMaxLevelForModule(
                        typeof(LevelValueModule));
                int upgradePrice =
                    selectedTower.BaseTowerDataObject.TowerBalanceDataObject.GetBalanceSingleValueForLevel(
                        typeof(SoftCurrencyValueModule), currentTowerLevel);
                if (m_SoftCurrencyValueModule.Value >= upgradePrice && currentTowerLevel < maxLevel)
                {
                    m_SwitcherCompoundModule.State = 0;
                }
                else
                {
                    m_SwitcherCompoundModule.State = 1;
                }

                if (currentTowerLevel < maxLevel)
                {
                    m_UpgradePriceTextModule.Text = upgradePrice.ToString();
                }
                else
                {
                    m_UpgradePriceTextModule.Text = "MAX";
                }
            }
        }

        private void UpgradeButtonClicked()
        {
            var selectedTower = m_EntitySelectorModule.CurrentSelectedEntity as BaseTower;

            if (selectedTower != null)
            {
                m_TowerUpgradeModule = selectedTower.GetBehaviorModuleByType<TowerUpgradeModule>();
            }

            if (m_TowerUpgradeModule != null)
            {
                if (m_SoftCurrencyValueModule != null)
                {
                    int currentTowerLevel = selectedTower.GetValueModuleByType<LevelValueModule>().Value;
                    var maxLevel =
                        selectedTower.BaseTowerDataObject.TowerBalanceDataObject.GetBalanceMaxLevelForModule(
                            typeof(LevelValueModule));
                    int upgradePrice =
                        selectedTower.BaseTowerDataObject.TowerBalanceDataObject.GetBalanceSingleValueForLevel(
                            typeof(SoftCurrencyValueModule), currentTowerLevel);

                    Debug.Log($"Tower current: {currentTowerLevel}, max: {maxLevel}");
                    if (m_SoftCurrencyValueModule.Value >= upgradePrice && currentTowerLevel < maxLevel)
                    {
                        m_SoftCurrencyValueModule.Value -= upgradePrice;
                        m_TowerUpgradeModule.UpgradeTowerToNextLevel();
                        m_EntitySelectorModule.TryDeselectCurrentEntity();
                        m_TowerContextMenuModule.CloseContextMenu();
                    }
                }
            }
        }
    }
}