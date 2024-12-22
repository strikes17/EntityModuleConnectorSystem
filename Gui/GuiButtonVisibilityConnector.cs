using UnityEngine;

namespace _Project.Scripts
{
    public class GuiButtonVisibilityConnector : BehaviourModuleConnector
    {
        [SerializeField] private GuiDefaultEntity m_VisibilityEntity;
        [SerializeField] private bool m_ShouldShow;
        
        [SelfInject] private GuiButtonModule m_OpenButtonModule;
        private GuiAbstractVisibilityModule m_AbstractVisibilityModule;
        
        protected override void Initialize()
        {
            m_AbstractVisibilityModule = m_VisibilityEntity.GetBehaviorModuleByType<GuiAbstractVisibilityModule>();
            m_OpenButtonModule.Clicked += OpenButtonModuleOnClicked;
        }

        private void OpenButtonModuleOnClicked()
        {
            if (m_ShouldShow)
            {
                m_AbstractVisibilityModule.Show();
            }
            else
            {
                m_AbstractVisibilityModule.Hide();
            }
        }
    }
}