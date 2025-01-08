using System;
using UnityEngine;

namespace _Project.Scripts
{
    [Serializable]
    public class GuiCopyRectTransformModule : AbstractBehaviourModule
    {
        [SerializeField] private RectTransform sourceRectTransform;
        [SerializeField] private RectTransform targetRectTransform;
        
        public override void Initialize(AbstractEntity abstractEntity)
        {
            base.Initialize(abstractEntity);
            if (sourceRectTransform == null || targetRectTransform == null) return;

            targetRectTransform.CopyRectTransform(sourceRectTransform); // Copy everything
        }

        public override void OnUpdate()
        {

        }

        public override void OnLateUpdate()
        {
            if (sourceRectTransform == null || targetRectTransform == null) return;

            targetRectTransform.CopyRectTransform(sourceRectTransform); // Copy every frame
        }
    }
}