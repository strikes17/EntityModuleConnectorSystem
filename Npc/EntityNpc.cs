using UnityEngine;

namespace _Project.Scripts
{
    public class EntityNpc : AbstractEntity
    {
        [SerializeField] private EntityNpcDataObject m_EntityNpcDataObject;

        public EntityNpcDataObject EntityNpcDataObject => m_EntityNpcDataObject;

        protected override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_ValueModules.Clear();

            var valueModules = m_EntityNpcDataObject.NpcBalanceDataObject.ValueModules;
            foreach (var abstractValueModule in valueModules)
            {
                var clone = abstractValueModule.Clone() as AbstractValueModule;
                if (clone.BalanceValueType == BaseEntityBalanceDataObject.BalanceValueType.Single)
                {
                    clone.Value =
                        m_EntityNpcDataObject.NpcBalanceDataObject.GetBalanceSingleValueForLevel(
                            abstractValueModule.GetType(),
                            1);
                }
                else if (clone.BalanceValueType == BaseEntityBalanceDataObject.BalanceValueType.Ranged)
                {
                    var balanceData =
                        m_EntityNpcDataObject.NpcBalanceDataObject.GetBalanceRangedValueForLevel(
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