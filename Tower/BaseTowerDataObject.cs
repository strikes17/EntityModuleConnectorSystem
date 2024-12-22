using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "New Tower Data", fileName = "New Tower Data")]
    public class BaseTowerDataObject : ScriptableObject
    {
        [Serializable]
        public class BaseTowerVisualsData
        {
            [SerializeField] private Sprite m_GuiIcon;

            public Sprite GuiIcon => m_GuiIcon;
        }

        [SerializeField] private ElementalType m_ElementalType;
        [SerializeField] private string m_Id;
        [SerializeField] private BaseTower m_TowerPrefab;
        [SerializeField] private BaseTowerVisualsData m_TowerVisualsData;
        [SerializeField] private BaseTowerBalanceDataObject m_TowerBalanceDataObject;
        [SerializeField] private BaseTowerConvertDataObject m_TowerConvertDataObject;

        public BaseTowerConvertDataObject TowerConvertDataObject => m_TowerConvertDataObject;

        public BaseTowerBalanceDataObject TowerBalanceDataObject => m_TowerBalanceDataObject;

        public BaseTower TowerPrefab => m_TowerPrefab;

        public ElementalType ElementalType => m_ElementalType;

        public BaseTowerVisualsData TowerVisualsData => m_TowerVisualsData;
    }
}