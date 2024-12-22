using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class TowerContextMenuModule : AbstractBehaviourModule
    {
        public event Action Opened = delegate { };
        public event Action Closed = delegate { };
        
        [SerializeField] private Transform m_CanvasTransform;
        

        public void OpenContextMenu(Vector3 position)
        {
            m_CanvasTransform.position = position;
            m_CanvasTransform.gameObject.SetActive(true);
            Opened();
        }

        public void CloseContextMenu()
        {
            m_CanvasTransform.gameObject.SetActive(false);
            Closed();
        }
    }
}