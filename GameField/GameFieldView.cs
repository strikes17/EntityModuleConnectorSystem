using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Project.Scripts
{
    public class GameFieldView : MonoBehaviour
    {
        [SerializeField] private TilemapRenderer m_TilemapRenderer;

        [SerializeField] private Sprite m_FieldDefaultSprite;
        [SerializeField] private Sprite m_FieldHoveredSprite;
        
    }
}