using UnityEngine;

namespace _Project.Scripts
{
    public abstract class AbstractPickableItemDataObject : ScriptableObject
    {
        [SerializeField] protected string m_Id;

        public string Id => m_Id;
    }
}