using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class AbstractValueModule : AbstractValueObject<int>
    {
        public abstract BaseEntityBalanceDataObject.BalanceValueType BalanceValueType { get; }

        [Button]
        private void PrintTypeToConsole()
        {
            Debug.Log(GetType());
        }

        [Button]
        private void UpdateValueChange()
        {
            Value = m_Value;
        }
    }
}