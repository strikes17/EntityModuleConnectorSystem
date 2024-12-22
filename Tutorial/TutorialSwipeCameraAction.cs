using System;
using _Project.Scripts.Camera;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class TutorialSwipeCameraAction : AbstractTutorialAction
    {
        [SerializeField] private Vector2 m_Position;
        [Inject] private CameraMoveModule m_CameraMoveModule;

        public void Swipe()
        {
            m_CameraMoveModule.SwipeCameraToPosition(m_Position);
        }

        public override void Execute()
        {
            Swipe();
        }
    }
}