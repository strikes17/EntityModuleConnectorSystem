using UnityEngine;

namespace _Project.Scripts
{
    public static class Vector3Extensions
    {
        public static Vector3 Rotate(this Vector3 vector, float angleDegrees, Vector3 axis)
        {
            Quaternion rotation = Quaternion.AngleAxis(angleDegrees, axis);
            return rotation * vector;
        }
    }
}