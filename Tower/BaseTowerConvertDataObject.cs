using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "New Tower Convert Data", fileName = "New Tower Convert Data")]
    public class BaseTowerConvertDataObject : ScriptableObject
    {
        [SerializeField] private List<BaseTowerDataObject> m_ConvertToTowers;

        public IEnumerable<BaseTowerDataObject> ConvertToTowers => m_ConvertToTowers;

        public BaseTowerDataObject GetTowerByElemental(ElementalType elementalType)
        {
            return m_ConvertToTowers.FirstOrDefault(x => x.ElementalType == elementalType);
        }
    }
}