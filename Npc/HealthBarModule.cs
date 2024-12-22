using UnityEngine;

namespace _Project.Scripts
{
    public class HealthBarModule : AbstractBehaviourModule
    {
        [SerializeField] private Color m_FullHealthColor;
        [SerializeField] private Color m_ZeroHealthColor;
        [SerializeField] private SpriteRenderer m_SpriteRenderer;
        [SerializeField] private Transform m_HealthBarScaler;

        public void SetValue(int value, int maxValue)
        {
            float progress = (float)value / maxValue;
            var color = Color.Lerp(m_ZeroHealthColor, m_FullHealthColor, progress);
            m_SpriteRenderer.color = color;
            var scale = m_HealthBarScaler.localScale;
            scale.x = Mathf.Lerp(0f, 1f, progress);
            m_HealthBarScaler.localScale = scale;
        }
    }
}