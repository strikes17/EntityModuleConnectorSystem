using System;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerValueModuleData
    {
        [SerializeField] private string m_Type;
        [SerializeField] private ulong m_Value;

        public ulong Value
        {
            get => m_Value;
            set => m_Value = value;
        }
    }
}