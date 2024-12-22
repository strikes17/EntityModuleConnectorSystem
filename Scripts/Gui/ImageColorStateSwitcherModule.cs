using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class ImageColorStateSwitcherModule : AbstractStateSwitcherModule
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private List<Color> m_States;

        public override int MaxState => m_States.Count - 1;

        protected override void OnStateChanged()
        {
            m_Image.color = m_States[State];
        }
    }
}