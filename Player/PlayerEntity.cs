using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerEntity : NpcEntity
    {
        [SerializeField] private PlayerDataObject m_PlayerBaseData;

        [SerializeField] private PlayerValueModuleDataList m_PlayerValueModuleActualData;
        [SerializeField] private List<PlayerElementData> m_PlayerElementActualDatas;

        public PlayerValueModuleDataList PlayerValueModuleActualData => m_PlayerValueModuleActualData;

        public IEnumerable<PlayerElementData> PlayerElementActualDatas => m_PlayerElementActualDatas;

        protected override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            
            var valueModules = m_PlayerBaseData.ValueModules;
            foreach (var abstractValueModule in valueModules)
            {
                var clone = abstractValueModule.Clone() as AbstractValueModule;
                m_ValueModules.Add(clone);
                OnValueModuleAdded(clone);
            }

            m_PlayerElementActualDatas = new List<PlayerElementData>();
            m_PlayerElementActualDatas.AddRange(m_PlayerBaseData.PlayerElementDatas.ToList());
        }
    }
}