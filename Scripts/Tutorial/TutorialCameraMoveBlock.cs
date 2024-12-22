using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialCameraMoveBlock : AbstractTutorialAction
    {
        [Inject] private CameraMoveModule m_CameraMoveModule;
        [SerializeField] private bool m_IsBlock;

        public void Block()
        {
            m_CameraMoveModule.TryBlockInteraction(GetHashCode());
        }
        
        public void Unblock()
        {
            m_CameraMoveModule.TryUnblockInteraction(GetHashCode());
        }
        
        public override void Execute()
        {
            if (m_IsBlock)
            {
                Block();
            }
            else
            {
                Unblock();
            }
        }
    }
}