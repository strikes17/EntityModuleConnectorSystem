using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class ButtonsGroupModule : AbstractBehaviourModule
    {
        protected Dictionary<string, GuiDefaultEntity> m_Buttons = new();

        public void AddButton(string id, GuiDefaultEntity entity)
        {
            m_Buttons.TryAdd(id, entity);
        }
        
        public void LockAll()
        {
            foreach (var guiDefaultEntity in m_Buttons.Values)
            {
                var button = guiDefaultEntity.GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = false;
                }
            }
        }

        public void UnlockAll()
        {
            foreach (var guiDefaultEntity in m_Buttons.Values)
            {
                var button = guiDefaultEntity.GetComponent<Button>();
                if (button != null)
                {
                    button.interactable = true;
                }
            }
        }

        public void LockAllExcept(string id)
        {
            foreach (var guiDefaultEntity in m_Buttons.Values)
            {
                var button = guiDefaultEntity.GetBehaviorModuleByType<GuiButtonModule>();
                if (button != null)
                {
                    button.Lock();
                }
            }

            var targetButton = m_Buttons.First(x => x.Key == id);
            var btn = targetButton.Value.GetBehaviorModuleByType<GuiButtonModule>();
            btn.Unlock();
        }
    }
}