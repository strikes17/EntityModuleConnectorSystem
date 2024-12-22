using System;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiEntityContainerConnector : BehaviourModuleConnector
    {
        [Inject] private GuiEntityContainerModule m_EntityContainerModule;
        
        protected override void Initialize()
        {
            m_EntityContainerModule.AddElement(m_AbstractEntity as GuiDefaultEntity);    
        }
    }
}