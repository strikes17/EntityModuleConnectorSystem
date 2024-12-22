using HighlightPlus;
using UnityEngine;

namespace _Project.Scripts
{
    public class HighlightModule : AbstractBehaviourModule
    {
        public void SetHighlightEnabled(AbstractEntity abstractEntity)
        {
            HighlightManager.instance.SelectObject(abstractEntity.transform);
        }
        
        public void SetHighlightDisabled(AbstractEntity abstractEntity)
        {
            HighlightManager.instance.UnselectObject(abstractEntity.transform);
        }
    }
}