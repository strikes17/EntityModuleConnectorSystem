using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    public class HostileNpcDeathRewardConnector : BehaviourModuleConnector
    {
        [Inject] private EntityContainerModule m_EntityContainerModule;
        [Inject(typeof(PlayerEntity))] private SoftCurrencyValueModule m_SoftCurrencyModule;
        
        protected override void Initialize()
        {
            m_EntityContainerModule.ElementAdded += EntityContainerModuleOnElementAdded;
            foreach (var abstractEntity in m_EntityContainerModule.ContainerCollection)
            {
                EntityContainerModuleOnElementAdded(abstractEntity);
            }
        }

        private void EntityContainerModuleOnElementAdded(AbstractEntity abstractEntity)
        {
            var statesModule = abstractEntity.GetBehaviorModuleByType<StatesModule>();
            if (statesModule != null)
            {
                statesModule.StateChanged += state =>
                {
                    if (state.GetType() == typeof(DeathState))
                    {
                        var rewardModule = abstractEntity.GetValueModuleByType<SoftCurrencyValueModule>();
                        if (rewardModule != null)
                        {
                            m_SoftCurrencyModule.Value += rewardModule.Value;
                        }
                    }
                };
            }
        }
    }
}