using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts
{
    public class BaseEntityBalanceDataObject : ScriptableObject
    {
        [SerializeReference] private List<AbstractValueModule> m_ValueModules;
        [SerializeField] private List<LevelBalanceListData> m_LevelBalanceDatas;
        
        public IEnumerable<AbstractValueModule> ValueModules
        {
            get
            {
                List<AbstractValueModule> clonedList = new();
                foreach (var abstractValueModule in m_ValueModules)
                {
                    var moduleClone = abstractValueModule.Clone() as AbstractValueModule;
                    clonedList.Add(moduleClone);
                }

                return clonedList;
            }
        }

        public int GetBalanceMaxLevelForModule(Type moduleType)
        {
            var balanceDataForModule = GetBalanceDataForModule(moduleType);
            if (balanceDataForModule != null)
            {
                return balanceDataForModule.MaxLevel;
            }

            return 0;
        }

        public int GetBalanceSingleValueForLevel(Type moduleType, int level)
        {
            var balanceDataForModule = GetBalanceDataForModule(moduleType);
            if (balanceDataForModule != null)
            {
                var data = balanceDataForModule.GetSingleValueBalanceDataForLevel(level);
                if (data != null)
                {
                    return data.Value;
                }

                Debug.Log($"Md: {moduleType}, {GetBalanceMaxLevelForModule(moduleType)}");
                return balanceDataForModule.GetSingleValueBalanceDataForLevel(GetBalanceMaxLevelForModule(moduleType)).Value;
            }

            return -1;
        }

        public LevelBalanceDataRangedValue GetBalanceRangedValueForLevel(Type moduleType, int level)
        {
            var balanceDataForModule = GetBalanceDataForModule(moduleType);
            if (balanceDataForModule != null)
            {
                LevelBalanceDataRangedValue data = balanceDataForModule.GetRangedValueBalanceDataForLevel(level);
                if (data != null)
                {
                    return data;
                }

                Debug.Log($"Md: {moduleType}, {GetBalanceMaxLevelForModule(moduleType)}");
                return balanceDataForModule.GetRangedValueBalanceDataForLevel(GetBalanceMaxLevelForModule(moduleType));
            }

            return null;
        }

        private LevelBalanceListData GetBalanceDataForModule(Type moduleType)
        {
            foreach (var abstractValueModule in m_ValueModules)
            {
                if (abstractValueModule.GetType() == moduleType)
                {
                    foreach (var levelBalanceListData in m_LevelBalanceDatas)
                    {
                        var type = levelBalanceListData.Type;
                        if (type == moduleType)
                        {
                            return levelBalanceListData;
                        }
                    }
                }
            }

            return null;
        }

        [Serializable]
        public class LevelBalanceDataSingleValue
        {
            [SerializeField] private int m_Value;
            [SerializeField] private int m_Level;

            public int Value => m_Value;

            public int Level => m_Level;
        }

        [Serializable]
        public class LevelBalanceDataRangedValue
        {
            [SerializeField] private int m_MinValue;
            [SerializeField] private int m_MaxValue;
            [SerializeField] private int m_Level;

            public int MinValue => m_MinValue;

            public int MaxValue => m_MaxValue;

            public int Level => m_Level;
        }

        public enum BalanceValueType
        {
            Single, Ranged
        }

        [Serializable]
        public class LevelBalanceListData
        {
            [SerializeField] private string m_TypeString;
            [SerializeField] private BalanceValueType m_BalanceValueType;
            [SerializeField, ShowIf(nameof(m_BalanceValueType), BalanceValueType.Single)] private List<LevelBalanceDataSingleValue> m_LevelBalanceDatas;
            [SerializeField, ShowIf(nameof(m_BalanceValueType), BalanceValueType.Ranged)] private List<LevelBalanceDataRangedValue> m_LevelBalanceDataRangedValues;

            public int MaxLevel => Mathf.Max(m_LevelBalanceDatas.Count, m_LevelBalanceDataRangedValues.Count);

            public Type Type => Type.GetType(m_TypeString);

            public LevelBalanceDataSingleValue GetSingleValueBalanceDataForLevel(int level)
            {
                foreach (var levelBalanceData in m_LevelBalanceDatas)
                {
                    if (levelBalanceData.Level == level)
                    {
                        return levelBalanceData;
                    }
                }

                return null;
            }

            public LevelBalanceDataRangedValue GetRangedValueBalanceDataForLevel(int level)
            {
                foreach (var levelBalanceData in m_LevelBalanceDataRangedValues)
                {
                    if (levelBalanceData.Level == level)
                    {
                        return levelBalanceData;
                    }
                }

                return null;
            }
        }
    }
}