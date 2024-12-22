using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Redcode.Moroutines;
using UnityEngine;

namespace _Project.Scripts.Camera
{
    public class CameraEntitySelectorModule : AbstractBehaviourModule
    {
        [SerializeField] private AbstractEntity m_CurrentSelectedEntity;
        private AbstractEntity m_PreviousSelectedEntity;
        private Queue<AbstractEntity> m_SelectionQueue = new();

        public AbstractEntity CurrentSelectedEntity => m_CurrentSelectedEntity;

        private bool m_IsLocked;
        private Moroutine m_LockForOneFrameCoroutine;

        public void LockForOneFrame()
        {
            if (m_LockForOneFrameCoroutine != null)
            {
                m_LockForOneFrameCoroutine.Stop();
            }
            m_LockForOneFrameCoroutine = Moroutine.Run(LockForOneFrameCoroutine());
        }

        private IEnumerator LockForOneFrameCoroutine()
        {
            m_IsLocked = true;
            yield return null;
            m_IsLocked = false;
        }
        
        public void TryDeselectCurrentEntity()
        {
            if (m_CurrentSelectedEntity != null)
            {
                m_CurrentSelectedEntity.GetBehaviorModuleByType<AbstractEntitySelectableModule>().Deselect();
                m_PreviousSelectedEntity = null;
                m_CurrentSelectedEntity = null;
                m_SelectionQueue.Clear();
            }
        }

        public void TrySelectTargetEntity(AbstractEntity abstractEntity, RaycastHit hit)
        {
            if (m_IsLocked)
            {
                return;
            }
            if (m_CurrentSelectedEntity == abstractEntity)
            {
                return;
            }

            var selectModule = abstractEntity.GetBehaviorModuleByType<AbstractEntitySelectableModule>();

            if (selectModule != null)
            {
                m_CurrentSelectedEntity = abstractEntity;
                selectModule.Select();
                
                m_SelectionQueue.Enqueue(m_CurrentSelectedEntity);

                if (m_SelectionQueue.Count > 2)
                {
                    m_SelectionQueue.Dequeue();
                }

                m_PreviousSelectedEntity = m_SelectionQueue.FirstOrDefault(x => x != m_CurrentSelectedEntity);
                
                var prevSelectedModule = m_PreviousSelectedEntity?.GetBehaviorModuleByType<AbstractEntitySelectableModule>();
                if (prevSelectedModule != null)
                {
                    prevSelectedModule.Deselect();
                    // Debug.Log($"Previous: {selectModule?.GetInfo()}");
                }
            }
        }
    }
}