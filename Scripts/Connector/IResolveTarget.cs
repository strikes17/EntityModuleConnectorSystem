using System;

namespace _Project.Scripts
{
    public interface IResolveTarget
    {
        bool IsResolved { get; set; }
        
        public event Action<IResolveTarget> Resolved;
    }
}