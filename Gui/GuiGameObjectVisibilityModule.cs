using System;
using System.Collections;
using Redcode.Moroutines;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    public class GuiGameObjectVisibilityModule : GuiAbstractVisibilityModule
    {
        [SerializeField] private GameObject m_GameObject;
        private Moroutine m_Moroutine;

        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            m_GameObject ??= m_AbstractEntity.gameObject;
        }

        public override event Action Shown = delegate { };
        public override event Action Hidden = delegate { };

        public override void Show()
        {
            m_GameObject.SetActive(true);
            Shown();
        }

        public override void Hide()
        {
            m_GameObject.SetActive(false);
            Hidden();
        }

        public override void DelayedHide()
        {
            if (m_Moroutine != null)
            {
                m_Moroutine.Stop();
            }

            m_Moroutine = Moroutine.Run(DelayedHideCoroutine());
        }

        public override bool Switch()
        {
            if (m_GameObject.activeSelf)
            {
                Hide();
                return false;
            }

            Show();
            return true;
        }

        private IEnumerator DelayedHideCoroutine()
        {
            yield return null;
            Hide();
        }

        public override bool IsShown => m_GameObject.activeSelf;
    }
}