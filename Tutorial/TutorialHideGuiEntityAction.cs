using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialHideGuiEntityAction : AbstractTutorialAction
    {
        [Inject] private GuiEntityContainerModule m_GuiEntityContainerModule;
        [SerializeField] private string m_EntityModuleType;
        [SerializeField] private bool m_ShouldHide;

        public override void Execute()
        {
            Type type = Type.GetType(m_EntityModuleType);
            Debug.Log($"Valuetype: {type}");

            var entity = m_GuiEntityContainerModule.ContainerCollection.FirstOrDefault(x =>
                x.GetBehaviorModuleByType(type)?.GetType() == type);

            if (entity != null)
            {
                entity.gameObject.SetActive(!m_ShouldHide);
            }
        }
    }
}