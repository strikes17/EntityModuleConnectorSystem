using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialBrushDisableInteraction : AbstractTutorialAction
    {
        [Inject] private PlayerBrushModule m_PlayerBrushModule;
        [SerializeField] private bool m_IsBlock;
        
        public void Block()
        {
            m_PlayerBrushModule.TryBlockPainting(GetHashCode());
        }

        public void Unblock()
        {
            m_PlayerBrushModule.TryUnblockPainting(GetHashCode());
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