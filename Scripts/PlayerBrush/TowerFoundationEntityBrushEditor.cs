using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    [Serializable]
    public class TowerFoundationEntityBrushEditor : EntityBrushEditor
    {
        [SerializeField] private List<AbstractEntity> m_CreatedTowerFoundations;
        [SerializeField] private AbstractEntity m_TowerFoundation;
        [SerializeField] private Transform m_Parent;
        private static int TOWER_SPAWNED_ID = 0;

        public override AbstractEntity Create(Vector3 position)
        {
            var instance = Instantiate(m_TowerFoundation, position, Quaternion.identity, m_Parent);
            instance.name = $"Tower_{++TOWER_SPAWNED_ID}";
            m_CreatedTowerFoundations.Add(instance);
            OnCreated(instance);
            return instance;
        }

        protected override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            TOWER_SPAWNED_ID = 0;
        }
    }
}