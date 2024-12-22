using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class GuiTowerConvertWidgetEntity : GuiDefaultEntity
    {
        [SerializeField] private Image m_IconImage;
        [SerializeField] private TMP_Text m_PriceText;
        
        private BaseTowerDataObject m_BaseTowerDataObject;

        public BaseTowerDataObject BaseTowerDataObject
        {
            get => m_BaseTowerDataObject;
            set
            {
                m_BaseTowerDataObject = value;
                m_IconImage.sprite = m_BaseTowerDataObject.TowerVisualsData.GuiIcon;
                m_PriceText.text =
                    m_BaseTowerDataObject.TowerBalanceDataObject.FoundationTowerConvertPrice.ToString();
            }
        }
    }
}