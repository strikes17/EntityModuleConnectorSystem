using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class StateMaterialRendererSwitcher : AbstractStateSwitcherModule
    {
        [SerializeField] private List<Material> m_Materials;
        [SerializeField] private Renderer m_Renderer;

        public override int MaxState => m_Materials.Count - 1;

        protected override void OnStateChanged()
        {
            m_Renderer.material = m_Materials[State];
        }
    }
}