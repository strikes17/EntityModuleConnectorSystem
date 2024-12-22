using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "New Tower Balance Data", fileName = "New Tower Balance Data")]
    public class BaseTowerBalanceDataObject : BaseEntityBalanceDataObject
    {
        [SerializeField] private int m_FoundationTowerConvertPrice;
        [SerializeField] private int m_MixElementsTowerConvertPrice;

        public int FoundationTowerConvertPrice => m_FoundationTowerConvertPrice;

        public int MixElementsTowerConvertPrice => m_MixElementsTowerConvertPrice;

    }
}