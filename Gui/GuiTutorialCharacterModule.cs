using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiTutorialCharacterModule : AbstractBehaviourModule
    {
        [SerializeField] private GameObject m_GameObject;
        [SerializeField] private RectTransform m_RectTransform;

        public RectTransform RectTransform => m_RectTransform;

        public GameObject GameObject => m_GameObject;
    }
}