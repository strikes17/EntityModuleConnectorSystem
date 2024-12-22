using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Camera;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    public class GuiTowerConvertMenuModule : GuiAbstractBehaviourModule
    {
        public Action<BaseTower, GuiTowerConvertWidgetEntity> ConvertButtonClicked = delegate { };

        [SerializeField] private RectTransform m_Root;
        [SerializeField] private GuiTowerConvertWidgetEntity m_TowerConvertWidgetEntity;

        private List<GuiTowerConvertWidgetEntity> m_Widgets = new();

        public void SpawnButtons(BaseTower baseTower)
        {
            var towerConvertDataObject = baseTower.BaseTowerDataObject.TowerConvertDataObject;
            var towers = towerConvertDataObject.ConvertToTowers.ToList();
            foreach (var widgetEntity in m_Widgets)
            {
                var visibilityModule = widgetEntity.GetBehaviorModuleByType<GuiAbstractVisibilityModule>();
                visibilityModule.Hide();
            }

            foreach (var baseTowerDataObject in towers)
            {
                var widgetEntity = m_Widgets.FirstOrDefault(x => x.BaseTowerDataObject == baseTowerDataObject);
                if (widgetEntity != null)
                {
                    var visibilityModule = widgetEntity.GetBehaviorModuleByType<GuiAbstractVisibilityModule>();
                    visibilityModule.Show();
                    continue;
                }

                widgetEntity = Object.Instantiate(m_TowerConvertWidgetEntity, m_Root);
                var buttonGroupIdModule = widgetEntity.GetBehaviorModuleByType<ButtonGroupIdModule>();
                buttonGroupIdModule.TowerDataObject = baseTowerDataObject;
                m_Widgets.Add(widgetEntity);
                if (widgetEntity != null)
                {
                    var buttonModule = widgetEntity.GetBehaviorModuleByType<GuiButtonModule>();
                    widgetEntity.BaseTowerDataObject = baseTowerDataObject;
                    buttonModule.Clicked += () => { ConvertButtonClicked(baseTower, widgetEntity); };
                }
            }
        }
    }
}