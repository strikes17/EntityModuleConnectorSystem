using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class NavigationBlockerContainerConnector : BehaviourModuleConnector
    {
        [Inject] private NavigationBlockerEntityContainerModule m_BlockerEntityContainerModule;
        [SerializeField] private Transform m_Transform;

        protected override void Initialize()
        {
            m_Transform.GetComponentsInChildren<AbstractEntity>()
                .Where(abstractEntity => abstractEntity.GetBehaviorModuleByType<NavigationBlockerModule>() != null)
                .ToList().ForEach(abstractEntity => m_BlockerEntityContainerModule.AddElement(abstractEntity));
        }
    }
}