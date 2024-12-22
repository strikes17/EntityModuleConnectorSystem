using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "New Animation Data", fileName = "New Animation Data")]
    public class AnimationDataObject : ScriptableObject
    {
        [SerializeReference] private List<AnimationKey> m_AnimationNames;

        public string GetAnimationKey<T>() where T : AnimationKey
        {
            var animationKey = m_AnimationNames.FirstOrDefault(x => x.GetType() == typeof(T));
            if (animationKey != null)
            {
                return animationKey.Key;
            }

            return "Not defined!";
        }
    }
}