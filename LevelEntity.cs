using UnityEngine;

namespace _Project.Scripts
{
    public class LevelEntity : AbstractEntity
    {
        protected override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            Debug.Log($"Level entity init");
        }
    }
}