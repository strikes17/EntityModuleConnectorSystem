using System;

namespace _Project.Scripts
{
    public abstract class GuiAbstractVisibilityModule : GuiAbstractBehaviourModule
    {
        public abstract event Action Shown; 
        
        public abstract event Action Hidden; 
        
        public abstract void Show();

        public abstract void Hide();

        public abstract void DelayedHide();

        public abstract bool Switch();

        public abstract bool IsShown { get; }
    }
}