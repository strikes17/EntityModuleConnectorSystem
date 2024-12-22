namespace _Project.Scripts
{
    public interface IHaveInit
    {
        void Init();
        int Order { get; }
    }
    
    public interface IHavePostInit
    {
        void PostInit();
        int PostInitOrder { get; }
    } 
}