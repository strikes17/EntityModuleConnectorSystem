using System.Collections.Generic;
using System.Linq;
using Tayx.Graphy;
using UnityEngine;

namespace _Project.Scripts
{
    public class GuiStartWaveButtonConnector : BehaviourModuleConnector
    {
        [SelfInject] private GuiButtonModule m_GuiButtonModule;
        [SelfInject] private GuiAbstractVisibilityModule m_AbstractVisibilityModule;
        [Inject] private LevelLoaderModule m_LevelLoaderModule;

        private int m_FinishedSpawnersCount = 0;
        private List<HostileNpcSpawnerModule> m_HostileNpcSpawnerModules;
        
        protected override void Initialize()
        {
            m_LevelLoaderModule.LevelLoaded += LevelLoaderModuleOnLevelLoaded;
            m_GuiButtonModule.Clicked += GuiButtonModuleOnClicked;
        }

        private void LevelLoaderModuleOnLevelLoaded(AbstractEntity levelEntity)
        {
            m_HostileNpcSpawnerModules = new List<HostileNpcSpawnerModule>();
            var spawners = Utility.FindEntitiesWithModule<HostileNpcSpawnerModule>();
            List<HostileNpcSpawnerModule> modules =
                spawners.Select(x => x.GetBehaviorModuleByType<HostileNpcSpawnerModule>()).ToList();
            m_HostileNpcSpawnerModules.AddRange(modules);

            foreach (var hostileNpcSpawnerModule in m_HostileNpcSpawnerModules)
            {
                hostileNpcSpawnerModule.SpawnerFinished += HostileNpcSpawnerModuleOnSpawnerFinished;
            }
        }

        private void HostileNpcSpawnerModuleOnSpawnerFinished()
        {
            m_FinishedSpawnersCount++;
            if (m_FinishedSpawnersCount == m_HostileNpcSpawnerModules.Count)
            {
                m_GuiButtonModule.Unlock();
                m_AbstractVisibilityModule.Show();
            }
        }

        private void GuiButtonModuleOnClicked()
        {
            m_GuiButtonModule.Lock();
            m_AbstractVisibilityModule.DelayedHide();
            m_FinishedSpawnersCount = 0;
            foreach (var hostileNpcSpawnerModule in m_HostileNpcSpawnerModules)
            {
                hostileNpcSpawnerModule.Start();
            }
        }
    }
}