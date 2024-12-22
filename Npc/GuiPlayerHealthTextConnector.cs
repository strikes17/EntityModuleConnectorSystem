using _Project.Scripts.Camera;

namespace _Project.Scripts
{
    public class GuiPlayerHealthTextConnector : BehaviourModuleConnector
    {
        [SelfInject] private GuiTextModule m_TextModule;
        [Inject(typeof(PlayerEntity))] private HealthModule m_PlayerHealth;
        
        protected override void Initialize()
        {
            PlayerHealthOnValueChanged(m_PlayerHealth.Value);
            m_PlayerHealth.ValueChanged += PlayerHealthOnValueChanged;
        }

        private void PlayerHealthOnValueChanged(int health)
        {
            m_TextModule.Text = health.ToString();
        }
    }
}