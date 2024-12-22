using System;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class BaseTower : AbstractEntity
    {
        [SerializeField] private BaseTowerDataObject m_BaseTowerDataObject;

        public BaseTowerDataObject BaseTowerDataObject => m_BaseTowerDataObject;

        public virtual int Order => 0;

        protected override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_ValueModules.Clear();

            var valueModules = m_BaseTowerDataObject.TowerBalanceDataObject.ValueModules;
            foreach (var abstractValueModule in valueModules)
            {
                var clone = abstractValueModule.Clone() as AbstractValueModule;
                if (clone.BalanceValueType == BaseEntityBalanceDataObject.BalanceValueType.Single)
                {
                    clone.Value =
                        m_BaseTowerDataObject.TowerBalanceDataObject.GetBalanceSingleValueForLevel(
                            abstractValueModule.GetType(),
                            1);
                }
                else if (clone.BalanceValueType == BaseEntityBalanceDataObject.BalanceValueType.Ranged)
                {
                    var balanceData =
                        m_BaseTowerDataObject.TowerBalanceDataObject.GetBalanceRangedValueForLevel(
                            abstractValueModule.GetType(), 1);
                    var rangedClone = clone as RangedValueModule;
                    rangedClone.InitValues(balanceData.MinValue, balanceData.MaxValue);
                    rangedClone.Value = balanceData.MaxValue;
                }

                m_ValueModules.Add(clone);
                OnValueModuleAdded(clone);
            }
        }
    }
}