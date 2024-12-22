using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialTowerContextMenuCondition : AbstractTutorialStepCondition
    {
        [Inject] private TowerContextMenuModule m_ContextMenuModule;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_ContextMenuModule.Opened += ContextMenuModuleOnOpened;
        }

        private void ContextMenuModuleOnOpened()
        {
            OnCompleted();
        }
    }
}