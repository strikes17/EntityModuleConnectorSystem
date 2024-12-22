using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    public class GuiTextSoftCurrencyConnector : BehaviourModuleConnector
    {
        [SelfInject] private GuiTextModule m_GuiTextModule;
        [Inject(typeof(PlayerEntity))] private SoftCurrencyValueModule m_SoftCurrencyValueModule;

        protected override void Initialize()
        {
            m_SoftCurrencyValueModule.ValueChanged += SoftCurrencyValueModuleOnValueChanged;
            m_GuiTextModule.Text = m_SoftCurrencyValueModule.Value.ToString();
        }

        private void SoftCurrencyValueModuleOnValueChanged(int softValue)
        {
            m_GuiTextModule.Text = softValue.ToString();
        }
    }
}