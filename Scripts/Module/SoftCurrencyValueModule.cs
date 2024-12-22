using System;

namespace _Project.Scripts
{
    [Serializable]
    public class SoftCurrencyValueModule : AbstractValueModule
    {
        public override BaseEntityBalanceDataObject.BalanceValueType BalanceValueType =>
            BaseEntityBalanceDataObject.BalanceValueType.Single;
    }
    
    [Serializable]
    public class HardCurrencyValueModule : AbstractValueModule
    {
        public override BaseEntityBalanceDataObject.BalanceValueType BalanceValueType =>
            BaseEntityBalanceDataObject.BalanceValueType.Single;
    }
    
    [Serializable]
    public class LevelValueModule : AbstractValueModule
    {
        public override BaseEntityBalanceDataObject.BalanceValueType BalanceValueType =>
            BaseEntityBalanceDataObject.BalanceValueType.Single;
    }
    
    [Serializable]
    public class SpeedValueModule : AbstractValueModule
    {
        public override BaseEntityBalanceDataObject.BalanceValueType BalanceValueType =>
            BaseEntityBalanceDataObject.BalanceValueType.Single;
    }

    [Serializable]
    public class AttackReloadModule : AbstractValueModule
    {
        public override BaseEntityBalanceDataObject.BalanceValueType BalanceValueType =>
            BaseEntityBalanceDataObject.BalanceValueType.Single;
    }

    [Serializable]
    public class AttackRangeModule : AbstractValueModule
    {
        public override BaseEntityBalanceDataObject.BalanceValueType BalanceValueType =>
            BaseEntityBalanceDataObject.BalanceValueType.Single;
    }

    [Serializable]
    public class OwnerModule : AbstractValueModule
    {
        public const int PLAYER_OWNER_ID = 0;
        
        public override BaseEntityBalanceDataObject.BalanceValueType BalanceValueType =>
            BaseEntityBalanceDataObject.BalanceValueType.Single;
    }
}