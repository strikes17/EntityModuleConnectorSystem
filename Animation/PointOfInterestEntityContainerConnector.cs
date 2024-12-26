using System;

namespace _Project.Scripts
{
    [Serializable]
    public class PointOfInterestEntityContainerConnector : BehaviourModuleConnector
    {
        [Inject] private PointOfInterestContainer m_PointOfInterestContainer;

        protected override void Initialize()
        {
            m_PointOfInterestContainer.AddElement(m_AbstractEntity);
        }
    }
}