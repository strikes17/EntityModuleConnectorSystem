using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialTurnVisibilityAction : AbstractTutorialAction
    {
        [SerializeField] private bool m_IsShowing;
        [SerializeField] private AbstractEntity m_VisibilityModuleEntity;

        public override void Execute()
        {
            var visibilityModule = m_VisibilityModuleEntity.GetBehaviorModuleByType<GuiAbstractVisibilityModule>();
            if (m_IsShowing)
            {
                visibilityModule.Show();
            }
            else
            {
                visibilityModule.Hide();
            }
        }
    }
}