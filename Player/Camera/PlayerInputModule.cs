using System;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class PlayerInputModule : AbstractBehaviourModule
    {
        public event Action PlayerEntityInteractionKeyPressed = delegate { };
        public event Action PlayerEntityFirePressed = delegate { };

        public event Action PlayerEntityFireHold = delegate { };

        public event Action PrimaryWeaponSelected = delegate { };

        public event Action SecondaryWeaponSelected = delegate { };

        public event Action PistolWeaponSelected = delegate { };

        public event Action InventorySwitched = delegate { };

        public override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                PlayerEntityInteractionKeyPressed();
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                InventorySwitched();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                PrimaryWeaponSelected();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                SecondaryWeaponSelected();
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                PistolWeaponSelected();
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlayerEntityFirePressed();
            }
            else if (Input.GetMouseButton(0))
            {
                PlayerEntityFireHold();
            }
        }
    }
}