using System;

namespace _Project.Scripts
{
    public interface IPoolable<T>
    {
        Action<T> Enabled { set; get; }

        Action<T> Disabled { set; get; }

        bool IsAvailable { get; }

        T CreateInstance();

        void Enable();

        void Disable();
    }
}