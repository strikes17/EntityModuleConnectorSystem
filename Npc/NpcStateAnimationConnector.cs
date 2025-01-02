using System;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class NpcStateAnimationConnector : BehaviourModuleConnector
    {
        [SelfInject] private AnimationModule m_AnimationModule;

        protected override void Initialize()
        {
            m_AnimationModule.PlayAnimation("walk_unarmed");
        }
    }
}