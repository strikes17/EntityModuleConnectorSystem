using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameUpdateHandler : MonoBehaviour, IHaveInit
    {
        public int Order => 100;

        private List<IUpdateListener> m_UpdateListeners = new();

        private void Update()
        {
            for (var index = 0; index < m_UpdateListeners.Count; index++)
            {
                var updateListener = m_UpdateListeners[index];
                updateListener.OnUpdate();
            }
        }

        public void AddListener(IUpdateListener updateListener)
        {
            //
            m_UpdateListeners.Add(updateListener);
            m_UpdateListeners = m_UpdateListeners.OrderBy(x => x.Order).ToList();
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