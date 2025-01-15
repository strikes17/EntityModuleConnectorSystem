using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameUpdateHandler : MonoBehaviour, IHaveInit
    {
        public int Order => 100;

        private List<IUpdateListener> m_UpdateListeners = new();
        private List<ILateUpdateListener> m_LateUpdateListeners = new();
        private List<IFixedUpdateListener> m_FixedUpdateListeners = new();

        private void Update()
        {
            for (var index = 0; index < m_UpdateListeners.Count; index++)
            {
                var updateListener = m_UpdateListeners[index];
                updateListener.OnUpdate();
            }
        }

        private void LateUpdate()
        {
            for (var index = 0; index < m_LateUpdateListeners.Count; index++)
            {
                var updateListener = m_LateUpdateListeners[index];
                updateListener.OnLateUpdate();
            }
        }
        
        private void FixedUpdate()
        {
            for (var index = 0; index < m_FixedUpdateListeners.Count; index++)
            {
                var updateListener = m_FixedUpdateListeners[index];
                updateListener.OnFixedUpdate();
            }
        }

        public void AddUpdateListener(IUpdateListener updateListener)
        {
            m_UpdateListeners.Add(updateListener);
            m_UpdateListeners = m_UpdateListeners.OrderBy(x => x.Order).ToList();
        }
        
        public void AddLateUpdateListener(ILateUpdateListener lateUpdateListener)
        {
            m_LateUpdateListeners.Add(lateUpdateListener);
            m_LateUpdateListeners = m_LateUpdateListeners.OrderBy(x => x.Order).ToList();
        }
        
        public void AddFixedUpdateListener(IFixedUpdateListener fixedUpdateListener)
        {
            m_FixedUpdateListeners.Add(fixedUpdateListener);
            m_FixedUpdateListeners = m_FixedUpdateListeners.OrderBy(x => x.Order).ToList();
        }
        
        public void RemoveListener(IUpdateListener updateListener)
        {
            m_UpdateListeners.Remove(updateListener);
        }

        public void Init()
        {
        }
    }
}