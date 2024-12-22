using UnityEngine;

namespace _Project.Scripts
{
    public class TowerSelectModule : AbstractEntitySelectableModule
    {
        // private GameObject m_gg;
        public override void Select()
        {
            OnSelected(m_AbstractEntity);
            // m_gg = GameObject.CreatePrimitive(PrimitiveType.Cube);
            // m_gg.transform.position = m_AbstractEntity.transform.position;
            // m_gg.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        public override void Deselect()
        {
            OnDeselected(m_AbstractEntity);
            // Object.Destroy(m_gg);
        }
    }
}