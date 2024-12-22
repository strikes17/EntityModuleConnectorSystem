using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerBrushSetOwnerConnector : BehaviourModuleConnector
    {
        [SerializeField] private AbstractEntity m_OwnerModuleEntity;
        [SerializeField] private AbstractEntity m_PlayerBrushModuleEntity;

        [SelfInject] private SetOwnerModule m_SetOwnerModule;
        [SelfInject] private PlayerBrushModule m_PlayerBrushModule;
        
        protected override void Initialize()
        {
            m_PlayerBrushModule.BrushUsed += PlayerBrushModuleOnBrushUsed;
        }

        private void PlayerBrushModuleOnBrushUsed(AbstractEntity abstractEntity)
        {
            m_SetOwnerModule.SetOwnerForEntity(abstractEntity);
        }
    }
}