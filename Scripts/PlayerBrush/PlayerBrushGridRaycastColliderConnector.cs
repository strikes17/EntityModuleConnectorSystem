using System.Linq;
using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerBrushGridRaycastColliderConnector : BehaviourModuleConnector
    {
        [Inject] private PlayerBrushModule m_PlayerBrushModule;
        [Inject] private LevelNavigationModule m_LevelNavigationModule;
        [Inject] private LevelLoaderModule m_LevelLoaderModule;
        [Inject,SerializeReference] private GridRaycastColliderModule m_GridRaycastColliderModule;

        protected override void Initialize()
        {
            m_GridRaycastColliderModule.Clicked += GridRaycastColliderModuleOnClicked;
        }
        
        private void GridRaycastColliderModuleOnClicked(Vector3 position)
        {
            if (!m_LevelNavigationModule.IsCellBlocked(Mathf.RoundToInt(position.x), Mathf.RoundToInt(position.z)))
            {
                m_PlayerBrushModule.TryUseBrush(position);
            }
        }
    }
}