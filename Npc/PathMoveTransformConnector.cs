using UnityEngine;

namespace _Project.Scripts
{
    public class PathMoveTransformConnector : BehaviourModuleConnector
    {
        [SelfInject] private TransformMoveModule m_TransformMove;
        [SelfInject] private PathMoveModule m_PathMoveModule;
        [SelfInject] private SpeedValueModule m_SpeedValueModule;
        
        protected override void Initialize()
        {
            m_PathMoveModule.StartedFollowingPath += PathMoveModuleOnStartedFollowingNextNode;
            m_PathMoveModule.StartedFollowingNextNode += PathMoveModuleOnStartedFollowingNextNode;
        }

        private void PathMoveModuleOnStartedFollowingNextNode(Vector2 targetNode)
        {
            m_TransformMove.MoveUntilReachTarget(targetNode, m_SpeedValueModule.Value);
        }
    }
}