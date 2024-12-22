using System;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerElementData
    {
        [SerializeField] private ElementalType m_ElementalType;
        [SerializeField] private uint m_Level;

        public ElementalType ElementalType => m_ElementalType;

        public uint Level
        {
            get => m_Level;
            set => m_Level = value;
        }
    }
}