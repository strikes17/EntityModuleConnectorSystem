using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class ButtonGroupIdModule : AbstractBehaviourModule
    {
        [SerializeField] private string m_ButtonId;
        [NonSerialized] public BaseTowerDataObject TowerDataObject;

        public string ButtonId => TowerDataObject != null ? $"{m_ButtonId}_{TowerDataObject.ElementalType.ToString()}" : m_ButtonId;
    }
}