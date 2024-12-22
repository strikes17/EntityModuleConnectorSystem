using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts
{
    public class LevelLoaderModule : AbstractBehaviourModule
    {
        [SerializeField] private GameObject m_LevelPrefab;
        [SerializeField, ReadOnly] private AbstractEntity m_LevelEntity;

        public event Action<AbstractEntity> LevelLoaded = delegate { };

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            AllowPostInitialization(10);
        }

        protected override void PostInitialize()
        {
            var instantiate = Object.Instantiate(m_LevelPrefab, Vector3.zero, Quaternion.identity);
            m_LevelEntity = instantiate.GetComponent<AbstractEntity>();
            LevelLoaded(m_LevelEntity);
            Debug.Log($"Level Loaded!");
        }
    }
}