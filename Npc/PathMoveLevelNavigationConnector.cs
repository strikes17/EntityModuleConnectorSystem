using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class PathMoveLevelNavigationConnector : BehaviourModuleConnector
    {
        [SelfInject] private PathMoveModule m_PathMoveModule;
        [SelfInject] private AiHostileModule m_AiHostileModule;
        [SelfInject] private GridIntegerPositionModule m_AiGridPosition;
        
        [Inject] private LevelNavigationModule m_LevelNavigationModule;
        [Inject] private HostileNpcTargetModule m_FinalSpotGridPosition;

        protected override void Initialize()
        {
            m_AiHostileModule.TargetedFinalSpot += AiHostileModuleOnTargetedFinalSpot;
        }

        private void AiHostileModuleOnTargetedFinalSpot()
        {
            var pathInteger = m_LevelNavigationModule.FindPath(m_AiGridPosition.GridPosition,
                m_FinalSpotGridPosition.GridPosition, m_AiHostileModule.GetHashCode());
            float rndValue = 0.4f;
            var rndX = Random.Range(-rndValue, rndValue);
            var rndY = Random.Range(-rndValue, rndValue);
            var path = pathInteger.Select(x => { return new Vector2(x.x + rndX, x.y + rndY); }).ToList();
            if (pathInteger.Count > 1)
            {
                m_PathMoveModule.StartFollowingAlongPath(path);
            }
            else
            {
                Debug.LogError($"Path is less than 2 points!");
            }
        }
    }
}