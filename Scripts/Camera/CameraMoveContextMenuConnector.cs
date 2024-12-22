using _Project.Scripts.Camera;

namespace _Project.Scripts
{
    public class CameraMoveContextMenuConnector : BehaviourModuleConnector
    {
        [Inject] private CameraMoveModule m_CameraMoveModule;
        [Inject] private TowerContextMenuModule m_TowerContextMenuModule;
        
        protected override void Initialize()
        {
            m_CameraMoveModule.Moved += CameraMoveModuleOnMoved;
        }

        private void CameraMoveModuleOnMoved()
        {
            m_TowerContextMenuModule.CloseContextMenu();
        }
    }
}