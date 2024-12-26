using System;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerKeyboardInputModule : AbstractBehaviourModule
    {
        public event Action PlayerEntityInteractionKeyPressed = delegate { };

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                PlayerEntityInteractionKeyPressed();
            }
        }
    }
}