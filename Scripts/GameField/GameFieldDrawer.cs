using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class GameFieldDrawer : IHaveInit
    {
        [SerializeField] private GameFieldView m_GameFieldView;
        
        public int Order => 1000;
  
        public void Init()
        {
            
        }

    }
}