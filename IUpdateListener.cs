namespace _Project.Scripts
{
    public interface IUpdateListener
    {
        void OnUpdate();

        void OnLateUpdate();
        
        int Order { get; }
    }

    public interface IRegisterUpdateListener
    {
        void Register(GameUpdateHandler gameUpdateHandler);
    }
}