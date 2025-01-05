using System;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponAnimatorConnector : BehaviourModuleConnector
    {
        [SelfInject] private WeaponModule m_WeaponModule;
        [SelfInject] private AnimatorStateMachineModule m_AnimatorStateMachineModule;
        
        protected override void Initialize()
        {
            m_WeaponModule.WeaponFired += WeaponModuleOnWeaponFired;
        }

        private void WeaponModuleOnWeaponFired(AbstractEntity obj)
        {
            m_AnimatorStateMachineModule.SetAnimationState("Fired");
        }
    }
}