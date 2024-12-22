using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialCameraInteractionBlock : AbstractTutorialAction
    {
        [Inject] private CameraInteractModule m_CameraInteractModule;
        [SerializeField] private bool m_IsBlock;

        public void Block()
        {
            m_CameraInteractModule.TryBlockInteraction(1000000000);
        }
        
        public void Unblock()
        {
            m_CameraInteractModule.TryUnblockInteraction(1000000000);
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