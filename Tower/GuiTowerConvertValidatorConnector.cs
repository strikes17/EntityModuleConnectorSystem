using _Project.Scripts.Camera;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class GuiTowerConvertValidatorConnector : BehaviourModuleConnector
    {
        [InfoBox("GuiTowerConvertMenuModule, CameraEntitySelectorModule, SoftCurrencyValueModule, TowerContextMenuModule")]
        [SerializeField]
        private bool m_Yes;

        [Inject] private GuiTowerConvertMenuModule m_ConvertMenuModule;
        [Inject] private CameraEntitySelectorModule m_EntitySelectorModule;

        private TowerConvertModule m_TowerConvertModule;
        [Inject] private TowerContextMenuModule m_TowerContextMenuModule;
        [Inject(typeof(PlayerEntity))] private SoftCurrencyValueModule m_SoftCurrencyValueModule;

        protected override void Initialize()
        {
            m_ConvertMenuModule.ConvertButtonClicked += ConvertButtonClicked;
        }

        private void ConvertButtonClicked(BaseTower baseTower, GuiTowerConvertWidgetEntity widgetEntity)
        {
            var selectedTower = m_EntitySelectorModule.CurrentSelectedEntity;

            if (selectedTower != null)
            {
                m_TowerConvertModule = selectedTower.GetBehaviorModuleByType<TowerConvertModule>();
            }
            
            if (m_TowerConvertModule != null)
            {
                if (m_SoftCurrencyValueModule != null)
                {
                    var softCurrencyValue =
                        widgetEntity.BaseTowerDataObject.TowerBalanceDataObject.FoundationTowerConvertPrice;

                    if (m_SoftCurrencyValueModule.Value >= softCurrencyValue)
                    {
                        m_SoftCurrencyValueModule.Value -= softCurrencyValue;
                        m_TowerConvertModule.ConvertToElementalTower(widgetEntity.BaseTowerDataObject.ElementalType);
                        m_EntitySelectorModule.TryDeselectCurrentEntity();
                        m_TowerContextMenuModule.CloseContextMenu();
                    }
                }
            }
        }
    }
}