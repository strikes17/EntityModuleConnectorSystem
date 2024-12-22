using System;
using _Project.Scripts.Camera;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    public class TowerConvertModule : AbstractBehaviourModule
    {
        public event Action<BaseTower, BaseTower> TowerConverted = delegate(BaseTower towerFrom, BaseTower towerTo) {  };

        [SerializeField] private BaseTowerConvertDataObject baseTowerConvertDataObject;
        
        public void ConvertToElementalTower(ElementalType elementalType)
        {
            var tower = baseTowerConvertDataObject.GetTowerByElemental(elementalType);
            var parent = m_AbstractEntity.transform.parent;
            var instance = Object.Instantiate(tower.TowerPrefab, m_AbstractEntity.transform.position,
                Quaternion.identity, parent);
            TowerConverted(m_AbstractEntity as BaseTower, instance);
            m_AbstractEntity.gameObject.SetActive(false);
            Object.Destroy(m_AbstractEntity.gameObject);
        }
    }
}