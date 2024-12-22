using UnityEngine;

namespace _Project.Scripts
{
    [ExecuteInEditMode, SelectionBase]
    public class MoveByGridInEditor : MonoBehaviour
    {
        private void Update()
        {
            var position = transform.position;
            position = new Vector3(Mathf.RoundToInt(position.x), 0f, Mathf.RoundToInt(position.z));
            transform.position = position;
        }
    }
}