using UnityEngine;
using TMPro;
using PiratesDream.Managers;

namespace PiratesDream.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("Üst Bilgi Paneli Metinleri")]
        public TextMeshProUGUI goldText;
        public TextMeshProUGUI suppliesText;
        public TextMeshProUGUI portNameText;
        public TextMeshProUGUI shipNameText;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            SetupTextPositions();
        }

        private void SetupTextPositions()
        {
            // Hizalama aralıkları daraltıldı, ekran kenarına tam oturması sağlandı
            FormatText(goldText, new Vector2(20, -25), Color.yellow);
            FormatText(suppliesText, new Vector2(170, -25), Color.cyan);
            FormatText(portNameText, new Vector2(350, -25), Color.white);
            FormatText(shipNameText, new Vector2(580, -25), Color.green);
        }

        private void FormatText(TextMeshProUGUI tmp, Vector2 anchoredPos, Color color)
        {
            if (tmp == null) return;

            // Anchor: Sol-Üst (Top-Left)
            tmp.rectTransform.anchorMin = new Vector2(0, 1);
            tmp.rectTransform.anchorMax = new Vector2(0, 1);
            tmp.rectTransform.pivot = new Vector2(0, 1);
            
            tmp.fontSize = 17; // Ekran genişliğine tam sığacak font boyutu
            tmp.color = color;
            tmp.alignment = TextAlignmentOptions.Left;
            tmp.rectTransform.anchoredPosition = anchoredPos;
            tmp.rectTransform.sizeDelta = new Vector2(200, 35);
        }

        private void Update()
        {
            UpdateDashboardUI();
        }

        public void UpdateDashboardUI()
        {
            if (PlayerManager.Instance == null) return;

            if (goldText != null)
                goldText.text = $"Altın: {PlayerManager.Instance.currentGold}";

            if (suppliesText != null)
                suppliesText.text = $"Erzak: {PlayerManager.Instance.currentSupplies:F0}/{PlayerManager.Instance.maxSupplies:F0}";

            if (portNameText != null)
                portNameText.text = PlayerManager.Instance.currentPort != null 
                    ? $"Liman: {PlayerManager.Instance.currentPort.portName}" 
                    : "Açık Denizde";

            if (shipNameText != null)
                shipNameText.text = PlayerManager.Instance.currentShip != null 
                    ? $"Gemi: {PlayerManager.Instance.currentShip.shipName}" 
                    : "Gemi Yok";
        }
    }
}