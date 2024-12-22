using System.Collections;
using Redcode.Moroutines;
using UnityEngine;

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

        public override void Show()
        {
            m_GameObject.SetActive(true);
        }

        public override void Hide()
        {
            m_GameObject.SetActive(false);
        }

        public override void DelayedHide()
        {
            if (m_Moroutine != null)
            {
                m_Moroutine.Stop();
            }

            m_Moroutine = Moroutine.Run(DelayedHideCoroutine());
        }

        private IEnumerator DelayedHideCoroutine()
        {
            yield return null;
            Hide();
        }
    }
}