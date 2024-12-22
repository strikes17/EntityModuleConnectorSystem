using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class GridRaycastColliderModule : RaycastTargetBehaviourModule
    {
        public event Action<Vector3> Clicked = delegate { };

        public override void OnStart(RaycastHit raycastHit)
        {

        }

        public override void OnHold(RaycastHit raycastHit)
        {
            
        }

        public override void OnEnd(RaycastHit raycastHit)
        {
            var point = raycastHit.point;
            point.y = m_AbstractEntity.transform.position.y;
            Clicked(point);
        }
    }
}