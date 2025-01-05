using System;

namespace _Project.Scripts
{
    [Serializable]
    public class DamageRecieveModule : AbstractBehaviourModule
    {
        public event Action<AbstractEntity, float> DamageRecieved = delegate { };

        public void DealDamage(AbstractEntity source, float damageValue)
        {
            DamageRecieved(source, damageValue);
        }
    }
}