using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [CreateAssetMenu(menuName = "New Player Data", fileName = "New Player Data")]
    public class PlayerDataObject : ScriptableObject
    {
        [SerializeReference] private List<AbstractValueModule> m_ValueModules;
        [SerializeField] private List<PlayerElementData> playerElementDatas;

        public IEnumerable<PlayerElementData> PlayerElementDatas => playerElementDatas;

        public IEnumerable<AbstractValueModule> ValueModules => m_ValueModules;
    }
}