namespace _Project.Scripts
{
    public interface IUpdateListener
    {
        void OnUpdate();

        int Order { get; }
    }
    
    public interface ILateUpdateListener
    {
        void OnLateUpdate();
        
        int Order { get; }
    }
    
    public interface IFixedUpdateListener
    {
        void OnFixedUpdate();
        
        int Order { get; }
    }

    public interface IRegisterUpdateListener
    {
        void Register(GameUpdateHandler gameUpdateHandler);
    }
}