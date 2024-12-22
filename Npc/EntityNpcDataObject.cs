using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "New Npc Data", fileName = "New Npc Data")]
    public class EntityNpcDataObject : ScriptableObject
    {
        [SerializeField] private EntityNpc m_EntityNpcPrefab;
        [SerializeField] private EntityNpcBalanceDataObject m_NpcBalanceDataObject;

        public EntityNpc EntityNpcPrefab => m_EntityNpcPrefab;

        public EntityNpcBalanceDataObject NpcBalanceDataObject => m_NpcBalanceDataObject;
    }
}