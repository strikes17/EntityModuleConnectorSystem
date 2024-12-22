using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]

    public class PlayerValueModuleDataList
    {
        [SerializeField] private List<PlayerValueModuleData> m_playerDatas;

        public ulong GetPlayerDataValueForModule(Type type)
        {
            foreach (var playerValueModuleData in PlayerDatas)
            {
                if (playerValueModuleData.GetType() == type)
                {
                    return playerValueModuleData.Value;
                }
            }

            return 0;
        }
        public IEnumerable<PlayerValueModuleData> PlayerDatas => m_playerDatas;
    }
}