using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponAnimationLockerConnector : BehaviourModuleConnector
    {
        [SelfInject] private WeaponAnimationLockerModule m_AnimationLockerModule;
        [SelfInject] private HandsAnimatorSwitcherModule m_AnimatorSwitcherModule;

        protected override void Initialize()
        {
            m_AnimatorSwitcherModule.AnimatorChanged += AnimatorSwitcherModuleOnAnimatorChanged;
        }

        private void AnimatorSwitcherModuleOnAnimatorChanged(RuntimeAnimatorController obj)
        {
            Debug.Log($"{obj.name}");
            m_AnimationLockerModule.Start();
        }
    }
}