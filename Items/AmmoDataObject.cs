using UnityEngine;

namespace _Project.Scripts
{
    public class AmmoDataObject : AbstractPickableItemDataObject
    {
        [SerializeField] private AmmoEntity m_AmmoEntity;

        public AmmoEntity AmmoEntity => m_AmmoEntity;
    }
}