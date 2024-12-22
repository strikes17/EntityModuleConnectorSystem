using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialIntroStartCondition : AbstractTutorialStepCondition
    {
        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            Moroutine.Run(WaitForFrame());
        }

        private IEnumerator WaitForFrame()
        {
            yield return null;
            OnCompleted();
        }
    }
}