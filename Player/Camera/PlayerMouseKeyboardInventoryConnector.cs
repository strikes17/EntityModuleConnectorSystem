using System;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerMouseKeyboardInventoryConnector : BehaviourModuleConnector
    {
        [SelfInject] private PlayerInputModule m_InputModule;
        [SelfInject] private InventoryModule m_InventoryModule;
        [Inject(typeof(GuiPdaEntity))] private GuiAbstractVisibilityModule m_GuiPdaVisibilityModule;

        protected override void Initialize()
        {
            m_InputModule.InventorySwitched += InputModuleOnInventorySwitched;
        }

        private void InputModuleOnInventorySwitched()
        {
            bool isShown = m_GuiPdaVisibilityModule.Switch();
            if (isShown)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}