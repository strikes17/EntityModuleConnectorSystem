using UnityEngine;

public static class RectTransformExtensions
{
    /// <summary>
    /// Copies all RectTransform parameters from one RectTransform to another.
    /// </summary>
    /// <param name="target">The RectTransform that will receive the copied parameters.</param>
    /// <param name="source">The RectTransform to copy parameters from.</param>
    public static void CopyRectTransform(this RectTransform target, RectTransform source)
    {
        if (target == null || source == null)
        {
            Debug.LogError("Cannot copy RectTransform: Target or source is null.");
            return;
        }

        // Copy Position & Rotation
        target.localPosition = source.localPosition;
        target.localRotation = source.localRotation;
        target.localScale = source.localScale;


        // Copy Size
        target.sizeDelta = source.sizeDelta;
        target.anchoredPosition = source.anchoredPosition;

        // Copy Anchors
        target.anchorMin = source.anchorMin;
        target.anchorMax = source.anchorMax;


        // Copy Pivot
        target.pivot = source.pivot;

        // Copy Offset
        target.offsetMin = source.offsetMin;
        target.offsetMax = source.offsetMax;


        // Copy Z position

        Vector3 pos = target.localPosition;
        pos.z = source.localPosition.z;
        target.localPosition = pos;
    }
}