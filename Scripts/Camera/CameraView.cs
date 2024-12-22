using UnityEngine;

namespace _Project.Scripts.Camera
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera m_Camera;

        public UnityEngine.Camera Camera => m_Camera;
    }
}