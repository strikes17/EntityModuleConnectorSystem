using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "New Animation Data", fileName = "New Animation Data")]
    public class AnimationDataObject : ScriptableObject
    {
        [SerializeReference] private List<AbstractState> m_AnimationNames;

        public string GetAnimationStateName(Type type)
        {
            var animationKey = m_AnimationNames.FirstOrDefault(x => x.GetType() == type);
            if (animationKey != null)
            {
                return animationKey.AnimationStateName;
            }

            return "Not defined!";
        }
    }
}