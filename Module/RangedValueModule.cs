using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class RangedValueModule : AbstractValueModule
    {
        [SerializeField] private int m_MinValue;
        [SerializeField] private int m_MaxValue;

        public int MinValue => m_MinValue;

        public int MaxValue => m_MaxValue;

        public override BaseEntityBalanceDataObject.BalanceValueType BalanceValueType =>
            BaseEntityBalanceDataObject.BalanceValueType.Ranged;

        public void InitValues(int minValue, int maxValue)
        {
            m_MinValue = minValue;
            m_MaxValue = maxValue;
        }

        public override int Value
        {
            get => m_Value;
            set => base.Value = Math.Clamp(value, m_MinValue, m_MaxValue);
        }
    }
}