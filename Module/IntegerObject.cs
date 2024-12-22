using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "New Integer Object", fileName = "New Integer Object")]
    public class IntegerObject : ScriptableObject
    {
        [SerializeField] private int m_Value;

        public int Value => m_Value;
    }
}