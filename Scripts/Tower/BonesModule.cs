using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class BonesModule : AbstractBehaviourModule
    {
        [SerializeReference] private List<AbstractBone> m_Bones;

        public T GetBone<T>() where T : AbstractBone
        {
            return m_Bones.FirstOrDefault(x => x.GetType() == typeof(T)) as T;
        }
    }
}