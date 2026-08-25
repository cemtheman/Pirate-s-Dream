using UnityEngine;
using TMPro;
using PiratesDream.Data;
using PiratesDream.Managers;

namespace PiratesDream.UI
{
    public class MarketUIManager : MonoBehaviour
    {
        public static MarketUIManager Instance { get; private set; }

        [Header("UI Paneli")]
        public GameObject marketPanel;
        public TextMeshProUGUI marketTitleText;
        public TextMeshProUGUI cargoInfoText;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void OpenMarketUI()
        {
            if (marketPanel != null)
            {
                marketPanel.SetActive(true);
                UpdateCargoInfo();
                Debug.Log("<color=cyan>[PAZAR YERİ]</color> Pazar arayüzü açıldı.");
            }
            else
            {
                Debug.LogError("MarketPanel objesi baglanmamis!");
            }
        }

        public void CloseMarketUI()
        {
            if (marketPanel != null)
            {
                marketPanel.SetActive(false);
            }
        }

        public void UpdateCargoInfo()
        {
            if (PlayerManager.Instance == null || cargoInfoText == null) return;

            int currentCargo = PlayerManager.Instance.GetCurrentCargoWeight();
            int maxCapacity = PlayerManager.Instance.currentShip != null ? PlayerManager.Instance.currentShip.cargoCapacity : 0;

            cargoInfoText.text = $"Ambar Doluluğu: {currentCargo} / {maxCapacity} Ton";
        }

        public void BuySampleItem(string itemName, int price)
        {
            if (MarketManager.Instance == null) return;

            MarketItem sampleItem = new MarketItem
            {
                itemName = itemName,
                basePrice = price,
                isAvailable = true
            };

            if (MarketManager.Instance.BuyItem(sampleItem, 1))
            {
                UpdateCargoInfo();
                if (UIManager.Instance != null) UIManager.Instance.UpdateDashboardUI();
            }
        }
    }
}