namespace _Project.Scripts
{
    public class HealthBarConnector : BehaviourModuleConnector
    {
        [SelfInject] private HealthBarModule m_HealthBarModule;
        [SelfInject] private HealthModule m_HealthModule;

        protected override void Initialize()
        {
            m_HealthModule.ValueChanged += HealthModuleOnValueChanged;
        }

        private void HealthModuleOnValueChanged(int healthValue)
        {
            m_HealthBarModule.SetValue(m_HealthModule.Value, m_HealthModule.MaxValue);
        }
    }
}