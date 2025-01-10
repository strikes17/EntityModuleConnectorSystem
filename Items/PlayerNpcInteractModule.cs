using System;
using _Project.Scripts.Camera;

namespace _Project.Scripts
{
    [Serializable]
    public class PlayerNpcInteractModule : AbstractBehaviourModule
    {
        public event Action<NpcEntity> TradeInteracted = delegate { };
    }
}