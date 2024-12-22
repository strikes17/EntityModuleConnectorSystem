using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialCreatedTowerCondition : AbstractTutorialStepCondition
    {
        [SerializeField] private BaseTowerDataObject m_BaseTowerDataObject;
        [Inject] private EntityContainerModule m_EntityContainerModule;
        
        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_EntityContainerModule.ElementAdded += ContainerModuleOnElementAdded;
        }

        private void ContainerModuleOnElementAdded(AbstractEntity obj)
        {
            var tower = obj as BaseTower;
            if (tower != null)
            {
                if (tower.BaseTowerDataObject == m_BaseTowerDataObject)
                {
                    OnCompleted();
                }
            }
        }
    }
}