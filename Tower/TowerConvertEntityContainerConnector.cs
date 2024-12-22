using UnityEngine;

namespace _Project.Scripts
{
    public class TowerConvertEntityContainerConnector : BehaviourModuleConnector
    {
        [SelfInject] private TowerConvertModule m_TowerConvertModule;
        [Inject] private EntityContainerModule m_EntityContainerModule;

        protected override void Initialize()
        {
            m_TowerConvertModule.TowerConverted += TowerConvertModuleOnTowerConverted;
        }

        private void TowerConvertModuleOnTowerConverted(BaseTower oldTower, BaseTower newTower)
        {
            m_EntityContainerModule.RemoveElement(oldTower);
            m_EntityContainerModule.AddElement(newTower);
        }
    }
}