using UnityEngine;

namespace _Project.Scripts
{
    public class TowerUpgradeModule : AbstractBehaviourModule
    {
        [SerializeField] private BaseTower m_BaseTower;
        
        public void UpgradeTowerToNextLevel()
        {
            var levelValueModule = m_BaseTower.GetValueModuleByType<LevelValueModule>();
            if (levelValueModule != null)
            {
                var level = levelValueModule.Value;
                UpgradeTowerToLevel(level + 1);
            }

        }

        private void UpgradeTowerToLevel(int level)
        {
            var levelValueModule = m_BaseTower.GetValueModuleByType<LevelValueModule>();
            levelValueModule.Value = level;
        }
    }
}