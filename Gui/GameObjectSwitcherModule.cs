using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    public class GameObjectSwitcherModule : AbstractStateSwitcherModule
    {
        [SerializeField] private GameObject m_GameObject;
        [SerializeField] private List<bool> m_States;

        public override int MaxState => m_States.Count - 1;

        protected override void OnStateChanged()
        {
            m_GameObject.SetActive(m_States[State]);
        }
    }
}