using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiFadeBackingButtonModule : GuiButtonModule
    {
        [SerializeField] private Image m_Image;
        [SerializeField] private Button m_Button;

        public Button Button => m_Button;

        public Image Image => m_Image;
    }
}