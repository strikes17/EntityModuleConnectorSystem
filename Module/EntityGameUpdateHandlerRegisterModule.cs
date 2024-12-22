using UnityEngine;

namespace _Project.Scripts
{
    public class EntityGameUpdateHandlerRegisterModule : AbstractBehaviourModule
    {
        [SerializeField] private GameUpdateHandler m_GameUpdateHandler;

        public void Register(AbstractEntity abstractEntity)
        {
            foreach (var behaviourModule in abstractEntity.BehaviourModules)
            {
                behaviourModule.Register(m_GameUpdateHandler);
            }
        }

        public void Unregister(AbstractEntity abstractEntity)
        {
            foreach (var behaviourModule in abstractEntity.BehaviourModules)
            {
                behaviourModule.Unregister(m_GameUpdateHandler);
            }
        }
    }
}