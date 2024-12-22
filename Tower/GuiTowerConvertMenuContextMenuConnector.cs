using _Project.Scripts.Camera;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class GuiTowerConvertMenuContextMenuConnector : BehaviourModuleConnector
    {
        [Inject] private GuiTowerConvertMenuModule m_ConvertMenuModule;
        [Inject] private TowerContextMenuModule m_TowerContextMenuModule;
        [Inject] private CameraEntitySelectorModule m_EntitySelectorModule;
        
        protected override void Initialize()
        {
            m_TowerContextMenuModule.Opened += TowerContextMenuModuleOnOpened;
        }

        private void TowerContextMenuModuleOnOpened()
        {
            var selectedEntity = m_EntitySelectorModule.CurrentSelectedEntity;
            var towerEntity = selectedEntity as BaseTower;
            if (selectedEntity != null && towerEntity != null)
            {
                m_ConvertMenuModule.SpawnButtons(towerEntity);
            }
        }
    }
}