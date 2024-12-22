using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    public class FinalPathNodePlayerHealthConnector : BehaviourModuleConnector
    {
        [Inject(typeof(PlayerEntity))] private HealthModule m_PlayerHealth;
        [SelfInject] private PathMoveModule m_PathMoveModule;
        
        protected override void Initialize()
        {
            m_PathMoveModule.ArrivedAtFinalNode += PathMoveModuleOnArrivedAtFinalNode;
        }

        private void PathMoveModuleOnArrivedAtFinalNode(Vector2 finalNodePosition)
        {
            m_PlayerHealth.Value--;
        }
    }
}