using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Moroutines;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Project.Scripts
{
    public class HostileNpcSpawnerModule : AbstractBehaviourModule
    {
        public event Action<EntityNpc> SpawnerEmitted = delegate { };
        public event Action SpawnerFinished = delegate { };

        [SerializeField] private Transform m_SpawnTransform;
        [SerializeField] private List<Emitter> m_Emitters;

        private int m_EmitterIndex;
        private int m_SpawnedCount;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            foreach (var emitter in m_Emitters)
            {
                emitter.Finished += EmitterOnFinished;
                emitter.Emitted += EmitterOnEmitted;
            }
        }

        private void EmitterOnEmitted(EntityNpcDataObject npcDataObject)
        {
            var entityNpcPrefab = npcDataObject.EntityNpcPrefab;
            
            var instance = Object.Instantiate(entityNpcPrefab, m_SpawnTransform);
            
            instance.transform.position = m_SpawnTransform.position;
            instance.transform.rotation = entityNpcPrefab.transform.rotation;
            instance.name = $"{entityNpcPrefab.name}_{++m_SpawnedCount}";
            
            SpawnerEmitted(instance);
        }

        public void Start()
        {
            m_EmitterIndex = 0;

            m_Emitters[m_EmitterIndex].Start();
        }

        private void EmitterOnFinished()
        {
            m_EmitterIndex++;
            if (m_EmitterIndex == m_Emitters.Count)
            {
                SpawnerFinished();
            }
            else
            {
                m_Emitters[m_EmitterIndex].Start();
            }
        }

        [Serializable]
        public class Emitter
        {
            public event Action Finished = delegate { };
            public event Action<EntityNpcDataObject> Emitted = delegate { };

            [SerializeField] private List<EntityNpcDataObject> m_NpcDataObjects;
            [SerializeField] private int m_TotalSpawnCount;
            [SerializeField] private float m_SpawnIntervalTime;

            private WaitForSeconds m_WaitForSeconds;
            private int m_Chance;
            private Moroutine m_Moroutine;

            public void Start()
            {
                m_WaitForSeconds = new WaitForSeconds(m_SpawnIntervalTime);

                m_Chance = 100 / m_NpcDataObjects.Count;

                if (m_Moroutine != null)
                {
                    m_Moroutine.Stop();
                }

                m_Moroutine = Moroutine.Run(EmitCoroutine());
            }

            public void Stop()
            {
                if (m_Moroutine != null)
                {
                    m_Moroutine.Stop();
                }
            }

            private IEnumerator EmitCoroutine()
            {
                for (int i = 0; i < m_TotalSpawnCount; i++)
                {
                    int npcVarCount = m_NpcDataObjects.Count;
                    int rand = Random.Range(0, 100);
                    int targetIndex = -1;
                    for (int j = 0; j < npcVarCount; j++)
                    {
                        if (rand < m_Chance * (j + 1))
                        {
                            targetIndex = j;
                            var dataObject = m_NpcDataObjects[targetIndex];
                            Emitted(dataObject);
                            break;
                        }
                    }

                    yield return m_WaitForSeconds;
                }

                Finished();
            }
        }
    }
}