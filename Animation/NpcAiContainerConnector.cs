using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class NpcAiContainerConnector : BehaviourModuleConnector
    {
        [Inject] private NpcAiContainer m_NpcAiContainer;
        
        protected override void Initialize()
        {
            m_NpcAiContainer.AddElement(m_AbstractEntity);
        }
    }
}