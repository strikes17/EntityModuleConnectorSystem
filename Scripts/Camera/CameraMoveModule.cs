using System;
using System.Collections;
using System.Collections.Generic;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    [Serializable]
    public class CameraMoveModule : AbstractBehaviourModule
    {
        public event Action<bool> MoveStateUpdated = delegate { };
        public event Action Moved = delegate { };

        [SerializeField] private Transform m_CameraMoveTransform;
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_MinMoveDelta;

        private float m_MoveDelta;

        private Vector2 m_CenteredPosition;
        private Vector2 m_FirstTouchPosition;

        private Moroutine m_SwipeCameraCoroutine;

        public override int Order => 9;

        public bool IsInteractionAvailable => m_InteractionBlockers.Count == 0;

        private Dictionary<int, byte> m_InteractionBlockers = new();

        public void TryBlockInteraction(int blockerHashCode)
        {
            m_InteractionBlockers.TryAdd(blockerHashCode, 0);
        }

        public void TryUnblockInteraction(int blockerHashCode)
        {
            m_InteractionBlockers.Remove(blockerHashCode);
        }

        public void SwipeCameraToPosition(Vector2 position)
        {
            TryBlockInteraction(GetHashCode());
            if (m_SwipeCameraCoroutine != null)
            {
                m_SwipeCameraCoroutine.Stop();
            }

            m_SwipeCameraCoroutine = Moroutine.Run(SwipeCameraCoroutine(position));
        }

        private IEnumerator SwipeCameraCoroutine(Vector2 position, float time = 1f)
        {
            var transform = m_AbstractEntity.transform;
            float progress = 0f;
            Vector2 startPosition = new Vector2(transform.position.x, transform.position.y);
            while (progress < 1f)
            {
                progress += Time.deltaTime;
                var camPosition = Vector2.Lerp(startPosition, position, progress);
                transform.position = new Vector3(camPosition.x, transform.position.y, camPosition.y);
                yield return null;
            }
            transform.position = new Vector3(position.x, transform.position.y, position.y);
            TryUnblockInteraction(GetHashCode());
        }

        public override void OnUpdate()
        {
            if (!IsInteractionAvailable)
            {
                return;
            }

            Vector2 mousePosition = Input.mousePosition;

            InputType inputType = InputType.None;

            if (Input.GetMouseButton(0))
            {
                inputType = InputType.Hold;
            }

            if (Input.GetMouseButtonDown(0))
            {
                inputType = InputType.OneClick;
            }

            if (Input.GetMouseButtonUp(0))
            {
                inputType = InputType.End;
            }

            if (inputType == InputType.OneClick)
            {
                m_CenteredPosition = m_FirstTouchPosition = mousePosition;
                MoveStateUpdated(true);
            }
            else if (inputType == InputType.Hold)
            {
                var direction = -(mousePosition - m_CenteredPosition).normalized;

                m_MoveDelta = (mousePosition - m_FirstTouchPosition).magnitude;

                if ((mousePosition - m_CenteredPosition).magnitude > m_MinMoveDelta)
                {
                    m_CenteredPosition = mousePosition;
                    float aspectRatio = (float)Screen.height / Screen.width;
                    var move = new Vector3(direction.x, 0f, direction.y * aspectRatio) * (Time.deltaTime * m_Speed);
                    move = Quaternion.AngleAxis(45f, Vector3.up) * move;
                    m_CameraMoveTransform.transform.Translate(move, Space.World);
                    Moved();
                }
            }
            else if (inputType == InputType.End)
            {
                MoveStateUpdated(m_MoveDelta < m_MinMoveDelta);
            }
        }
    }
}