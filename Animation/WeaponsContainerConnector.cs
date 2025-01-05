using System;

namespace _Project.Scripts
{
    [Serializable]
    public class WeaponsContainerConnector : BehaviourModuleConnector
    {
        [Inject] private WeaponsContainer m_WeaponsContainer;

        protected override void Initialize()
        {
            m_WeaponsContainer.AddElement(m_AbstractEntity);
        }
    }
}