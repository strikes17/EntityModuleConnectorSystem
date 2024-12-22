using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class ImageStateSwitcherModule : AbstractStateSwitcherModule
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private List<Sprite> m_States;

        public override int MaxState => m_States.Count - 1;

        protected override void OnStateChanged()
        {
            m_Image.sprite = m_States[State];
        }
    }
}