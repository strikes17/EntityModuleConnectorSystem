using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public abstract class EntityBrushEditor : AbstractEntity
    {
        public event Action<AbstractEntity> Created = delegate(AbstractEntity entity) {  };

        public abstract AbstractEntity Create(Vector3 position);

        protected virtual void OnCreated(AbstractEntity abstractEntity)
        {
            Created(abstractEntity);
        }
    }
}